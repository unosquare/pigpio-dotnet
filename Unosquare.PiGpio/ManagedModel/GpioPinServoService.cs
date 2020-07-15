
namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using Swan.DependencyInjection;
    using System;
    using Unosquare.PiGpio.NativeMethods.Interfaces;

    /// <summary>
    /// Provides a standard servo PWM service running at 50Hz.
    /// The pulse width must be 0, or a number between 500 and 2500.
    /// </summary>
    /// <seealso cref="GpioPinServiceBase" />
    public sealed class GpioPinServoService : GpioPinServiceBase
    {
        private readonly IPwmService _pwmService;

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
            _pwmService = DependencyContainer.Current.Resolve<IPwmService>();
        }

        /// <summary>
        /// Gets or sets the width of the pulse in microseconds.
        /// Value must be between 500 and 2500 microseconds.
        /// Setting to 0 will turn off the PWM.
        /// </summary>
        public uint PulseWidth
        {
            get => _pwmService.GpioGetServoPulseWidth((UserGpio)Pin.BcmPinNumber);
            set => BoardException.ValidateResult(
                _pwmService.GpioServo((UserGpio)Pin.BcmPinNumber, Convert.ToUInt32(value)));
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
                PulseWidth = Convert.ToUInt32(PulseWidthMin + (value * PulseWidthRange));
            }
        }

        /// <inheritdoc />
        protected override bool ResolveAvailable() => Pin.IsUserGpio;
    }
}
