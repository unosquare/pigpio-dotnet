namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using Swan.DependencyInjection;
    using System;
    using System.Linq;
    using Unosquare.PiGpio.NativeMethods.Interfaces;

    /// <summary>
    /// Provides hardware-based PWM services on the pin.
    /// </summary>
    /// <seealso cref="GpioPinServiceBase" />
    public sealed class GpioPinPwmService : GpioPinServiceBase
    {
        private static readonly int[] PwmChannelZeroPins = { 12, 18, 40, 52 };
        private static readonly int[] PwmChannelOnePins = { 13, 19, 41, 45, 53 };
        private readonly IPwmService _pwmService;

        internal GpioPinPwmService(GpioPin pin)
           : base(pin)
        {
            _pwmService = DependencyContainer.Current.Resolve<IPwmService>();
        }

        /// <summary>
        /// Gets the range of the duty cycle.
        /// </summary>
        public uint Range
        {
            get => _pwmService.GpioGetPwmRealRange((UserGpio)Pin.BcmPinNumber);
            set => BoardException.ValidateResult(_pwmService.GpioSetPwmRange((UserGpio)Pin.BcmPinNumber, value));
        }

        /// <summary>
        /// Gets the frequency.
        /// </summary>
        public uint Frequency
        {
            get => _pwmService.GpioGetPwmFrequency((UserGpio)Pin.BcmPinNumber);
            set => BoardException.ValidateResult(_pwmService.GpioSetPwmFrequency((UserGpio)Pin.BcmPinNumber, value));
        }

        /// <summary>
        /// Gets the duty cycle.
        /// </summary>
        public uint DutyCycle
        {
            get => _pwmService.GpioGetPwmDutyCycle((UserGpio)Pin.BcmPinNumber);
            set => BoardException.ValidateResult(_pwmService.GpioPwm((UserGpio)Pin.BcmPinNumber, value));
        }

        /// <summary>
        /// Gets the PWM channel, 0 or 1. A negative number mans there is no associated PWM channel.
        /// </summary>
        public int Channel { get; private set; }

        /// <summary>
        /// Starts PWM hardware pulses.
        /// Frequencies above 30MHz are unlikely to work.
        /// </summary>
        /// <param name="frequency">The frequency. 0 (off) or 1-125000000 (125M).</param>
        /// <param name="dutyCycle">0 (off) to 1000000 (1M)(fully on).</param>
        public void Start(int frequency, int dutyCycle)
        {
            BoardException.ValidateResult(
                _pwmService.GpioHardwarePwm(Pin.PinGpio, Convert.ToUInt32(frequency), Convert.ToUInt32(dutyCycle)));
        }

        /// <summary>
        /// Stops PWM hardware pulses.
        /// </summary>
        public void Stop() => Start(0, 0);

        /// <inheritdoc />
        protected override bool ResolveAvailable()
        {
            if (Board.BoardType == BoardType.Type1 || Board.BoardType == BoardType.Type2)
            {
                if (Pin.BcmPinNumber == 18)
                {
                    Channel = 0;
                    return true;
                }

                Channel = -1;
                return false;
            }

            if (PwmChannelZeroPins.Contains(Pin.BcmPinNumber))
            {
                Channel = 0;
                return true;
            }

            if (PwmChannelOnePins.Contains(Pin.BcmPinNumber))
            {
                Channel = 1;
                return true;
            }

            Channel = -1;
            return false;
        }
    }
}
