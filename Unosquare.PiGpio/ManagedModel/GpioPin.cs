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
            PortId = (int)gpio;
            IsUserGpio = PortId < 32;
            PadId = Constants.GetPad(SystemGpio);
            m_PullMode = Constants.GetDefaultPullMode(SystemGpio);
            Alerts = new GpioPinAlertService(this);
            Interrupts = new GpioPinInterruptService(this);
        }

        /// <summary>
        /// Gets the port identifier.
        /// </summary>
        public int PortId { get; }

        /// <summary>
        /// Gets a value indicating whether this pin is user gpio.
        /// </summary>
        public bool IsUserGpio { get; }

        /// <summary>
        /// Gets the electrical pad this pin belongs to.
        /// </summary>
        public GpioPadId PadId { get; }

        /// <summary>
        /// Gets or sets the resustor pull mode in input mode.
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
        /// Gets or sets the digital value of the pin
        /// </summary>
        public bool Value
        {
            get => IO.GpioRead(SystemGpio);
            set => PiGpioException.ValidateResult(IO.GpioWrite(SystemGpio, value));
        }

        /// <summary>
        /// Provides GPIO change alert services.
        /// </summary>
        public GpioPinAlertService Alerts { get; }

        /// <summary>
        /// Provides GPIO Interrupt Service Routine services.
        /// </summary>
        public GpioPinInterruptService Interrupts { get; }

        /// <summary>
        /// Pulsates the pin for the specified micro seconds.
        /// The value is the start value of the pulse.
        /// </summary>
        /// <param name="microSecs">The micro secs.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public void Pulsate(int microSecs, bool value)
        {
            PiGpioException.ValidateResult(
                IO.GpioTrigger((UserGpio)PortId, Convert.ToUInt32(microSecs), value));
        }
    }
}
