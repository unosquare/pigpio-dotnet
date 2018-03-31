namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;

    /// <summary>
    /// A class representing a GPIO port (pin)
    /// </summary>
    public sealed class GpioPin
    {
        private SystemGpio SystemGpio = default(SystemGpio);
        private GpioPullMode m_PullMode = GpioPullMode.Off;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPin"/> class.
        /// </summary>
        /// <param name="gpio">The gpio.</param>
        internal GpioPin(SystemGpio gpio)
        {
            SystemGpio = gpio;
            PinNumber = (int)gpio;
            IsUserGpio = PinNumber < 32;
            PadId = Constants.GetPad(SystemGpio);
            m_PullMode = Constants.GetDefaultPullMode(SystemGpio);
            Alerts = new GpioPinAlertService(this);
            Interrupts = new GpioPinInterruptService(this);
            Servo = new GpioPinServoService(this);
        }

        /// <summary>
        /// Gets the BCM pin identifier.
        /// </summary>
        public int PinNumber { get; }

        /// <summary>
        /// Gets a value indicating whether this pin is a user gpio (0 to 31).
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
            get
            {
                return m_PullMode;
            }
            set
            {
                PiGpioException.ValidateResult(IO.GpioSetPullUpDown(SystemGpio, value));
                m_PullMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the digital value of the pin.
        /// This call actively reads or writes the pin.
        /// </summary>
        public bool Value
        {
            get => IO.GpioRead(SystemGpio);
            set => PiGpioException.ValidateResult(IO.GpioWrite(SystemGpio, value));
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
        /// Pulsates the pin for the specified micro seconds.
        /// The value is the start value of the pulse.
        /// </summary>
        /// <param name="microSecs">The micro secs.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void Pulsate(int microSecs, bool value)
        {
            PiGpioException.ValidateResult(
                IO.GpioTrigger((UserGpio)PinNumber, Convert.ToUInt32(microSecs), value));
        }
    }
}
