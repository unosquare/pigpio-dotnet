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
            var sharedFiles = Files.FileList("/home/pi/pigpio-dotnet/*.exe");
            foreach (var entry in sharedFiles)
            {
                Console.WriteLine(entry);
            }

            var pin = SystemGpio.Bcm18;
            IO.GpioSetMode(pin, PortMode.Output);
            IO.GpioSetAlertFunc((UserGpio)pin, OnPinChange);

            while (true)
            {
                IO.GpioWrite(pin, true);
                Thread.Sleep(500);
                IO.GpioWrite(pin, false);
                Thread.Sleep(500);
            }
        }

        static void OnPinChange(UserGpio userGpio, LevelChange levelChange, uint tick)
        {
            Console.WriteLine($"{userGpio} detected {levelChange}. Elapsed: {tick} uS.");
        }
    }
}
