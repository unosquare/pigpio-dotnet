namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using Swan.DependencyInjection;
    using System;
    using Unosquare.PiGpio.NativeMethods.Interfaces;

    /// <summary>
    /// Provides a software-based PWM service on the associated pin.
    /// </summary>
    /// <seealso cref="GpioPinServiceBase" />
    public class GpioPinSoftPwmService : GpioPinServiceBase
    {
        private readonly IPwmService _pwmService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPinSoftPwmService"/> class.
        /// </summary>
        /// <param name="pin">The pin.</param>
        internal GpioPinSoftPwmService(GpioPin pin)
            : base(pin)
        {
            _pwmService = DependencyContainer.Current.Resolve<IPwmService>();
        }

        /// <summary>
        /// Gets or sets the range of the duty cycle.
        /// </summary>
        public uint Range
        {
            get => _pwmService.GpioGetPwmRange((UserGpio)Pin.BcmPinNumber);
            set => BoardException.ValidateResult(_pwmService.GpioSetPwmRange((UserGpio)Pin.BcmPinNumber, Convert.ToUInt32(value)));
        }

        /// <summary>
        /// Gets or sets the duty cycle. Setting this property starts the PWM pulses.
        /// The default range is 255.
        /// </summary>
        public uint DutyCycle
        {
            get => _pwmService.GpioGetPwmDutyCycle((UserGpio)Pin.BcmPinNumber);
            set => BoardException.ValidateResult(_pwmService.GpioPwm((UserGpio)Pin.BcmPinNumber, Convert.ToUInt32(value)));
        }

        /// <summary>
        /// Gets or sets the frequency (in Hz) at which the PWM runs.
        /// </summary>
        public uint Frequency
        {
            get => _pwmService.GpioGetPwmFrequency((UserGpio)Pin.BcmPinNumber);
            set => BoardException.ValidateResult(_pwmService.GpioSetPwmFrequency((UserGpio)Pin.BcmPinNumber, Convert.ToUInt32(value)));
        }

        /// <summary>
        /// Resolves the availability of this service for the associated pin.
        /// </summary>
        /// <returns>
        /// True when the service is deemed as available.
        /// </returns>
        protected override bool ResolveAvailable()
        {
            return Pin.IsUserGpio;
        }
    }
}
