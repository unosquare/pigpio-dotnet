using Unosquare.RaspberryIO.Abstractions;

namespace Unosquare.PiGpio
{
    using ManagedModel;
    using NativeEnums;
    using NativeMethods;
    using System;

    /// <summary>
    /// Represents the Raspberry Pi Board and provides
    /// access to all GPIO initialization and functionality.
    /// </summary>
    public static class Board
    {
        /// <summary>
        /// Initializes static members of the <see cref="Board"/> class.
        /// </summary>
        static Board()
        {
            try
            {
                // Retrieve internal configuration
                var config = (int)Setup.GpioCfgGetInternals();

                // config = config.ApplyBits(false, 3, 2, 1, 0); // Clear debug flags
                /*
                MJA
                If you use Visual Studio 2019 together with VSMonoDebugger and X11 remote debugging,
                you need to enable the next line, otherwise Mono will catch the Signals and stop
                debugging immediately although native started program will work ok
                */
                config = config | (int)ConfigFlags.NoSignalHandler;
                Setup.GpioCfgSetInternals((ConfigFlags)config);

                var initResultCode = Setup.GpioInitialise();

                /*
                MJA
                Setup.GpioInitialise() gives back value greater than zero if it has success.
                More in detail:
                The given back number is the version of the library version you use on RasPi.
                Therefore a greater or equal comparison would make potentially more sense.
                */
                // IsAvailable = initResultCode == ResultCode.Ok;
                IsAvailable = initResultCode >= ResultCode.Ok;

                // You will need to compile libgpio.h adding
                // #define EMBEDDED_IN_VM
                // Also, remove the atexit( ... call in pigpio.c
                // So there is no output or signal handling
            }
            catch { IsAvailable = false; }

            // Populate basic information
            HardwareRevision = Utilities.GpioHardwareRevision();
            LibraryVersion = Utilities.GpioVersion();
            BoardRevision = Constants.GetBoardRevision(HardwareRevision);
            BoardType = Constants.GetBoardType(HardwareRevision);

            // Instantiate collections and services.
            Pins = new GpioPinCollection();
            GpioPads = new GpioPadCollection();
            BankA = new GpioBank(1);
            BankB = new GpioBank(2);
            Timing = new BoardTimingService();
            Peripherals = new BoardPeripheralsService();
            Waves = new BoardWaveService();
        }

        /// <summary>
        /// Gets a value indicating whether the board has been initialized.
        /// </summary>
        public static bool IsAvailable { get; private set; }

        /// <summary>
        /// Gets the hardware revision number.
        /// </summary>
        public static long HardwareRevision { get; }

        /// <summary>
        /// Gets the hardware revision as the generic type
        /// </summary>
        public static BoardRevision BoardRevision { get; }

        /// <summary>
        /// Gets the library version number.
        /// </summary>
        public static long LibraryVersion { get; }

        /// <summary>
        /// Gets the type of the board. See the <see cref="NativeEnums.BoardType"/> enumeration.
        /// </summary>
        public static BoardType BoardType { get; }

        /// <summary>
        /// Provides access to the pin collection.
        /// </summary>
        public static GpioPinCollection Pins { get; }

        /// <summary>
        /// Provides access to the electrical pads.
        /// </summary>
        public static GpioPadCollection GpioPads { get; }

        /// <summary>
        /// Provides access to GPIO bank 1 (or A)
        /// consisting of GPIO 0 to 31.
        /// </summary>
        public static GpioBank BankA { get; }

        /// <summary>
        /// Provides access to GPIO bank 2 (or B)
        /// consisting of GPIO 32 to 53.
        /// </summary>
        public static GpioBank BankB { get; }

        /// <summary>
        /// Provides timing and date functions.
        /// </summary>
        public static BoardTimingService Timing { get; }

        /// <summary>
        /// Provides peripheral communication buses available to the board.
        /// </summary>
        public static BoardPeripheralsService Peripherals { get; }

        /// <summary>
        /// Provides a service to build and send waveforms
        /// with precisions of a few microseconds ~5us per pulse.
        /// </summary>
        public static BoardWaveService Waves { get; }

        /// <summary>
        /// Releases board resources.
        /// </summary>
        public static void Release()
        {
            IsAvailable = false;
            Setup.GpioTerminate();
            Console.CursorVisible = true;
        }
    }
}
