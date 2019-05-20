namespace Unosquare.PiGpio.Workbench
{
    using RaspberryIO;
    using Runners;
    using Swan;
    using Swan.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Program
    {
        public static void Main()
        {
            Terminal.Settings.DisplayLoggingMessageType = (LogMessageType)int.MaxValue;
            Pi.Init<BootstrapPiGpio>();

            var workbenchItems = new List<RunnerBase>
            {
                new BoardInfo(true),
                new LedBlinking(false),
                new Servo(false),
                new Timers(false),
                new ButtonInterrupts(false),
                new DhtSensor(false),
                new Mpu6050(false),
            };

            $"Enabled Workbench Items: {workbenchItems.Count(wbi => wbi.IsEnabled)}".Info(nameof(Program));
            foreach (var wbi in workbenchItems)
                wbi.Start();

            Terminal.ReadKey(intercept: true, disableLocking: true);

            foreach (var wbi in workbenchItems)
                wbi.Stop();

            "Program Finished".Info(nameof(Program));
            Terminal.Flush(TimeSpan.FromSeconds(2));
            Board.Release();
        }
    }
}
