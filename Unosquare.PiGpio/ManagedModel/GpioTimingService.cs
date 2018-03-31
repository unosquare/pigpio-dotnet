namespace Unosquare.PiGpio.ManagedModel
{
    using NativeMethods;
    using NativeTypes;
    using System;
    using System.Threading;

    /// <summary>
    /// Provides timing, date and delay functions.
    /// Also provides access to registered timers.
    /// </summary>
    public class GpioTimingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpioTimingService"/> class.
        /// </summary>
        internal GpioTimingService()
        {
            Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            Timers = new GpioTimerCollection();
        }

        /// <summary>
        /// Provides access to the registiered timers.
        /// Timers execute a block of code every elapsed period of milliseconds.
        /// </summary>
        public GpioTimerCollection Timers { get; }

        /// <summary>
        /// Gets the Linux epoch (Jan 1, 1970) in UTC.
        /// </summary>
        public DateTime Epoch { get; }

        /// <summary>
        /// Gets the number of seconds elapsed since Jan 1, 1970.
        /// </summary>
        public double SecondsSinceEpoch => Utilities.TimeTime();

        /// <summary>
        /// Gets the elapsed time since Jan 1, 1970.
        /// </summary>
        public TimeSpan ElapsedSinceEpoch => TimeSpan.FromSeconds(SecondsSinceEpoch);

        /// <summary>
        /// Sleeps for the given amount of microseconds.
        /// Waits of 100 microseconds or less use busy waits.
        /// Returns the real elapsed microseconds.
        /// </summary>
        /// <param name="microsecs">The micro seconds.</param>
        /// <returns>Returns the real elapsed microseconds.</returns>
        public long SleepMicros(long microsecs) => GpioThread.SleepMicros(microsecs);

        /// <summary>
        /// Sleeps for the specified milliseconds.
        /// </summary>
        /// <param name="millisecs">The milliseconds to sleep for.</param>
        public void Sleep(double millisecs) => GpioThread.Sleep(millisecs);

        /// <summary>
        /// Sleeps for the specified time span.
        /// </summary>
        /// <param name="timeSpan">The time span to sleep for.</param>
        public void Sleep(TimeSpan timeSpan) => GpioThread.Sleep(timeSpan);

        /// <summary>
        /// Gets a timestamp since boot time in microceconds.
        /// Use the <see cref="EndBenchmark"/> to get the number of elapsed microseconds.
        /// The ticks wrap around every ~72 minutes.
        /// </summary>
        /// <returns>A timestamp in microseconds sin boot time.</returns>
        public long StartBenchmark() => Utilities.GpioTick();

        /// <summary>
        /// Gets the difference between a timestamp obtained in <see cref="StartBenchmark"/>
        /// and the current timestamp. Useful to benchmark operations.
        /// </summary>
        /// <param name="startBenchmark">The start benchmark.</param>
        /// <returns>The elapsed time between the start timestamp and the current timestamp.</returns>
        public long EndBenchmark(long startBenchmark)
        {
            var currentTicks = Convert.ToInt64(Utilities.GpioTick());
            if (currentTicks < startBenchmark)
                return uint.MaxValue - startBenchmark + currentTicks;

            return currentTicks - startBenchmark;
        }

        /// <summary>
        /// Shortcut method to start a POSIX thread.
        /// Starts the thread automatically
        /// </summary>
        /// <param name="doWork">The do work.</param>
        /// <returns>A reference to the POSIX thread.</returns>
        public GpioThread StartThread(ThreadStart doWork)
        {
            var thread = new GpioThread(doWork);
            thread.Start();
            return thread;
        }

        /// <summary>
        /// Starts the next available timer with the given period and callback.
        /// </summary>
        /// <param name="periodMilliseconds">The period milliseconds. From 10 to 60000</param>
        /// <param name="callback">The callback.</param>
        /// <returns>A reference to a timer.</returns>
        /// <exception cref="InvalidOperationException">No more timers are available. Please stop at least one of them and retry this operation.</exception>
        public GpioTimer StartTimer(int periodMilliseconds, PiGpioTimerDelegate callback)
        {
            foreach (var t in Timers)
            {
                if (t.Value.IsRunning == false)
                {
                    t.Value.Start(periodMilliseconds, callback);
                    return t.Value;
                }
            }

            throw new InvalidOperationException(
                "No more timers are available. Please stop at least one of them and retry this operation.");
        }
    }
}
