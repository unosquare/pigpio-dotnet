namespace Unosquare.PiGpio
{
    using System;
    using NativeMethods;
    using RaspberryIO.Abstractions;

    /// <summary>
    /// Use this class to access threading methods using interop.
    /// </summary>
    /// <seealso cref="IThreading" />
    public class Threading : IThreading
    {
        private readonly object _lock = new object();
        private UIntPtr _currentThread;

        /// <inheritdoc />
        public void StartThread(Action worker)
        {
            if (worker == null)
                throw new ArgumentNullException(nameof(worker));

            lock (_lock)
            {
                StopThread();
                _currentThread = StartThreadEx(x => worker(), UIntPtr.Zero);
            }
        }

        /// <summary>
        /// Stops a thread that was previously started with <see cref="StartThread(Action)"/>.
        /// </summary>
        public void StopThread()
        {
            if (_currentThread == UIntPtr.Zero)
                return;

            lock (_lock)
            {
                if (_currentThread == UIntPtr.Zero)
                    return;

                StopThreadEx(_currentThread);
                _currentThread = UIntPtr.Zero;
            }
        }

        /// <inheritdoc />
        public UIntPtr StartThreadEx(Action<UIntPtr> worker, UIntPtr userData)
        {
            if (worker == null)
                throw new ArgumentNullException(nameof(worker));

            return BoardException.ValidateResult(
                Threads.GpioStartThread(w => worker(w), userData));
        }

        /// <inheritdoc />
        public void StopThreadEx(UIntPtr handle)
        {
            if (handle == UIntPtr.Zero)
                return;

            Threads.GpioStopThread(handle);
        }
    }
}
