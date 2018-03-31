namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using NativeTypes;
    using System;

    /// <summary>
    /// Holds a reference to a GPIO predefined timer.
    /// </summary>
    public sealed class GpioTimer
    {
        private PiGpioTimerDelegate Callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpioTimer"/> class.
        /// </summary>
        /// <param name="timerId">The timer identifier.</param>
        internal GpioTimer(TimerId timerId)
        {
            TimerId = timerId;
        }

        /// <summary>
        /// Gets the timer identifier.
        /// </summary>
        public TimerId TimerId { get; }

        /// <summary>
        /// Gets a value indicating whether the timer is running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets the period in milliceonds.
        /// 10ms to 60000ms
        /// </summary>
        public int PeriodMilliseconds { get; private set; }

        /// <summary>
        /// Starts calling the specified callback every elapsed milliseconds.
        /// the period must be between 10ms and 60000ms
        /// </summary>
        /// <param name="periodMilliseconds">The period milliseconds. 10 to 60000.</param>
        /// <param name="callback">The callback.</param>
        public void Start(int periodMilliseconds, PiGpioTimerDelegate callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            PiGpioException.ValidateResult(
                Threads.GpioSetTimerFunc(TimerId, Convert.ToUInt32(periodMilliseconds), callback));
            Callback = callback;
            PeriodMilliseconds = periodMilliseconds;
            IsRunning = true;
        }

        /// <summary>
        /// Stops the callback from being called every elapsed milliseconds.
        /// </summary>
        public void Stop()
        {
            if (Callback == null)
                return;

            PiGpioException.ValidateResult(
                Threads.GpioSetTimerFunc(TimerId, Convert.ToUInt32(PeriodMilliseconds), null));
            Callback = null;
            IsRunning = false;
        }
    }
}
