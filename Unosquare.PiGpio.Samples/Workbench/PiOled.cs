namespace Unosquare.PiGpio.Samples.Workbench
{
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Threading;
    using Unosquare.PiGpio.Peripherals;
    using Unosquare.Swan;

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
            var cycleSw = new Stopwatch();
            var frameCount = 0d;
            var cycleCount = 0;

            var bitmap = new Bitmap(Display.Width, Display.Height, PixelFormat.Format32bppArgb);
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var font = new Font(FontFamily.GenericMonospace, 7);
            $"Selected font is: {font.Name}".Info(Name);

            var fontBrush = Brushes.White;
            var graphicPen = Pens.White;
            var graphics = Graphics.FromImage(bitmap);
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.SmoothingMode = SmoothingMode.None;

            sw.Start();
            cycleSw.Start();
            while (!ct.IsCancellationRequested)
            {
                cycleSw.Restart();
                graphics.Clear(Color.Black);
                graphics.DrawString($"X: {currentX,3}  Y: {currentY,3}", font, fontBrush, 2, 2);
                graphics.DrawString($"Cycles: {cycleCount,6}", font, fontBrush, 2, 13);
                graphics.DrawEllipse(graphicPen, currentX, 24, 6, 6);
                graphics.Flush();

                Display.LoadBitmap(bitmap, 0, 0);
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

                Board.Timing.Sleep(40 - cycleSw.ElapsedMilliseconds);
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
