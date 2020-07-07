namespace Unosquare.PiGpio.Workbench.Runners
{
    using ManagedModel;
    using NativeEnums;
    using Swan.Logging;
    using Swan.Threading;
    using System.Threading;

    internal class ButtonInterrupts : RunnerBase
    {
        private GpioPin _pin;
        private uint _previousTick;

        public ButtonInterrupts(bool isEnabled)
            : base(isEnabled) { }

        protected override void OnSetup()
        {
            _pin = Board.Pins[26];
            _pin.Direction = PinDirection.Input;
            _pin.PullMode = GpioPullMode.Off;
            _pin.Interrupts.EdgeDetection = EdgeDetection.EitherEdge;
            _pin.Interrupts.TimeoutMilliseconds = 1000;
            _previousTick = Board.Timing.TimestampTick;
            _pin.Interrupts.Start((pin, level, tick) =>
            {
                if (level == LevelChange.NoChange)
                {
                    $"Pin: {pin} | Level: {_pin.Value} | Tick: {tick} | Elapsed: {(tick - _previousTick) / 1000d:0.000} ms.".Info(Name);
                }
                else
                {
                    $"Pin: {pin} | Level: {level} | Tick: {tick} | Elapsed: {(tick - _previousTick) / 1000d:0.000} ms.".Info(Name);

                    if (level == LevelChange.LowToHigh)
                    {
                        Board.Pins[17].Write(1);
                    }
                }

                _previousTick = tick;
            });
        }

        protected override void DoBackgroundWork(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                Board.Timing.Sleep(50);
            }
        }

        protected override void Cleanup() => _pin.Interrupts.Stop();
    }
}
