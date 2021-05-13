namespace Unosquare.PiGpio
{
    using RaspberryIO.Abstractions;
    using Swan.DependencyInjection;
    using System;
    using Unosquare.PiGpio.NativeMethods.Interfaces;

    /// <summary>
    /// Use this class to access threading methods using interop.
    /// </summary>
    /// <seealso cref="IThreading" />
    public class PigpioThreading : IThreading
    {
        private readonly object _lock = new object();
        private readonly IThreadsService _threadsService;
        private UIntPtr _currentThread;

        /// <summary>
        /// 
        /// </summary>
        public PigpioThreading()
        {
            _threadsService = DependencyContainer.Current.Resolve<IThreadsService>();
        }

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
                _threadsService.GpioStartThread(w => worker(w), userData));
        }

        /// <inheritdoc />
        public void StopThreadEx(UIntPtr handle)
        {
            if (handle == UIntPtr.Zero)
                return;

            _threadsService.GpioStopThread(handle);
        }
    }
}
