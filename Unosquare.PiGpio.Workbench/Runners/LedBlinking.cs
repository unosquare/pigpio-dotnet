namespace Unosquare.PiGpio.Workbench.Runners
{
    using System.Threading;
    using ManagedModel;

    internal class LedBlinking : RunnerBase
    {
        private GpioPin Pin = null;

        public LedBlinking(bool isEnabled)
            : base(isEnabled) { }

        protected override void Setup()
        {
            Pin = Board.Pins[17];
        }

        protected override void DoBackgroundWork(CancellationToken ct)
        {
            while (ct.IsCancellationRequested == false)
            {
                Pin.Value = !Pin.Value;
                Board.Timing.Sleep(500);
            }
        }

        protected override void Cleanup()
        {
            Pin.Value = false;
            Board.Timing.Sleep(200);
        }
    }
}
