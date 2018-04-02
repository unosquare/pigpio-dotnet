namespace Unosquare.PiGpio.Samples.Workbench
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Swan;

    internal class Timers : WorkbenchItemBase
    {
        private Timer CurrentTimer = null;
        private ManualResetEvent TimerTicked;
        private int RemainingTicks = 30;

        public Timers(bool isEnabled) : base(isEnabled) { }

        protected override void Setup()
        {
            RemainingTicks = 30;
            TimerTicked = new ManualResetEvent(false);
            CurrentTimer = Board.Timing.StartTimer(500, () => 
            {
                RemainingTicks--;
                TimerTicked.Set();
            });
        }

        protected override void DoBackgroundWork(CancellationToken ct)
        {
            while (ct.IsCancellationRequested == false)
            {
                if (TimerTicked.WaitOne(50))
                {
                    $"Timer Ticked. Remaining Ticks: {RemainingTicks}".Info(Name);
                    TimerTicked.Reset();
                }

                if (RemainingTicks <= 0)
                    break;
            }
        }

        protected override void Cleanup()
        {
            CurrentTimer.Dispose();
            TimerTicked.Dispose();
        }
    }
}
