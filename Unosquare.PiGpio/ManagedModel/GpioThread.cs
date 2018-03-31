namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;
    using System.Threading;

    /// <summary>
    /// Represents a background worker created by the pigpio library.
    /// This class also contains timing and delay mechanisms.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public sealed class GpioThread : IDisposable
    {
        #region Private Fields

        private readonly object SyncLock = new object();
        private UIntPtr ThreadHandle = UIntPtr.Zero;
        private ThreadStart WorkerCallback = null;
        private ManualResetEvent HasFinished = null;
        private bool IsDisposed = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GpioThread"/> class.
        /// </summary>
        /// <param name="threadStart">The thread start.</param>
        /// <exception cref="ArgumentNullException">threadStart</exception>
        public GpioThread(ThreadStart threadStart)
        {
            WorkerCallback = threadStart ?? throw new ArgumentNullException(nameof(threadStart));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the thread is running.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                lock (SyncLock)
                {
                    return ThreadHandle != UIntPtr.Zero;
                }
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Sleeps for the given amount of microseconds.
        /// Waits of 100 microseconds or less use busy waits.
        /// Returns the real elapsed microseconds.
        /// </summary>
        /// <param name="microsecs">The micro seconds.</param>
        /// <returns>Returns the real elapsed microseconds.</returns>
        public static long SleepMicros(long microsecs)
        {
            if (microsecs <= 0)
                return 0L;

            if (microsecs <= uint.MaxValue)
                return Threads.GpioDelay(Convert.ToUInt32(microsecs));

            var componentSeconds = microsecs / 1000000d;
            var componentMicrosecs = microsecs % 1000000d;

            if (componentSeconds <= int.MaxValue && componentMicrosecs <= int.MaxValue)
            {
                PiGpioException.ValidateResult(
                    Threads.GpioSleep(
                        TimeType.Relative,
                        Convert.ToInt32(componentSeconds),
                        Convert.ToInt32(componentMicrosecs)));

                return microsecs;
            }

            Threads.TimeSleep(componentSeconds);
            return microsecs;
        }

        /// <summary>
        /// Sleeps for the specified milliseconds.
        /// </summary>
        /// <param name="millisecs">The milliseconds to sleep for.</param>
        public static void Sleep(double millisecs)
        {
            if (millisecs <= 0d)
                return;

            var microsecs = Convert.ToInt64(millisecs * 1000d);
            SleepMicros(microsecs);
        }

        /// <summary>
        /// Sleeps for the specified time span.
        /// </summary>
        /// <param name="timeSpan">The time span to sleep for.</param>
        public static void Sleep(TimeSpan timeSpan)
        {
            if (timeSpan.TotalSeconds <= 0d)
                return;

            Threads.TimeSleep(timeSpan.TotalSeconds);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Starts the thread.
        /// </summary>
        /// <exception cref="InvalidOperationException">When the thread was already started</exception>
        public void Start()
        {
            lock (SyncLock)
            {
                if (HasFinished != null || ThreadHandle != UIntPtr.Zero)
                    throw new InvalidOperationException($"Thread with handle '{ThreadHandle:X}' was already started.");

                HasFinished = new ManualResetEvent(false);
                ThreadHandle = Threads.GpioStartThread(DoWork, UIntPtr.Zero);
            }
        }

        /// <summary>
        /// Stops the thread.
        /// </summary>
        public void Stop()
        {
            lock (SyncLock)
            {
                if (HasFinished == null || ThreadHandle == UIntPtr.Zero)
                    return;

                try
                {
                    Threads.GpioStopThread(ThreadHandle);
                    HasFinished?.WaitOne(1000);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    ThreadHandle = UIntPtr.Zero;
                }
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="alsoManaged"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool alsoManaged)
        {
            lock (SyncLock)
            {
                if (IsDisposed) return;

                if (alsoManaged)
                {
                    Stop();
                    HasFinished?.Set();
                    HasFinished?.Dispose();
                }

                HasFinished = null;
                IsDisposed = true;
            }
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="userData">The user data.</param>
        private void DoWork(UIntPtr userData)
        {
            try
            {
                WorkerCallback.Invoke();
            }
            catch
            {
                throw;
            }
            finally
            {
                ThreadHandle = UIntPtr.Zero;
                HasFinished?.Set();
            }
        }

        #endregion
    }
}
