namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;
    using System.Linq;

    /// <summary>
    /// Provides hardware-based PWM services on the pin.
    /// </summary>
    /// <seealso cref="GpioPinServiceBase" />
    public sealed class GpioPinPwmService : GpioPinServiceBase
    {
        internal GpioPinPwmService(GpioPin pin)
           : base(pin)
        {
            // placeholder
            // TODO: Not fully implemented yet
        }

        /// <summary>
        /// Gets the range of the duty cycle.
        /// </summary>
        public int Range => BoardException.ValidateResult(Pwm.GpioGetPwmRealRange((UserGpio)Pin.PinNumber));

        /// <summary>
        /// Gets the frequency.
        /// </summary>
        public int Frequency => BoardException.ValidateResult(Pwm.GpioGetPwmFrequency((UserGpio)Pin.PinNumber));

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
                Pwm.GpioHardwarePwm(Pin.PinGpio, Convert.ToUInt32(frequency), Convert.ToUInt32(dutyCycle)));
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
                if (Pin.PinNumber == 18)
                {
                    Channel = 0;
                    return true;
                }

                Channel = -1;
                return false;
            }

            if ((new int[] { 12, 18, 40, 52 }).Contains(Pin.PinNumber))
            {
                Channel = 0;
                return true;
            }

            if ((new int[] { 13, 19, 41, 45, 53 }).Contains(Pin.PinNumber))
            {
                Channel = 1;
                return true;
            }

            Channel = -1;
            return false;
        }
    }
}
