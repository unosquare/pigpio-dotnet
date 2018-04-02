namespace Unosquare.PiGpio.Samples.Workbench
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Unosquare.Swan;

    internal abstract class WorkbenchItemBase
    {
        private Thread Worker = null;
        private CancellationTokenSource CancelTokenSource = null;
        private ManualResetEvent WorkFinished;

        protected WorkbenchItemBase(bool isEnabled)
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
            CancelTokenSource = new CancellationTokenSource();
            WorkFinished = new ManualResetEvent(false);
            Worker = new Thread(() =>
            {
                WorkFinished.Reset();
                IsRunning = true;
                try
                {
                    Setup();
                    DoBackgroundWork(CancelTokenSource.Token);
                }
                catch (ThreadAbortException)
                {
                    $"{nameof(ThreadAbortException)} caught.".Warn(Name);
                }
                catch (Exception ex)
                {
                    $"{ex.GetType()}: {ex.Message}".Error(Name);
                }
                finally
                {
                    Cleanup();
                    WorkFinished?.Set();
                    IsRunning = false;
                    $"Stopped Completely".Debug(Name);
                }
            })
            {
                IsBackground = true,
                Name = $"{Name}Thread",
            };

            Worker.Start();
        }

        public virtual void Stop()
        {
            if (IsEnabled == false)
                return;

            if (IsRunning == false)
                return;

            $"Stop Requested".Debug(Name);
            CancelTokenSource.Cancel();
            var waitRetries = 5;
            while (waitRetries >= 1)
            {
                if (WorkFinished.WaitOne(250))
                {
                    waitRetries = -1;
                    break;
                }

                waitRetries--;
            }

            if (waitRetries < 0)
            {
                $"Workbench stopped gracefully".Debug(Name);
            }
            else
            {
                $"Did not respond to stop request. Aborting thread and waiting . . .".Warn(Name);
                Worker.Abort();

                if (WorkFinished.WaitOne(5000) == false)
                    $"Waited and no response. Worker might have been left in an inconsistent state.".Error(Name);
                else
                    $"Waited for worker and it finally responded (OK).".Debug(Name);
            }

            WorkFinished.Dispose();
            WorkFinished = null;
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
