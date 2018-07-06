namespace Unosquare.PiGpio.Workbench.Runners
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Threading;
    using Peripherals;
    using Swan;

    internal class PiOled : RunnerBase
    {
        private OledDisplaySSD1306 Display;

        internal PiOled(bool isEnabled)
            : base(isEnabled)
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
            var cycleSw = new Stopwatch();
            var frameCount = 0d;
            var cycleCount = 0;
            const double currentThreshold = 0.5d;

            var bitmap = new Bitmap(Display.Width, Display.Height, PixelFormat.Format32bppArgb);
            var graphicPen = Pens.White;
            var graphics = Graphics.FromImage(bitmap);
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.Default;
                graphics.SmoothingMode = SmoothingMode.Default;
            }

            // ProcessRunner.GetProcessOutputAsync("hostname", "-I").GetAwaiter().GetResult().RemoveControlChars().Trim().Truncate(15);
            const string address = "W.X.Y.Z";

            sw.Start();
            cycleSw.Start();
            while (!ct.IsCancellationRequested)
            {
                cycleSw.Restart();

                // Display.Render(
                //    $"X: {currentX,3}  Y: {currentY,3}",
                //    $"Cycles: {cycleCount,6} T {currentThreshold:p}",
                //    $"{DateTime.Now}",
                //    $"IP: {address} THIS IS SOME VERY LONG LINE",
                //    $"THIS SHOULD NOT BE SHOWN");
                graphics.Clear(Color.Black);
                Display.DrawText(bitmap,
                   graphics,
                   $"X: {currentX,3}  Y: {currentY,3}",
                   $"Cycles: {cycleCount,6} T {currentThreshold:p}",
                   $"{DateTime.Now}",
                   $"IP: {address}");
                graphics.DrawEllipse(graphicPen, currentX, 24, 6, 6);
                graphics.Flush();
                Display.LoadBitmap(bitmap, currentThreshold, 0, 0);
                Display[currentX, currentY] = true; // currentVal;
                Display.Render();

                currentX++;
                frameCount += 1;
                cycleCount += 1;

                if (currentX >= Display.Width)
                {
                    var elapsedSeconds = sw.Elapsed.TotalSeconds;
                    var framesPerSecond = frameCount / elapsedSeconds;
                    $"Contrast: {Display.Contrast}. X: {currentX} Y: {currentY} Frames: {cycleCount:0} Elapsed {elapsedSeconds:0.000} FPS: {framesPerSecond:0.000}".Info(Name);
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

                if (cycleSw.ElapsedMilliseconds > 40)
                    continue;

                // Board.Timing.Sleep(40 - cycleSw.ElapsedMilliseconds);
            }

            graphics.Dispose();
            bitmap.Dispose();
        }

        protected override void Cleanup()
        {
            Display.Dispose();
        }
    }
}
