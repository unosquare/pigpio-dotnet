namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;

    /// <summary>
    /// Provides software and hardware PWM and clock services.
    /// </summary>
    /// <seealso cref="GpioPinServiceBase" />
    public class GpioPwmService : GpioPinServiceBase
    {
        internal GpioPwmService(GpioPin pin)
            : base(pin)
        {
            // placeholder;
            // TODO: HardwarePwm and HardwareClock
        }

        public int RealRange => BoardException.ValidateResult(Pwm.GpioGetPwmRealRange((UserGpio)Pin.PinNumber));

        public int Range
        {
            get => BoardException.ValidateResult(Pwm.GpioGetPwmRange((UserGpio)Pin.PinNumber));
            set => BoardException.ValidateResult(Pwm.GpioSetPwmRange((UserGpio)Pin.PinNumber, Convert.ToUInt32(value)));
        }

        public int DutyCycle
        {
            get => BoardException.ValidateResult(Pwm.GpioGetPwmDutyCycle((UserGpio)Pin.PinNumber));
            set => BoardException.ValidateResult(Pwm.GpioPwm((UserGpio)Pin.PinNumber, Convert.ToUInt32(value)));
        }

        public int Frequency
        {
            get => BoardException.ValidateResult(Pwm.GpioGetPwmFrequency((UserGpio)Pin.PinNumber));
            set => BoardException.ValidateResult(Pwm.GpioSetPwmFrequency((UserGpio)Pin.PinNumber, Convert.ToUInt32(value)));
        }

        protected override bool ResolveAvailable()
        {
            return Pin.IsUserGpio;
        }
    }
}
