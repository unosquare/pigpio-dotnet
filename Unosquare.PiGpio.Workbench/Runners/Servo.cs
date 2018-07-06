namespace Unosquare.PiGpio.Workbench.Runners
{
    using ManagedModel;
    using Swan;
    using System.Threading;

    internal class Servo : RunnerBase
    {
        private GpioPin _pin;
        private int MinPulseWidth = GpioPinServoService.PulseWidthMin + 50;
        private int MaxPulseWidth = GpioPinServoService.PulseWidthMax;
        private int InitialPulseWidth = GpioPinServoService.PulseWidthMin + 500;

        public Servo(bool isEnabled)
            : base(isEnabled) { }

        protected override void Setup()
        {
            _pin = Board.Pins[18];
        }

        protected override void DoBackgroundWork(CancellationToken ct)
        {
            var pulseWidth = InitialPulseWidth;
            var pulseDelta = 10;
            while (ct.IsCancellationRequested == false)
            {
                var flipDelta = false;

                if (pulseWidth >= MaxPulseWidth)
                {
                    pulseWidth = MaxPulseWidth;
                    flipDelta = true;
                }
                else if (pulseWidth <= MinPulseWidth)
                {
                    pulseWidth = MinPulseWidth;
                    flipDelta = true;
                }

                _pin.Servo.PulseWidth = pulseWidth;

                if (flipDelta)
                {
                    $"Pulse Witdh is now {_pin.Servo.PulseWidth}, {_pin.Servo.PositionPercent:p}".Info(Name);
                    pulseDelta *= -1;
                    Board.Timing.Sleep(500);
                }

                pulseWidth = pulseWidth + pulseDelta;
                Board.Timing.SleepMicros(5000);
            }
        }

        protected override void Cleanup()
        {
            _pin.Servo.PulseWidth = InitialPulseWidth;
            Board.Timing.Sleep(500); // give it some time to let the servo move to the requested position
        }
    }
}
