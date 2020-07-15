namespace Unosquare.PiGpio.NativeMethods.Pipe
{
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure;

    /// <summary>
    /// Pwm Service Pipe strategy pattern.
    /// </summary>
    public class PwmServicePipe : IPwmService
    {
        private readonly IPigpioPipe _pigpioPipe;

        internal PwmServicePipe(IPigpioPipe pipe)
        {
            _pigpioPipe = pipe;
        }

        /// <inheritdoc />
        public ResultCode GpioPwm(UserGpio userGpio, uint dutyCycle)
        {
            return _pigpioPipe.SendCommandWithResultCode($"p {userGpio} {dutyCycle}");
        }

        /// <inheritdoc />
        public uint GpioGetPwmDutyCycle(UserGpio userGpio)
        {
            return BoardException.ValidateResult(_pigpioPipe.SendCommandWithIntResult($"gdc ${userGpio}"));
        }

        /// <inheritdoc />
        public ResultCode GpioServo(UserGpio userGpio, uint pulseWidth)
        {
            return _pigpioPipe.SendCommandWithResultCode($"s ${userGpio} ${pulseWidth}");
        }

        /// <inheritdoc />
        public uint GpioGetServoPulseWidth(UserGpio userGpio)
        {
            return BoardException.ValidateResult(_pigpioPipe.SendCommandWithIntResult($"gpw ${userGpio}"));
        }

        /// <inheritdoc />
        public ResultCode GpioSetPwmRange(UserGpio userGpio, uint range)
        {
            return _pigpioPipe.SendCommandWithResultCode($"prs ${userGpio} ${range}");
        }

        /// <inheritdoc />
        public uint GpioGetPwmRange(UserGpio userGpio)
        {
            return BoardException.ValidateResult(_pigpioPipe.SendCommandWithIntResult($"prg ${userGpio}"));
        }

        /// <inheritdoc />
        public uint GpioGetPwmRealRange(UserGpio userGpio)
        {
            return BoardException.ValidateResult(_pigpioPipe.SendCommandWithIntResult($"prrg ${userGpio}"));
        }

        /// <inheritdoc />
        public ResultCode GpioSetPwmFrequency(UserGpio userGpio, uint frequency)
        {
            return _pigpioPipe.SendCommandWithResultCode($"pfs ${userGpio} {frequency}");
        }

        /// <inheritdoc />
        public uint GpioGetPwmFrequency(UserGpio userGpio)
        {
            return BoardException.ValidateResult(_pigpioPipe.SendCommandWithIntResult($"pfg ${userGpio}"));
        }

        /// <inheritdoc />
        public ResultCode GpioHardwareClock(SystemGpio gpio, uint clockFrequency)
        {
            return _pigpioPipe.SendCommandWithResultCode($"hc ${gpio} ${clockFrequency}");
        }

        /// <inheritdoc />
        public ResultCode GpioHardwarePwm(SystemGpio gpio, uint pwmFrequency, uint pwmDutyCycle)
        {
            return _pigpioPipe.SendCommandWithResultCode($"hp ${gpio} ${pwmFrequency} ${pwmDutyCycle}");
        }
    }
}