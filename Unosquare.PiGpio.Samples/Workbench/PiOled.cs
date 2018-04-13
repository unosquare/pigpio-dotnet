using System;
using System.Collections.Generic;
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
            while (!ct.IsCancellationRequested)
            {

                Display[currentX, currentY] = currentVal;
                Display.Render();

                currentX++;
                if (currentX >= Display.Width)
                {
                    currentX = 0;
                    currentY += 1;
                    $"Contrast: {Display.Contrast}. X: {currentX} Y: {currentY}".Info(Name);
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
            base.Cleanup();
        }
    }
}
