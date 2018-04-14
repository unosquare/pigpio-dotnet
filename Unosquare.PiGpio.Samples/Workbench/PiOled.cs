using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unosquare.PiGpio.Peripherals;
using Unosquare.Swan;

namespace Unosquare.PiGpio.Samples.Workbench
{
    internal class PiOled : WorkbenchItemBase
    {
        private OledDisplaySSD1306 Display;

        internal PiOled(bool isEnabled) : base(isEnabled)
        {
            // placeholder
        }

        protected override void Setup()
        {
            Display = new OledDisplaySSD1306(OledDisplaySSD1306.DisplayModel.Display128x32);
        }

        protected override void DoBackgroundWork(CancellationToken ct)
        {
            var currentX = 0;
            var currentY = 0;
            var currentVal = true;
            var sw = new Stopwatch();
            var frameCount = 0d;

            sw.Start();
            while (!ct.IsCancellationRequested)
            {

                Display[currentX, currentY] = currentVal;
                Display.Render();

                currentX++;
                frameCount += 1;

                if (currentX >= Display.Width)
                {
                    var elapsedSeconds = sw.Elapsed.TotalSeconds;
                    var framesPerSecond = frameCount / elapsedSeconds;
                    $"Contrast: {Display.Contrast}. X: {currentX} Y: {currentY} Frames: {frameCount:0} Elapsed {elapsedSeconds:0.000} FPS: {framesPerSecond:0.000}".Info(Name);
                    sw.Restart();
                    frameCount = 0;
                    currentX = 0;
                    currentY += 1;
                }

                if (currentY >= Display.Height)
                {
                    currentY = 0;
                    currentVal = !currentVal;
                }

                // Thread.Sleep(15);
            }
        }

        protected override void Cleanup()
        {
            Display.IsActive = false;
            // Display.Contrast = 0;
        }
    }
}
