namespace Unosquare.PiGpio.Workbench.Runners
{
    using ManagedModel;
    using Swan.Threading;
    using System.Threading;

    internal class LedBlinking : RunnerBase
    {
        private GpioPin _pin;

        public LedBlinking(bool isEnabled)
            : base(isEnabled) { }

        protected override void OnSetup()
        {
            _pin = Board.Pins[17];
        }

        protected override void DoBackgroundWork(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _pin.Value = !_pin.Value;
                Board.Timing.Sleep(500);
            }
        }

        protected override void Cleanup()
        {
            _pin.Value = false;
            Board.Timing.Sleep(200);
        }
    }
}
