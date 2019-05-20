namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;
    using System.Threading;
    using RaspberryIO.Abstractions;

    /// <summary>
    /// Provides timing, date and delay functions.
    /// Also provides access to registered timers.
    /// </summary>
    public class BoardTimingService : ITiming
    {
        private const long SecondsToMicrosFactor = 1000000L;
        private const long MillisToMicrosFactor = 1000L;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardTimingService"/> class.
        /// </summary>
        internal BoardTimingService()
        {
            Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        /// <summary>
        /// Gets the Linux Epoch (Jan 1, 1970) in UTC.
        /// </summary>
        public DateTime Epoch { get; }

        /// <summary>
        /// Gets the timestamp tick.
        /// Useful to calculate offsets in Alerts or ISR callbacks.
        /// </summary>
        public uint TimestampTick => Utilities.GpioTick();

        /// <summary>
        /// Gets the number of seconds elapsed since the Epoc (Jan 1, 1970).
        /// </summary>
        public double TimestampSeconds => Utilities.TimeTime();

        /// <summary>
        /// Gets a timestamp since Jan 1, 1970 in microseconds.
        /// </summary>
        public long TimestampMicroseconds => EllapsedMicroseconds(TimeType.Absolute);

        /// <summary>
        /// Gets the elapsed time since the Epoc (Jan 1, 1970).
        /// </summary>
        public TimeSpan Timestamp => TimeSpan.FromSeconds(TimestampSeconds);

        /// <inheritdoc />
        public uint Milliseconds => Convert.ToUInt32(EllapsedMicroseconds(TimeType.Relative) / MillisToMicrosFactor);

        /// <inheritdoc />
        public uint Microseconds => Convert.ToUInt32(EllapsedMicroseconds(TimeType.Relative));

        /// <summary>
        /// Sleeps for the given amount of microseconds.
        /// Waits of 100 microseconds or less use busy waits.
        /// Returns the real elapsed microseconds.
        /// </summary>
        /// <param name="microsecs">The micro seconds.</param>
        /// <returns>Returns the real elapsed microseconds.</returns>
        public long SleepMicros(long microsecs)
        {
            if (microsecs <= 0)
                return 0L;

            if (microsecs <= uint.MaxValue)
                return Threads.GpioDelay(Convert.ToUInt32(microsecs));

            var componentSeconds = microsecs / SecondsToMicrosFactor;
            var componentMicrosecs = microsecs % SecondsToMicrosFactor;

            if (componentSeconds <= int.MaxValue && componentMicrosecs <= int.MaxValue)
            {
                BoardException.ValidateResult(
                    Threads.GpioSleep(
                        TimeType.Relative,
                        Convert.ToInt32(componentSeconds),
                        Convert.ToInt32(componentMicrosecs)));

                return microsecs;
            }

            Threads.TimeSleep(componentSeconds + (componentMicrosecs / (double)SecondsToMicrosFactor));
            return microsecs;
        }

        /// <summary>
        /// Sleeps for the specified milliseconds.
        /// </summary>
        /// <param name="millis">The milliseconds to sleep for.</param>
        public void Sleep(long millis) =>
            SleepMicros(millis * MillisToMicrosFactor);

        /// <summary>
        /// Sleeps for the specified time span.
        /// </summary>
        /// <param name="timeSpan">The time span to sleep for.</param>
        public void Sleep(TimeSpan timeSpan) =>
            Sleep(Convert.ToInt64(timeSpan.TotalMilliseconds));

        /// <summary>
        /// Shortcut method to start a thread.
        /// It runs the thread automatically.
        /// </summary>
        /// <param name="doWork">The do work.</param>
        /// <param name="threadName">Name of the thread.</param>
        /// <returns>
        /// A reference to the thread object.
        /// </returns>
        public Thread StartThread(Action doWork, string threadName)
        {
            var thread = new Thread(() => { doWork?.Invoke(); })
            {
                IsBackground = true,
            };

            if (string.IsNullOrWhiteSpace(threadName) == false)
                thread.Name = threadName;

            thread.Start();
            return thread;
        }

        /// <summary>
        /// Shortcut method to start a thread.
        /// It runs the thread automatically.
        /// </summary>
        /// <param name="doWork">The do work.</param>
        /// <returns>
        /// A reference to the thread object.
        /// </returns>
        public Thread StartThread(Action doWork) => StartThread(doWork, null);

        /// <summary>
        /// Starts a timer that executes a block of code with the given period.
        /// </summary>
        /// <param name="periodMilliseconds">The period in milliseconds.</param>
        /// <param name="callback">The callback.</param>
        /// <returns>A reference to a timer.</returns>
        public Timer StartTimer(int periodMilliseconds, Action callback)
        {
            return new Timer(s => { callback?.Invoke(); }, this, 0, periodMilliseconds);
        }

        /// <inheritdoc />
        public void SleepMilliseconds(uint millis) =>
            Sleep(millis);

        /// <inheritdoc />
        public void SleepMicroseconds(uint micros) =>
            SleepMicros(micros);

        private long EllapsedMicroseconds(TimeType type)
        {
            BoardException.ValidateResult(
                Utilities.GpioTime(type, out var seconds, out var microseconds));

            return (seconds * SecondsToMicrosFactor) + microseconds;
        }
    }
}
