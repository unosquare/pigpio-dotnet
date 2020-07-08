using Unosquare.PiGpio.NativeEnums;
using Unosquare.PiGpio.NativeMethods.Interfaces;

namespace Unosquare.PiGpio.NativeMethods.Socket
{
    /// <summary>
    /// Pwm Service Socket strategy pattern.
    /// </summary>
    public class PwmServiceSocket : IPwmService
    {

        public ResultCode GpioPwm(UserGpio userGpio, uint dutyCycle)
        {
            throw new System.NotImplementedException();
        }

        public int GpioGetPwmDutyCycle(UserGpio userGpio)
        {
            throw new System.NotImplementedException();
        }

        public ResultCode GpioServo(UserGpio userGpio, uint pulseWidth)
        {
            throw new System.NotImplementedException();
        }

        public int GpioGetServoPulseWidth(UserGpio userGpio)
        {
            throw new System.NotImplementedException();
        }

        public ResultCode GpioSetPwmRange(UserGpio userGpio, uint range)
        {
            throw new System.NotImplementedException();
        }

        public int GpioGetPwmRange(UserGpio userGpio)
        {
            throw new System.NotImplementedException();
        }

        public int GpioGetPwmRealRange(UserGpio userGpio)
        {
            throw new System.NotImplementedException();
        }

        public ResultCode GpioSetPwmFrequency(UserGpio userGpio, uint frequency)
        {
            throw new System.NotImplementedException();
        }

        public int GpioGetPwmFrequency(UserGpio userGpio)
        {
            throw new System.NotImplementedException();
        }

        public ResultCode GpioHardwareClock(SystemGpio gpio, uint clockFrequency)
        {
            throw new System.NotImplementedException();
        }

        public ResultCode GpioHardwarePwm(SystemGpio gpio, uint pwmFrequency, uint pwmDutyCycle)
        {
            throw new System.NotImplementedException();
        }
    }
}