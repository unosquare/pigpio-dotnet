namespace Unosquare.PiGpio.Samples
{
    using Swan;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Workbench;

    class Program
    {
        public static void Main(string[] args)
        {
            Terminal.Settings.DisplayLoggingMessageType = (LogMessageType)int.MaxValue;
            var workbenchItems = new List<WorkbenchItemBase>
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

            $"Program Finished".Info(nameof(Program));
            Terminal.Flush(TimeSpan.FromSeconds(2));
            Board.Release();
        }
    }
}
