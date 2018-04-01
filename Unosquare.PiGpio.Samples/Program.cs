namespace Unosquare.PiGpio.Samples
{
    using System;
    using System.Threading;
    using NativeEnums;
    using NativeMethods;
    using ManagedModel;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"{nameof(Board.BoardType),-22}: {Board.BoardType}");
            Console.WriteLine($"{nameof(Board.HardwareRevision),-22}: {Board.HardwareRevision}");
            Console.WriteLine($"{nameof(Board.IsAvailable),-22}: {Board.IsAvailable}");
            Console.WriteLine($"{nameof(Board.LibraryVersion),-22}: {Board.LibraryVersion}");

            Console.WriteLine($"{nameof(Board.Timing.Timestamp),-22}: {Board.Timing.Timestamp}");
            Console.WriteLine($"{nameof(Board.Timing.TimestampSeconds),-22}: {Board.Timing.TimestampSeconds}");
            Console.WriteLine($"{nameof(Board.Timing.TimestampMicroseconds),-22}: {Board.Timing.TimestampMicroseconds}");

            var timer = default(Timer);
            {
                var timerCyclesRemaining = 60;
                
                timer = Board.Timing.StartTimer(500, () =>
                {
                    Console.WriteLine($"Timer Remaining Cycles: {timerCyclesRemaining}");
                    timerCyclesRemaining--;
                    if (timerCyclesRemaining <= 0)
                    {
                        Console.WriteLine("Timer cycles ended. No more timer messages should appear.");
                        timer.Dispose();
                    }
                });
            }

            var thread = default(Thread);
            {
                var threadPin = Board.Pins[18];
                thread = Board.Timing.StartThread(() =>
                {
                    var startTime = Board.Timing.TimestampSeconds;
                    while (true)
                    {
                        threadPin.Value = !threadPin.Value;
                        Board.Timing.Sleep(250);
                        if (Board.Timing.TimestampSeconds - startTime >= 3d)
                            break;
                    }

                    threadPin.Value = false;

                    try
                    {
                        var minPulseWidth = GpioPinServoService.PulseWidthMin + 50;
                        var maxPulseWidth = GpioPinServoService.PulseWidthMax;

                        var pulseWidth = minPulseWidth+ 500;
                        var pulseDelta = 10;
                        while (true)
                        {
                            var flipDelta = false;

                            if (pulseWidth >= maxPulseWidth)
                            {
                                pulseWidth = maxPulseWidth;
                                flipDelta = true;
                            }
                            else if (pulseWidth <= minPulseWidth)
                            {
                                pulseWidth = minPulseWidth;
                                flipDelta = true;
                            }

                            threadPin.Servo.PulseWidth = pulseWidth;

                            if (flipDelta)
                            {
                                Console.WriteLine($"Pulse Witdh is now {threadPin.Servo.PulseWidth}, {threadPin.Servo.PositionPercent:p}");
                                pulseDelta *= -1;
                                Board.Timing.Sleep(500);
                            }

                            pulseWidth = pulseWidth + pulseDelta;
                            Board.Timing.SleepMicros(5000);
                        }
                    }
                    catch (ThreadAbortException tabex)
                    {
                        Console.WriteLine($"Thread Terminates: {tabex}");
                    }
                });
            }

            Console.WriteLine("Press any key to continue . . .");
            Console.ReadKey(intercept: true);
            Console.WriteLine();
            thread.Abort();
            // var pin = Board.Pins[18];
            // pin.Servo.PositionPercent = 0.5;
        }
    }
}
