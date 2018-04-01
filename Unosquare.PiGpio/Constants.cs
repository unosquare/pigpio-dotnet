namespace Unosquare.PiGpio
{
    using NativeEnums;

    /// <summary>
    /// Defines the constants used by the libpigpio library
    /// </summary>
    public static class Constants
    {
        internal const string PiGpioLibrary = "libpigpio.so";

        internal static BoardType GetBoardType(long hardwareRevision)
        {
            if (hardwareRevision.IsBetween(2, 3))
                return BoardType.Type1;
            else if (hardwareRevision.IsBetween(4, 6) || hardwareRevision == 15)
                return BoardType.Type2;
            else if (hardwareRevision >= 16)
                return BoardType.Type3;

            return BoardType.Unknown;
        }

        internal static GpioPadId GetPad(SystemGpio gpio)
        {
            var gpioNumber = (int)gpio;
            if (gpioNumber.IsBetween(0, 27))
                return GpioPadId.Pad00To27;
            else if (gpioNumber.IsBetween(28, 45))
                return GpioPadId.Pad28To45;
            else if (gpioNumber.IsBetween(46, 53))
                return GpioPadId.Pad46To53;

            throw new BoardException(ResultCode.BadPad, $"Invalid {nameof(SystemGpio)} '{gpioNumber}'");
        }

        internal static GpioPullMode GetDefaultPullMode(SystemGpio gpio)
        {
            var gpioNumber = (int)gpio;
            if (gpioNumber.IsBetween(0, 8))
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

            throw new BoardException(ResultCode.BadPud, $"Invalid {nameof(GpioPullMode)} '{gpioNumber}'");
        }

        internal static bool IsBetween(this int number, int min, int max)
        {
            return number >= min && number <= max;
        }

        internal static bool IsBetween(this long number, long min, long max)
        {
            return number >= min && number <= max;
        }
    }
}
