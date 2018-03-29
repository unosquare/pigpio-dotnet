namespace Unosquare.PiGpio.Samples
{
    using System;
    using System.Threading;
    using NativeEnums;
    using NativeMethods;

    class Program
    {
        static void Main(string[] args)
        {
            Setup.GpioInitialise();
            
            var sharedFiles = NativeMethods.Files.FileList("/home/pi/pigpio-dotnet/*.exe");
            foreach (var entry in sharedFiles)
            {
                Console.WriteLine(entry);
            }

            var pin = SystemGpio.Bcm18;
            Controller.GpioSetMode(pin, PortMode.Output);

            Controller.GpioSetAlertFunc((UserGpio)pin, OnPinChange);

            while (true)
            {
                Controller.GpioWrite(pin, true);
                Thread.Sleep(500);
                Controller.GpioWrite(pin, false);
                Thread.Sleep(500);
            }

        }

        static void OnPinChange(UserGpio userGpio, LevelChange levelChange, uint tick)
        {
            Console.WriteLine($"{userGpio} detected {levelChange}. Elapsed: {tick} uS.");
        }
    }
}
