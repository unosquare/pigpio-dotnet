namespace Unosquare.PiGpio
{
    using System;
    using ManagedModel;
    using NativeEnums;
    using RaspberryIO.Abstractions;
    using Swan.DependencyInjection;
    using Unosquare.PiGpio.NativeMethods.InProcess.DllImports;
    using Unosquare.PiGpio.NativeMethods.Interfaces;

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
            var utilitiesService = DependencyContainer.Current.Resolve<IUtilityService>();

            // Populate basic information
            HardwareRevision = utilitiesService.GpioHardwareRevision();
            LibraryVersion = utilitiesService.GpioVersion();
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
        public static bool IsAvailable { get; internal set; }

        /// <summary>
        /// Gets the hardware revision number.
        /// </summary>
        public static long HardwareRevision { get; }

        /// <summary>
        /// Gets the hardware revision as the generic type.
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
