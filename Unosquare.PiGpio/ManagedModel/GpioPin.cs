namespace Unosquare.PiGpio.ManagedModel
{
    using System;
    using NativeEnums;
    using NativeMethods.Interfaces;
    using RaspberryIO.Abstractions;
    using Swan.DependencyInjection;

    /// <summary>
    /// A class representing a GPIO port (pin).
    /// </summary>
    public sealed partial class GpioPin: IGpioPin, IDisposable
    {
        private readonly object _syncLock = new object();

        private GpioPullMode _pullMode;
        private GpioPinDriveMode _pinMode;
        private GpioPinResistorPullMode _resistorPullMode;
        private bool _interruptRegistered = false;
        private IIOService IOService { get; }

        private GpioPin()
        {
            IOService = DependencyContainer.Current.Resolve<IIOService>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPin"/> class.
        /// </summary>
        /// <param name="ioService">Service providing IO via the chosen strategy.</param>
        /// <param name="gpio">The system gpio.</param>
        internal GpioPin(SystemGpio gpio)
            : this()
        {
            PinGpio = gpio;
            BcmPinNumber = (int)gpio;
            IsUserGpio = gpio.GetIsUserGpio(Board.BoardType);
            PadId = Constants.GetPad(PinGpio);
            _pullMode = Constants.GetDefaultPullMode(PinGpio);

            BcmPin = (BcmPin)gpio;
            try
            {
                PhysicalPinNumber = Definitions.BcmToPhysicalPinNumber(Board.BoardRevision, BcmPin);
            }
            catch
            {
                PhysicalPinNumber = 0;
            }

            Header = (BcmPinNumber >= 28 && BcmPinNumber <= 31) ? GpioHeader.P5 : GpioHeader.P1;

            // Instantiate the pin services
            Alerts = new GpioPinAlertService(this);
            Interrupts = new GpioPinInterruptService(this);
            Servo = new GpioPinServoService(this);
            SoftPwm = new GpioPinSoftPwmService(this);
            Clock = new GpioPinClockService(this);
            Pwm = new GpioPinPwmService(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPin"/> class.
        /// </summary>
        /// <param name="bcmPin">The BCM gpio number.</param>
        internal GpioPin(BcmPin bcmPin)
            : this()
        {
            BcmPin = bcmPin;
            BcmPinNumber = (int)bcmPin;
            PinGpio = (SystemGpio)bcmPin;
            IsUserGpio = PinGpio.GetIsUserGpio(Board.BoardType);
            PadId = Constants.GetPad(PinGpio);
            _pullMode = Constants.GetDefaultPullMode(PinGpio);

            PhysicalPinNumber = Definitions.BcmToPhysicalPinNumber(Board.BoardRevision, bcmPin);
            Header = (BcmPinNumber >= 28 && BcmPinNumber <= 31) ? GpioHeader.P5 : GpioHeader.P1;

            // Instantiate the pin services
            Alerts = new GpioPinAlertService(this);
            Interrupts = new GpioPinInterruptService(this);
            Servo = new GpioPinServoService(this);
            SoftPwm = new GpioPinSoftPwmService(this);
            Clock = new GpioPinClockService(this);
            Pwm = new GpioPinPwmService(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GpioPin"/> class.
        /// </summary>
        ~GpioPin()
        {
            ReleaseUnmanagedResources();
        }

        /// <summary>
        /// Gets the friendly name of the pin.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the hardware mode capabilities of this pin.
        /// </summary>
        public PinCapabilities Capabilities { get; private set; }

        /// <summary>
        /// Gets or sets the digital value of the pin.
        /// This call actively reads or writes the pin.
        /// </summary>
        public bool Value
        {
            get => IOService.GpioRead(PinGpio);
            set => BoardException.ValidateResult(IOService.GpioWrite(PinGpio, value));
        }

        /// <inheritdoc />
        public BcmPin BcmPin { get; }

        /// <inheritdoc />
        public int BcmPinNumber { get; }

        /// <inheritdoc />
        public int PhysicalPinNumber { get; }

        /// <inheritdoc />
        public GpioHeader Header { get; }

        private IIOService IoService { get; }

        /// <summary>
        /// Gets the pin number as a system GPIO Identifier.
        /// </summary>
        public SystemGpio PinGpio { get; }

        /// <summary>
        /// Gets a value indicating whether this pin is a user gpio (0 to 31)
        /// and also available on the current board type.
        /// </summary>
        public bool IsUserGpio { get; }

        /// <summary>
        /// Gets the electrical pad this pin belongs to.
        /// </summary>
        public GpioPadId PadId { get; }

        /// <summary>
        /// Gets the current pin mode.
        /// </summary>
        public PigpioPinMode Mode => IOService.GpioGetMode(PinGpio);

        /// <summary>
        /// Gets or sets the resistor pull mode in input mode.
        /// You typically will need to set this to pull-up mode
        /// for most sensors to perform reliable reads.
        /// </summary>
        public GpioPullMode PullMode
        {
            get => _pullMode;
            set
            {
                BoardException.ValidateResult(IOService.GpioSetPullUpDown(PinGpio, value));
                _pullMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the direction of the pin.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        /// <exception cref="InvalidOperationException">Unable to set the pin mode to an alternative function.</exception>
        public PinDirection Direction
        {
            get
            {
                var result = IOService.GpioGetMode(PinGpio);
                if (result == PigpioPinMode.Input || result == PigpioPinMode.Output)
                    return (PinDirection)result;

                return PinDirection.Alternative;
            }
            set
            {
                if (value != PinDirection.Input && value != PinDirection.Output)
                    throw new InvalidOperationException("Unable to set the pin mode to an alternative function.");

                BoardException.ValidateResult(
                    IOService.GpioSetMode(PinGpio, (PigpioPinMode)value));
            }
        }

        #region Hardware-Specific Properties

        /// <inheritdoc />
        /// <exception cref="T:System.NotSupportedException">Thrown when a pin does not support the given operation mode.</exception>
        public GpioPinDriveMode PinMode
        {
            get => _pinMode;

            set
            {
                lock (_syncLock)
                {
                    var mode = value;
                    if ((mode == GpioPinDriveMode.GpioClock && !HasCapability(PinCapabilities.GPCLK)) ||
                        (mode == GpioPinDriveMode.PwmOutput && !HasCapability(PinCapabilities.PWM)) ||
                        (mode == GpioPinDriveMode.Input && !HasCapability(PinCapabilities.GP)) ||
                        (mode == GpioPinDriveMode.Output && !HasCapability(PinCapabilities.GP)))
                    {
                        throw new NotSupportedException(
                            $"Pin {BcmPinNumber} '{Name}' does not support mode '{mode}'. Pin capabilities are limited to: {Capabilities}");
                    }

                    switch (mode)
                    {
                        case GpioPinDriveMode.Input:
                            IOService.GpioSetMode(PinGpio, PigpioPinMode.Input);
                            break;
                        case GpioPinDriveMode.Output:
                        case GpioPinDriveMode.PwmOutput:
                            IOService.GpioSetMode(PinGpio, PigpioPinMode.Output);
                            break;
                        case GpioPinDriveMode.Alt0:
                            IOService.GpioSetMode(PinGpio, PigpioPinMode.Alt0);
                            break;
                        case GpioPinDriveMode.Alt1:
                            IOService.GpioSetMode(PinGpio, PigpioPinMode.Alt1);
                            break;
                        case GpioPinDriveMode.Alt2:
                            IOService.GpioSetMode(PinGpio, PigpioPinMode.Alt2);
                            break;
                        case GpioPinDriveMode.Alt3:
                            IOService.GpioSetMode(PinGpio, PigpioPinMode.Alt3);
                            break;
                    }

                    _pinMode = mode;
                }
            }
        }

        /// <inheritdoc />
        public GpioPinResistorPullMode InputPullMode
        {
            get => PinMode == GpioPinDriveMode.Input ? _resistorPullMode : GpioPinResistorPullMode.Off;

            set
            {
                lock (_syncLock)
                {
                    if (PinMode != GpioPinDriveMode.Input)
                    {
                        _resistorPullMode = GpioPinResistorPullMode.Off;
                        throw new InvalidOperationException(
                            $"Unable to set the {nameof(InputPullMode)} for pin {BcmPinNumber} because operating mode is {PinMode}."
                            + $" Setting the {nameof(InputPullMode)} is only allowed if {nameof(PinMode)} is set to {GpioPinDriveMode.Input}");
                    }

                    switch (value)
                    {
                        case GpioPinResistorPullMode.Off:
                            IOService.GpioSetPullUpDown(PinGpio, GpioPullMode.Off);
                            break;
                        case GpioPinResistorPullMode.PullDown:
                            IOService.GpioSetPullUpDown(PinGpio, GpioPullMode.Down);
                            break;
                        case GpioPinResistorPullMode.PullUp:
                            IOService.GpioSetPullUpDown(PinGpio, GpioPullMode.Up);
                            break;
                    }
                    
                    _resistorPullMode = value;
                }
            }
        }

        /// <summary>
        /// Gets the interrupt edge detection mode.
        /// </summary>
        public RaspberryIO.Abstractions.EdgeDetection InterruptEdgeDetection { get; private set; }

        /// <summary>
        /// Determines whether the specified pin has certain capabilities.
        /// </summary>
        /// <param name="capabilities">The capabilities.</param>
        /// <returns>
        ///   <c>true</c> if the specified pin has capabilities; otherwise, <c>false</c>.
        /// </returns>
        public bool HasCapability(PinCapabilities capabilities) =>
            (Capabilities & capabilities) == capabilities;

        #endregion

        #region Pin Read-Write

        /// <inheritdoc />
        public void Write(bool value) => IOService.GpioWrite(PinGpio, value);

        /// <inheritdoc />
        public void Write(GpioPinValue value) => IOService.GpioWrite(PinGpio, value == GpioPinValue.High);

        /// <inheritdoc />
        public bool WaitForValue(GpioPinValue status, int timeOutMillisecond)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void RegisterInterruptCallback(RaspberryIO.Abstractions.EdgeDetection edgeDetection, Action callback)
        {
            lock (_syncLock)
            {
                InterruptEdgeDetection = edgeDetection;
                IOService.GpioSetIsrFunc(PinGpio, Constants.GetEdgeDetection(edgeDetection), 0, (gpio, level, tick) =>
                {
                    if (level != LevelChange.NoChange)
                    {
                        callback.Invoke();
                    }
                });
                _interruptRegistered = true;
            }
        }

        /// <inheritdoc />
        public void RegisterInterruptCallback(RaspberryIO.Abstractions.EdgeDetection edgeDetection, Action<int, int, uint> callback)
        {
            lock (_syncLock)
            {
                InterruptEdgeDetection = edgeDetection;
                IOService.GpioSetIsrFunc(PinGpio, Constants.GetEdgeDetection(edgeDetection), 0, (gpio, level, tick) =>
                {
                    if (level != LevelChange.NoChange)
                    {
                        callback.Invoke(BcmPinNumber, (int)level, tick);
                    }
                });
                _interruptRegistered = true;
            }
        }

        #endregion

        /// <summary>
        /// Provides GPIO change alert services.
        /// This provides more sophisticated notification settings
        /// but it is based on sampling.
        /// </summary>
        public GpioPinAlertService Alerts { get; }

        /// <summary>
        /// Provides GPIO Interrupt Service Routine services.
        /// This is hardware-based input-only notifications.
        /// </summary>
        public GpioPinInterruptService Interrupts { get; }

        /// <summary>
        /// Gets the servo pin service.
        /// This is a standard 50Hz PWM servo that operates
        /// in pulse widths between 500 and 2500 microseconds.
        /// Use the PWM service instead if you wish further flexibility.
        /// </summary>
        public GpioPinServoService Servo { get; }

        /// <summary>
        /// Provides a sfotware based PWM pulse generator.
        /// This and the servo functionality use the DMA and PWM or PCM peripherals
        /// to control and schedule the pulse lengths and dutycycles. Using hardware based
        /// PWM is preferred.
        /// </summary>
        public GpioPinSoftPwmService SoftPwm { get; }

        /// <summary>
        /// Gets a hardware-based clock service. A clock channel spans multiple
        /// pins and therefore, clock frequency is not necessarily a per-pin setting.
        /// </summary>
        public GpioPinClockService Clock { get; }

        /// <summary>
        /// Gets the hardware-based PWM services associated to the pin.
        /// Hardware PWM groups several pins by their PWM channel.
        /// </summary>
        public GpioPinPwmService Pwm { get; }

        /// <summary>
        /// Pulsates the pin for the specified micro seconds.
        /// The value is the start value of the pulse.
        /// </summary>
        /// <param name="microSecs">The micro secs.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void Pulsate(int microSecs, bool value)
        {
            BoardException.ValidateResult(
                IOService.GpioTrigger((UserGpio)BcmPinNumber, Convert.ToUInt32(microSecs), value));
        }

        /// <summary>
        /// The fastest way to read from the pin.
        /// No error checking is performed.
        /// </summary>
        /// <returns>Returns true for success. False for error.</returns>
        public bool Read() => IOService.GpioReadUnmanaged(PinGpio) >= 0;

        /// <summary>
        /// The fastest way to write to the pin.
        /// Anything non-zero is a high. No error checking is performed.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result code. 0 (OK) for success.</returns>
        public ResultCode Write(int value) => IOService.GpioWriteUnmanaged(PinGpio, (DigitalValue)(value == 0 ? 0 : 1));

        /// <inheritdoc/>
        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private void ReleaseUnmanagedResources()
        {
            if (_interruptRegistered)
            {
                IOService.GpioSetIsrFunc(PinGpio, NativeEnums.EdgeDetection.EitherEdge, 0, null);
            }
        }
    }
}
