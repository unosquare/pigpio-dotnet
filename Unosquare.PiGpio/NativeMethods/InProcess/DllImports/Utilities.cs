namespace Unosquare.PiGpio.NativeMethods.InProcess.DllImports
{
    using NativeEnums;
    using NativeTypes;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Provides various utility methods to retrieve hardware and software versions,
    /// time, shell commands, and bitwise masking.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// If the hardware revision can not be found or is not a valid hexadecimal
        /// number the function returns 0.
        ///
        /// The hardware revision is the last few characters on the Revision line of
        /// /proc/cpuinfo.
        ///
        /// The revision number can be used to determine the assignment of GPIO
        /// to pins (see <see cref="PiIO"/>).
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
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioHardwareRevision")]
        public static extern uint GpioHardwareRevision();

        /// <summary>
        /// Returns the pigpio version number.
        /// </summary>
        /// <returns>Returns the pigpio version.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioVersion")]
        public static extern uint GpioVersion();

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
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioTick")]
        public static extern uint GpioTick();

        /// <summary>
        /// Retrieves the seconds and micros variables with the current time.
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
        /// gpioTime(PI_TIME_RELATIVE, &amp;secs, &amp;mics);
        /// printf("library started %d.%03d seconds ago", secs, mics/1000);
        /// </code>
        /// </example>
        /// <param name="timeType">0 (relative), 1 (absolute).</param>
        /// <param name="seconds">a pointer to an int to hold seconds.</param>
        /// <param name="microseconds">a pointer to an int to hold microseconds.</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_TIMETYPE.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioTime")]
        public static extern int GpioTime(TimeType timeType, out int seconds, out int microseconds);

        /// <summary>
        /// Return the current time in seconds since the Epoch.
        /// </summary>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "time_time")]
        public static extern double TimeTime();

        /// <summary>
        /// Gets the bit value at the bit position.
        /// </summary>
        /// <param name="bitPos">bit index from the start of buf.</param>
        /// <param name="buf">array of bits.</param>
        /// <param name="numBits">number of valid bits in buf.</param>
        /// <returns>Returns the value of the bit bitPos bits from the start of buf.  Returns 0 if bitPos is greater than or equal to numBits.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "getBitInBytes")]
        public static extern int GetBitInBytes(int bitPos, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buf, int numBits);

        /// <summary>
        /// Sets the bit bitPos bits from the start of buf to bit.
        ///
        /// </summary>
        /// <param name="bitPos">bit index from the start of buf.</param>
        /// <param name="buf">array of bits.</param>
        /// <param name="bit">0-1, value to set.</param>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "putBitInBytes")]
        public static extern void PutBitInBytes(int bitPos, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buf, int bit);

        /// <summary>
        /// This function uses the system call to execute a shell script
        /// with the given string as its parameter.
        ///
        /// The exit status of the system call is returned if OK, otherwise
        /// PI_BAD_SHELL_STATUS.
        ///
        /// scriptName must exist in /opt/pigpio/cgi and must be executable.
        ///
        /// The returned exit status is normally 256 times that set by the
        /// shell script exit function.  If the script can't be found 32512 will
        /// be returned.
        ///
        /// The following table gives some example returned statuses.
        ///
        /// Script exit status @ Returned system call status
        /// 1                  @ 256
        /// 5                  @ 1280
        /// 10                 @ 2560
        /// 200                @ 51200
        /// script not found   @ 32512.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// // pass two parameters, hello and world
        /// status = shell("scr1", "hello world");
        ///
        /// // pass three parameters, hello, string with spaces, and world
        /// status = shell("scr1", "hello 'string with spaces' world");
        ///
        /// // pass one parameter, hello string with spaces world
        /// status = shell("scr1", "\"hello string with spaces world\"");
        /// </code>
        /// </example>
        /// <remarks>
        ///               '-' and '_' are allowed in the name.
        /// </remarks>
        /// <param name="scriptName">the name of the script, only alphanumeric characters,.</param>
        /// <param name="scriptString">the string to pass to the script.</param>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "shell")]
        public static extern ResultCode Shell(string scriptName, string scriptString);

        /// <summary>
        /// Registers a function to be called (a callback) when a signal occurs.
        ///
        /// The function is passed the signal number.
        ///
        /// One function may be registered per signal.
        ///
        /// The callback may be cancelled by passing NULL.
        ///
        /// By default all signals are treated as fatal and cause the library
        /// to call gpioTerminate and then exit.
        /// </summary>
        /// <param name="signalNumber">0-63.</param>
        /// <param name="f">the callback function.</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_signalNumber.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioSetSignalFunc")]
        public static extern ResultCode GpioSetSignalFunc(uint signalNumber, [In, MarshalAs(UnmanagedType.FunctionPtr)] PiGpioSignalDelegate f);

        /// <summary>
        /// Registers a function to be called (a callback) when a signal occurs.
        ///
        /// The function is passed the signal number and the userData pointer.
        ///
        /// Only one of gpioSetSignalFunc or gpioSetSignalFuncEx can be
        /// registered per signal.
        ///
        /// See gpioSetSignalFunc for further details.
        /// </summary>
        /// <param name="signalNumber">0-63.</param>
        /// <param name="callback">the callback function.</param>
        /// <param name="userData">a pointer to arbitrary user data.</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_signalNumber.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioSetSignalFuncEx")]
        public static extern ResultCode GpioSetSignalFuncEx(uint signalNumber, [In, MarshalAs(UnmanagedType.FunctionPtr)] PiGpioSignalExDelegate callback, UIntPtr userData);

        /// <summary>
        /// Raises the given UNIX signal number (0 to 63).
        /// </summary>
        /// <param name="signalNumber">The UNIX signal number from 0 to 63.</param>
        /// <returns>0 for success.</returns>
        [DllImport("libc.so.6", EntryPoint = "raise")]
        public static extern int RaiseSignal(int signalNumber);
    }
}
