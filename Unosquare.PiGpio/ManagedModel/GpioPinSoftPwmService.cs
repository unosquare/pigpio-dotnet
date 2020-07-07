namespace Unosquare.PiGpio.ManagedModel
{
    using System;
    using NativeEnums;
    using NativeMethods.InProcess.DllImports;

    /// <summary>
    /// Provides a software-based PWM service on the associated pin.
    /// </summary>
    /// <seealso cref="GpioPinServiceBase" />
    public class GpioPinSoftPwmService : GpioPinServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPinSoftPwmService"/> class.
        /// </summary>
        /// <param name="pin">The pin.</param>
        internal GpioPinSoftPwmService(GpioPin pin)
            : base(pin)
        {
            // placeholder
        }

        /// <summary>
        /// Gets or sets the range of the duty cycle.
        /// </summary>
        public int Range
        {
            get => BoardException.ValidateResult(Pwm.GpioGetPwmRange((UserGpio)Pin.BcmPinNumber));
            set => BoardException.ValidateResult(Pwm.GpioSetPwmRange((UserGpio)Pin.BcmPinNumber, Convert.ToUInt32(value)));
        }

        /// <summary>
        /// Gets or sets the duty cycle. Setting this property starts the PWM pulses.
        /// The default range is 255.
        /// </summary>
        public int DutyCycle
        {
            get => BoardException.ValidateResult(Pwm.GpioGetPwmDutyCycle((UserGpio)Pin.BcmPinNumber));
            set => BoardException.ValidateResult(Pwm.GpioPwm((UserGpio)Pin.BcmPinNumber, Convert.ToUInt32(value)));
        }

        /// <summary>
        /// Gets or sets the frequency (in Hz) at which the PWM runs.
        /// </summary>
        public int Frequency
        {
            get => BoardException.ValidateResult(Pwm.GpioGetPwmFrequency((UserGpio)Pin.BcmPinNumber));
            set => BoardException.ValidateResult(Pwm.GpioSetPwmFrequency((UserGpio)Pin.BcmPinNumber, Convert.ToUInt32(value)));
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
