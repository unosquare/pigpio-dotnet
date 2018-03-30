namespace Unosquare.PiGpio
{
    using System;
    using System.Threading;
    using NativeMethods;

    /// <summary>
    /// Represents a background worker created by the pigpio library.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public sealed class PiGpioThread : IDisposable
    {
        private readonly object SyncLock = new object();
        private UIntPtr ThreadHandle = UIntPtr.Zero;
        private ThreadStart WorkerCallback = null;
        private ManualResetEvent HasFinished = new ManualResetEvent(false);
        private bool IsDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="PiGpioThread"/> class.
        /// </summary>
        /// <param name="threadStart">The thread start.</param>
        /// <exception cref="ArgumentNullException">threadStart</exception>
        public PiGpioThread(ThreadStart threadStart)
        {
            WorkerCallback = threadStart ?? throw new ArgumentNullException(nameof(threadStart));
        }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        public bool IsRunning => ThreadHandle != UIntPtr.Zero;

        /// <summary>
        /// Starts the thread.
        /// </summary>
        /// <exception cref="InvalidOperationException">When the thread was already started</exception>
        public void Start()
        {
            lock (SyncLock)
            {
                if (ThreadHandle != UIntPtr.Zero)
                    throw new InvalidOperationException($"Thread with handle '{ThreadHandle:X}' was already started.");

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
                if (ThreadHandle == UIntPtr.Zero)
                    return;

                try
                {
                    Threads.GpioStopThread(ThreadHandle);
                    HasFinished.WaitOne(1000);
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
                HasFinished.Set();
            }
        }
    }
}
