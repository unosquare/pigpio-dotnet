namespace Unosquare.PiGpio
{
    using Enums;
    using System.Runtime.InteropServices;

    public static partial class NativeMethods
    {
        /// <summary>
        /// Tick is the number of microseconds since system boot.
        ///
        /// As tick is an unsigned 32 bit quantity it wraps around after
        /// 2^32 microseconds, which is approximately 1 hour 12 minutes.
        ///
        /// You don't need to worry about the wrap around as long as you
        /// take a tick (uint) from another tick, i.e. the following
        /// code will always provide the correct difference.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// uint startTick, endTick;
        /// int diffTick;
        ///
        /// startTick = gpioTick();
        ///
        /// // do some processing
        ///
        /// endTick = gpioTick();
        ///
        /// diffTick = endTick - startTick;
        ///
        /// printf("some processing took %d microseconds", diffTick);
        /// </code>
        /// </example>
        /// <returns>Returns the current system tick.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioTick")]
        public static extern uint GpioTick();

        /// <summary>
        /// If the hardware revision can not be found or is not a valid hexadecimal
        /// number the function returns 0.
        ///
        /// The hardware revision is the last few characters on the Revision line of
        /// /proc/cpuinfo.
        ///
        /// The revision number can be used to determine the assignment of GPIO
        /// to pins (see <see cref="Gpio"/>).
        ///
        /// There are at least three types of board.
        ///
        /// Type 1 boards have hardware revision numbers of 2 and 3.
        ///
        /// Type 2 boards have hardware revision numbers of 4, 5, 6, and 15.
        ///
        /// Type 3 boards have hardware revision numbers of 16 or greater.
        ///
        /// for "Revision       : 0002" the function returns 2.
        /// for "Revision       : 000f" the function returns 15.
        /// for "Revision       : 000g" the function returns 0.
        /// </summary>
        /// <returns>Returns the hardware revision.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioHardwareRevision")]
        public static extern uint GpioHardwareRevision();

        /// <summary>
        /// </summary>
        /// <returns>Returns the pigpio version.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioVersion")]
        public static extern uint GpioVersion();

        /// <summary>
        ///
        /// </summary>
        /// <param name="bitPos">bit index from the start of buf</param>
        /// <param name="buf">array of bits</param>
        /// <param name="numBits">number of valid bits in buf</param>
        /// <returns>Returns the value of the bit bitPos bits from the start of buf.  Returns 0 if bitPos is greater than or equal to numBits.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "getBitInBytes")]
        public static extern int GetBitInBytes(int bitPos, byte[] buf, int numBits);

        /// <summary>
        /// Sets the bit bitPos bits from the start of buf to bit.
        ///
        /// </summary>
        /// <param name="bitPos">bit index from the start of buf</param>
        /// <param name="buf">array of bits</param>
        /// <param name="bit">0-1, value to set</param>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "putBitInBytes")]
        public static extern void PutBitInBytes(int bitPos, byte[] buf, int bit);

        /// <summary>
        /// Updates the seconds and micros variables with the current time.
        ///
        /// If timetype is PI_TIME_ABSOLUTE updates seconds and micros with the
        /// number of seconds and microseconds since the epoch (1st January 1970).
        ///
        /// If timetype is PI_TIME_RELATIVE updates seconds and micros with the
        /// number of seconds and microseconds since the library was initialised.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// int secs, mics;
        ///
        /// // print the number of seconds since the library was started
        /// gpioTime(PI_TIME_RELATIVE, &secs, &mics);
        /// printf("library started %d.%03d seconds ago", secs, mics/1000);
        /// </code>
        /// </example>
        /// <param name="timeType">0 (relative), 1 (absolute)</param>
        /// <param name="seconds">a pointer to an int to hold seconds</param>
        /// <param name="microseconds">a pointer to an int to hold microseconds</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_TIMETYPE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioTime")]
        public static extern int GpioTime(uint timeType, out int seconds, out int microseconds);

        /// <summary>
        /// Sleeps for the number of seconds and microseconds specified by seconds
        /// and micros.
        ///
        /// If timetype is PI_TIME_ABSOLUTE the sleep ends when the number of seconds
        /// and microseconds since the epoch (1st January 1970) has elapsed.  System
        /// clock changes are taken into account.
        ///
        /// If timetype is PI_TIME_RELATIVE the sleep is for the specified number
        /// of seconds and microseconds.  System clock changes do not effect the
        /// sleep length.
        ///
        /// For short delays (say, 50 microseonds or less) use <see cref="GpioDelay"/>.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioSleep(PI_TIME_RELATIVE, 2, 500000); // sleep for 2.5 seconds
        ///
        /// gpioSleep(PI_TIME_RELATIVE, 0, 100000); // sleep for 0.1 seconds
        ///
        /// gpioSleep(PI_TIME_RELATIVE, 60, 0);     // sleep for one minute
        /// </code>
        /// </example>
        /// <param name="timetype">0 (relative), 1 (absolute)</param>
        /// <param name="seconds">seconds to sleep</param>
        /// <param name="microseconds">microseconds to sleep</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_TIMETYPE, PI_BAD_SECONDS, or PI_BAD_MICROS.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSleep")]
        public static extern ResultCode GpioSleep(uint timetype, int seconds, int microseconds);

        /// <summary>
        /// Delay execution for a given number of seconds
        ///
        /// </summary>
        /// <param name="seconds">the number of seconds to sleep</param>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "time_sleep")]
        public static extern void TimeSleep(double seconds);

        /// <summary>
        /// Return the current time in seconds since the Epoch.
        /// </summary>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "time_time")]
        public static extern double TimeTime();
    }
}
