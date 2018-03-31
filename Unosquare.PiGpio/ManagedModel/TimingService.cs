namespace Unosquare.PiGpio.ManagedModel
{
    using NativeMethods;
    using System;

    /// <summary>
    /// Provides timing, date and delay functions.
    /// </summary>
    public class TimingService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimingService"/> class.
        /// </summary>
        internal TimingService()
        {
            Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

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
        public long SleepMicros(long microsecs) => PiGpioThread.SleepMicros(microsecs);

        /// <summary>
        /// Sleeps for the specified milliseconds.
        /// </summary>
        /// <param name="millisecs">The milliseconds to sleep for.</param>
        public void Sleep(double millisecs) => PiGpioThread.Sleep(millisecs);

        /// <summary>
        /// Sleeps for the specified time span.
        /// </summary>
        /// <param name="timeSpan">The time span to sleep for.</param>
        public void Sleep(TimeSpan timeSpan) => PiGpioThread.Sleep(timeSpan);
    }
}
