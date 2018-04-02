namespace Unosquare.PiGpio.Samples.Workbench
{
    using System.Threading;
    using ManagedModel;

    internal class LedBlinking : WorkbenchItemBase
    {
        private GpioPin Pin = null;

        public LedBlinking(bool isEnabled) : base(isEnabled) { }

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
