namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;
    using System.Linq;

    /// <summary>
    /// Provides a hardware clock services on the associated pin.
    /// Only a few pins support this.
    /// </summary>
    /// <seealso cref="GpioPinServiceBase" />
    public sealed class GpioPinClockService : GpioPinServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPinClockService"/> class.
        /// </summary>
        /// <param name="pin">The pin.</param>
        internal GpioPinClockService(GpioPin pin)
            : base(pin)
        {
            // placeholder
            if (Constants.HardwareClockPins0.Contains(Pin.PinNumber))
                ClockChannel = 0;
            else if (Constants.HardwareClockPins2.Contains(Pin.PinNumber))
                ClockChannel = 2;
            else
                ClockChannel = -1;
        }

        /// <summary>
        /// Gets the clock channel.
        /// </summary>
        public int ClockChannel { get; private set; }

        /// <summary>
        /// Starts the hardware clock on this pin.
        /// All pins sharing the clock channel and running in clock mode will get theis new frequency.
        /// The frequency must be 0 (off) or 4689-250,000,000 (250M) Hz.
        /// </summary>
        /// <param name="frequency">The frequency. 0 (off) or 4689-250000000 (250M)</param>
        public void Start(int frequency)
        {
            if (ClockChannel < 0) return;
            BoardException.ValidateResult(Pwm.GpioHardwareClock(Pin.PinGpio, Convert.ToUInt32(frequency)));
        }

        /// <summary>
        /// Stops the hardware clock on this pin
        /// </summary>
        public void Stop()
        {
            if (ClockChannel < 0) return;
            BoardException.ValidateResult(Pwm.GpioHardwareClock(Pin.PinGpio, 0));
        }

        /// <summary>
        /// Resolves the availability of this service for the associated pin.
        /// </summary>
        /// <returns>
        /// True when the service is deemed as available.
        /// </returns>
        protected override bool ResolveAvailable()
        {
            if (Board.BoardType == BoardType.Type1 || Board.BoardType == BoardType.Type2)
            {
                if (Pin.PinNumber == 4)
                {
                    ClockChannel = 0;
                    return true;
                }
                else
                {
                    ClockChannel = -1;
                    return false;
                }
            }

            if (Constants.HardwareClockPins0.Contains(Pin.PinNumber))
            {
                ClockChannel = 0;
                return true;
            }

            if (Constants.HardwareClockPins2.Contains(Pin.PinNumber))
            {
                ClockChannel = 2;
                return true;
            }

            ClockChannel = -1;
            return false;
        }
    }
}
