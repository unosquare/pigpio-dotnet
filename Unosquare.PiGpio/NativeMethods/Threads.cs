namespace Unosquare.PiGpio.NativeMethods
{
    using NativeEnums;
    using NativeTypes;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// PRovides threading and delay operations.
    /// </summary>
    public static class Threads
    {
        /// <summary>
        /// Registers a function to be called (a callback) every millis milliseconds.
        ///
        /// The function is passed the userData pointer.
        ///
        /// Only one of <see cref="GpioSetTimerFunc"/> or <see cref="GpioSetTimerFuncEx"/> can be
        /// registered per timer.
        ///
        /// See <see cref="GpioSetTimerFunc"/> for further details.
        /// </summary>
        /// <param name="timer">0-9.</param>
        /// <param name="millisecondsTimeout">10-60000</param>
        /// <param name="callback">the function to call</param>
        /// <param name="userData">a pointer to arbitrary user data</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_TIMER, PI_BAD_MS, or PI_TIMER_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetTimerFuncEx")]
        public static extern ResultCode GpioSetTimerFuncEx(TimerId timer, uint millisecondsTimeout, PiGpioTimerExDelegate callback, UIntPtr userData);

        /// <summary>
        /// Registers a function to be called (a callback) every millis milliseconds.
        ///
        /// 10 timers are supported numbered 0 to 9.
        ///
        /// One function may be registered per timer.
        ///
        /// The timer may be cancelled by passing NULL as the function.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// void bFunction(void)
        /// {
        ///    printf("two seconds have elapsed");
        /// }
        ///
        /// // call bFunction every 2000 milliseconds
        /// gpioSetTimerFunc(0, 2000, bFunction);
        /// </code>
        /// </example>
        /// <param name="timer">0-9</param>
        /// <param name="periodMilliseconds">10-60000</param>
        /// <param name="callback">the function to call</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_TIMER, PI_BAD_MS, or PI_TIMER_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetTimerFunc")]
        public static extern ResultCode GpioSetTimerFunc(TimerId timer, uint periodMilliseconds, PiGpioTimerDelegate callback);

        /// <summary>
        /// Starts a new thread of execution with f as the main routine.
        ///
        /// The function is passed the single argument arg.
        ///
        /// The thread can be cancelled by passing the pointer to pthread_t to
        /// <see cref="GpioStopThread"/>.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// #include &lt;stdio.h&gt;
        /// #include &lt;pigpio.h&gt;
        ///
        /// void *myfunc(void *arg)
        /// {
        ///    while (1)
        ///    {
        ///       printf("%s", arg);
        ///       sleep(1);
        ///    }
        /// }
        ///
        /// int main(int argc, char *argv[])
        /// {
        ///    pthread_t *p1, *p2, *p3;
        ///
        ///    if (gpioInitialise() &lt; 0) return 1;
        ///
        ///    p1 = gpioStartThread(myfunc, "thread 1"); sleep(3);
        ///
        ///    p2 = gpioStartThread(myfunc, "thread 2"); sleep(3);
        ///
        ///    p3 = gpioStartThread(myfunc, "thread 3"); sleep(3);
        ///
        ///    gpioStopThread(p3); sleep(3);
        ///
        ///    gpioStopThread(p2); sleep(3);
        ///
        ///    gpioStopThread(p1); sleep(3);
        ///
        ///    gpioTerminate();
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">the main function for the new thread</param>
        /// <param name="userData">a pointer to arbitrary user data</param>
        /// <returns>Returns a pointer to pthread_t if OK, otherwise NULL.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioStartThread")]
        public static extern UIntPtr GpioStartThread(PiGpioThreadDelegate callback, UIntPtr userData);

        /// <summary>
        /// Cancels the thread pointed at by threadHandle.
        ///
        /// No value is returned.
        ///
        /// The thread to be stopped should have been started with <see cref="GpioStartThread"/>.
        /// </summary>
        /// <param name="handle">a thread pointer returned by <see cref="GpioStartThread"/></param>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioStopThread")]
        public static extern void GpioStopThread(UIntPtr handle);

        /// <summary>
        /// Delays for at least the number of microseconds specified by micros.
        /// Delays of 100 microseconds or less use busy waits.
        /// </summary>
        /// <param name="microSecs">the number of microseconds to sleep</param>
        /// <returns>Returns the actual length of the delay in microseconds.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioDelay")]
        public static extern uint GpioDelay(uint microSecs);

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
        /// <param name="timeType">0 (relative), 1 (absolute)</param>
        /// <param name="seconds">seconds to sleep</param>
        /// <param name="microseconds">microseconds to sleep</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_TIMETYPE, PI_BAD_SECONDS, or PI_BAD_MICROS.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSleep")]
        public static extern ResultCode GpioSleep(TimeType timeType, int seconds, int microseconds);

        /// <summary>
        /// Delay execution for a given number of seconds
        ///
        /// </summary>
        /// <param name="seconds">the number of seconds to sleep</param>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "time_sleep")]
        public static extern void TimeSleep(double seconds);
    }
}
