using Unosquare.PiGpio.NativeEnums;
using Unosquare.PiGpio.NativeMethods.Interfaces;

namespace Unosquare.PiGpio.NativeMethods.Socket
{
    /// <summary>
    /// Pwm Service Socket strategy pattern.
    /// </summary>
    public class PwmServiceSocket : IPwmService
    {

        /// <inheritdoc />
        public ResultCode GpioPwm(UserGpio userGpio, uint dutyCycle)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public uint GpioGetPwmDutyCycle(UserGpio userGpio)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioServo(UserGpio userGpio, uint pulseWidth)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public uint GpioGetServoPulseWidth(UserGpio userGpio)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetPwmRange(UserGpio userGpio, uint range)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public uint GpioGetPwmRange(UserGpio userGpio)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public uint GpioGetPwmRealRange(UserGpio userGpio)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetPwmFrequency(UserGpio userGpio, uint frequency)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public uint GpioGetPwmFrequency(UserGpio userGpio)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioHardwareClock(SystemGpio gpio, uint clockFrequency)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioHardwarePwm(SystemGpio gpio, uint pwmFrequency, uint pwmDutyCycle)
        {
            throw new System.NotImplementedException();
        }
    }
}