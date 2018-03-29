namespace Unosquare.PiGpio.Samples
{
    using System;
    using System.Threading;
    using Unosquare.PiGpio.Enums;

    class Program
    {
        static void Main(string[] args)
        {
            NativeMethods.GpioInitialise();

            var sharedFiles = NativeMethods.FileList("/home/pi/pigpio-dotnet/*.exe");
            foreach (var entry in sharedFiles)
            {
                Console.WriteLine(entry);
            }

            var pin = SystemGpio.Bcm18;
            NativeMethods.GpioSetMode(pin, PortMode.Output);

            NativeMethods.GpioSetAlertFunc((UserGpio)pin, OnPinChange);

            while (true)
            {
                NativeMethods.GpioWrite(pin, true);
                Thread.Sleep(500);
                NativeMethods.GpioWrite(pin, false);
                Thread.Sleep(500);
            }

        }

        static void OnPinChange(UserGpio userGpio, LevelChange levelChange, uint tick)
        {
            Console.WriteLine($"{userGpio} detected {levelChange}. Elapsed: {tick} uS.");
        }
    }
}
