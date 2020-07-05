namespace Unosquare.PiGpio.ManagedModel
{
    using System;
    using NativeEnums;
    using NativeMethods;

    /// <summary>
    /// Provides a standard servo PWM service running at 50Hz.
    /// The pulse width must be 0, or a number beween 500 and 2500.
    /// </summary>
    /// <seealso cref="GpioPinServiceBase" />
    public sealed class GpioPinServoService : GpioPinServiceBase
    {
        /// <summary>
        /// The pulse width minimum in microseconds.
        /// </summary>
        public const int PulseWidthMin = 500;

        /// <summary>
        /// The pulse width maximum in microseconds.
        /// </summary>
        public const int PulseWidthMax = 2500;

        /// <summary>
        /// The pulse range difference in microseconds.
        /// </summary>
        public const int PulseWidthRange = PulseWidthMax - PulseWidthMin;

        internal GpioPinServoService(GpioPin pin)
            : base(pin)
        {
            // placeholder
        }

        /// <summary>
        /// Gets or sets the width of the pulse in microseconds.
        /// Value must be between 500 and 2500 microseconds.
        /// Setting to 0 will turn off the PWM.
        /// </summary>
        public int PulseWidth
        {
            get => BoardException.ValidateResult(
                Pwm.GpioGetServoPulseWidth((UserGpio)Pin.BcmPinNumber));
            set => BoardException.ValidateResult(
                Pwm.GpioServo((UserGpio)Pin.BcmPinNumber, Convert.ToUInt32(value)));
        }

        /// <summary>
        /// Gets or sets the pulse width as a position percent for 0.0 to 1.0.
        /// Use -1.0 to turn off the PWM pulses.
        /// </summary>
        public double PositionPercent
        {
            get
            {
                var currentWidth = PulseWidth;
                if (currentWidth <= 0) return -1d;

                currentWidth -= PulseWidthMin;
                return currentWidth / (double)PulseWidthRange;
            }
            set
            {
                if (value < 0d)
                {
                    PulseWidth = 0;
                    return;
                }

                if (value > 1d) value = 1d;
                PulseWidth = Convert.ToInt32(PulseWidthMin + (value * PulseWidthRange));
            }
        }

        /// <inheritdoc />
        protected override bool ResolveAvailable() => Pin.IsUserGpio;
    }
}
