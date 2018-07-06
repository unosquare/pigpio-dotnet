namespace Unosquare.PiGpio.Workbench.Runners
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Swan;

    internal abstract class RunnerBase
    {
        private Thread _worker;
        private CancellationTokenSource _cancelTokenSource;
        private ManualResetEvent _workFinished;

        protected RunnerBase(bool isEnabled)
        {
            Name = GetType().Name;
            IsEnabled = isEnabled;
        }

        public List<string> ErrorMessages { get; } = new List<string>();

        public string Name { get; }

        public bool IsRunning { get; private set; }

        public bool IsEnabled { get; }

        public virtual void Start()
        {
            if (IsEnabled == false)
                return;

            $"Start Requested".Debug(Name);
            _cancelTokenSource = new CancellationTokenSource();
            _workFinished = new ManualResetEvent(false);
            _worker = new Thread(() =>
            {
                _workFinished.Reset();
                IsRunning = true;
                try
                {
                    Setup();
                    DoBackgroundWork(_cancelTokenSource.Token);
                }
                catch (ThreadAbortException)
                {
                    $"{nameof(ThreadAbortException)} caught.".Warn(Name);
                }
                catch (Exception ex)
                {
                    $"{ex.GetType()}: {ex.Message}\r\n{ex.StackTrace}".Error(Name);
                }
                finally
                {
                    Cleanup();
                    _workFinished?.Set();
                    IsRunning = false;
                    "Stopped Completely".Debug(Name);
                }
            })
            {
                IsBackground = true,
                Name = $"{Name}Thread",
            };

            _worker.Start();
        }

        public virtual void Stop()
        {
            if (IsEnabled == false)
                return;

            if (IsRunning == false)
                return;

            $"Stop Requested".Debug(Name);
            _cancelTokenSource.Cancel();
            var waitRetries = 5;
            while (waitRetries >= 1)
            {
                if (_workFinished.WaitOne(250))
                {
                    waitRetries = -1;
                    break;
                }

                waitRetries--;
            }

            if (waitRetries < 0)
            {
                "Workbench stopped gracefully".Debug(Name);
            }
            else
            {
                "Did not respond to stop request. Aborting thread and waiting . . .".Warn(Name);
                _worker.Abort();

                if (_workFinished.WaitOne(5000) == false)
                    "Waited and no response. Worker might have been left in an inconsistent state.".Error(Name);
                else
                    "Waited for worker and it finally responded (OK).".Debug(Name);
            }

            _workFinished.Dispose();
            _workFinished = null;
        }

        protected virtual void Setup()
        {
            // empty
        }

        protected virtual void Cleanup()
        {
            // empty
        }

        protected abstract void DoBackgroundWork(CancellationToken ct);
    }
}
