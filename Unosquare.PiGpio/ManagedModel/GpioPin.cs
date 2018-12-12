namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;

    /// <summary>
    /// A class representing a GPIO port (pin).
    /// </summary>
    public sealed class GpioPin
    {
        private GpioPullMode m_PullMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPin"/> class.
        /// </summary>
        /// <param name="gpio">The gpio.</param>
        internal GpioPin(SystemGpio gpio)
        {
            PinGpio = gpio;
            PinNumber = (int)gpio;
            IsUserGpio = gpio.GetIsUserGpio(Board.BoardType);
            PadId = Constants.GetPad(PinGpio);
            m_PullMode = Constants.GetDefaultPullMode(PinGpio);

            // Instantiate the pin services
            Alerts = new GpioPinAlertService(this);
            Interrupts = new GpioPinInterruptService(this);
            Servo = new GpioPinServoService(this);
            SoftPwm = new GpioPinSoftPwmService(this);
            Clock = new GpioPinClockService(this);
            Pwm = new GpioPinPwmService(this);
        }

        /// <summary>
        /// Gets the BCM pin identifier.
        /// </summary>
        public int PinNumber { get; }

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
        /// Gets or sets the resistor pull mode in input mode.
        /// You typically will need to set this to pull-up mode
        /// for most sensors to perform reliable reads.
        /// </summary>
        public GpioPullMode PullMode
        {
            get => m_PullMode;
            set
            {
                BoardException.ValidateResult(IO.GpioSetPullUpDown(PinGpio, value));
                m_PullMode = value;
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
                var result = IO.GpioGetMode(PinGpio);
                if (result == PinMode.Input || result == PinMode.Output)
                    return (PinDirection)result;

                return PinDirection.Alternative;
            }
            set
            {
                if (value != PinDirection.Input && value != PinDirection.Output)
                    throw new InvalidOperationException("Unable to set the pin mode to an alternative function.");

                BoardException.ValidateResult(
                    IO.GpioSetMode(PinGpio, (PinMode)value));
            }
        }

        /// <summary>
        /// Gets the current pin mode.
        /// </summary>
        public PinMode Mode => IO.GpioGetMode(PinGpio);

        /// <summary>
        /// Gets or sets the digital value of the pin.
        /// This call actively reads or writes the pin.
        /// </summary>
        public bool Value
        {
            get => IO.GpioRead(PinGpio);
            set => BoardException.ValidateResult(IO.GpioWrite(PinGpio, value));
        }

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
                IO.GpioTrigger((UserGpio)PinNumber, Convert.ToUInt32(microSecs), value));
        }

        /// <summary>
        /// The fastest way to read from the pin.
        /// No error checking is performed.
        /// </summary>
        /// <returns>Returns a 0 or a 1 for success. A negative number for error.</returns>
        public int Read() => IO.GpioReadUnmanaged(PinGpio);

        /// <summary>
        /// The fastest way to write to the pin.
        /// Anything non-zero is a high. No error checking is performed.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result code. 0 (OK) for success.</returns>
        public ResultCode Write(int value) => IO.GpioWriteUnmanaged(PinGpio, (DigitalValue)(value == 0 ? 0 : 1));
    }
}
