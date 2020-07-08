namespace Unosquare.PiGpio.NativeMethods.InProcess
{
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.InProcess.DllImports;
    using Unosquare.PiGpio.NativeMethods.Interfaces;

    /// <summary>
    /// Utility Service In Process strategy pattern implementation.
    /// </summary>
    public class PwmServiceInProcess : IPwmService {
        /// <inheritdoc />
        public ResultCode GpioPwm(UserGpio userGpio, uint dutyCycle)
        {
            return Pwm.GpioPwm(userGpio, dutyCycle);
        }

        /// <inheritdoc />
        public int GpioGetPwmDutyCycle(UserGpio userGpio)
        {
            return Pwm.GpioGetPwmDutyCycle(userGpio);
        }

        /// <inheritdoc />
        public ResultCode GpioServo(UserGpio userGpio, uint pulseWidth)
        {
            return Pwm.GpioServo(userGpio, pulseWidth);
        }

        /// <inheritdoc />
        public int GpioGetServoPulseWidth(UserGpio userGpio)
        {
            return Pwm.GpioGetServoPulseWidth(userGpio);
        }

        /// <inheritdoc />
        public ResultCode GpioSetPwmRange(UserGpio userGpio, uint range)
        {
            return Pwm.GpioSetPwmRange(userGpio, range);
        }

        /// <inheritdoc />
        public int GpioGetPwmRange(UserGpio userGpio)
        {
            return Pwm.GpioGetPwmRange(userGpio);
        }

        /// <inheritdoc />
        public int GpioGetPwmRealRange(UserGpio userGpio)
        {
            return Pwm.GpioGetPwmRealRange(userGpio);
        }

        /// <inheritdoc />
        public ResultCode GpioSetPwmFrequency(UserGpio userGpio, uint frequency)
        {
            return Pwm.GpioSetPwmFrequency(userGpio, frequency);
        }

        /// <inheritdoc />
        public int GpioGetPwmFrequency(UserGpio userGpio)
        {
            return Pwm.GpioGetPwmFrequency(userGpio);
        }

        /// <inheritdoc />
        public ResultCode GpioHardwareClock(SystemGpio gpio, uint clockFrequency)
        {
            return Pwm.GpioHardwareClock(gpio, clockFrequency);
        }

        /// <inheritdoc />
        public ResultCode GpioHardwarePwm(SystemGpio gpio, uint pwmFrequency, uint pwmDutyCycle)
        {
            return Pwm.GpioHardwarePwm(gpio, pwmFrequency, pwmDutyCycle);
        }
    }
}