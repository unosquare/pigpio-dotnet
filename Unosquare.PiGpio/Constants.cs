namespace Unosquare.PiGpio
{
    using NativeEnums;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the constants used by the libpigpio library
    /// </summary>
    public static class Constants
    {
        internal const string PiGpioLibrary = "libpigpio.so";

        internal static GpioPad GetPad(SystemGpio gpio)
        {
            var gpioNumber = (int)gpio;
            if (gpioNumber.IsBetween(0, 27))
                return GpioPad.Pad00To27;
            else if (gpioNumber.IsBetween(28, 45))
                return GpioPad.Pad28To45;
            else if (gpioNumber.IsBetween(46, 53))
                return GpioPad.Pad46To53;

            throw new PiGpioException(ResultCode.BadPad, $"Invalid {nameof(SystemGpio)} '{gpioNumber}'");
        }

        internal static GpioPullMode GetDefaultPullMode(SystemGpio gpio)
        {
            var gpioNumber = (int)gpio;
            if (gpioNumber.IsBetween(0, 7))
                return GpioPullMode.Up;
            else if (gpioNumber.IsBetween(9, 27))
                return GpioPullMode.Down;
            else if (gpioNumber.IsBetween(28, 29))
                return GpioPullMode.Off;
            else if (gpioNumber.IsBetween(30, 33))
                return GpioPullMode.Down;
            if (gpioNumber.IsBetween(34, 36))
                return GpioPullMode.Up;
            else if (gpioNumber.IsBetween(37, 43))
                return GpioPullMode.Down;
            else if (gpioNumber.IsBetween(44, 45))
                return GpioPullMode.Off;
            if (gpioNumber.IsBetween(46, 53))
                return GpioPullMode.Up;

            throw new PiGpioException(ResultCode.BadPud, $"Invalid {nameof(GpioPullMode)} '{gpioNumber}'");
        }

        internal static bool IsBetween(this int number, int min, int max)
        {
            return number >= min && number <= max;
        }
    }
}
