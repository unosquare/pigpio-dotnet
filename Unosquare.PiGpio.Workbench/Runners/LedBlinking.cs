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
            _pin = Board.Pins[16];
        }

        protected override void DoBackgroundWork(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _pin.Value = !_pin.Value;
                Thread.Sleep(500);
            }
        }

        protected override void Cleanup()
        {
            _pin.Value = !_pin.Value;
            Thread.Sleep(200);
        }
    }
}
