namespace Unosquare.PiGpio
{
    using ManagedModel;
    using NativeEnums;
    using NativeMethods;

    /// <summary>
    /// Represents the Raspberry Pi Board and provides
    /// access to all GPIO initialization and functionality
    /// </summary>
    public static class Board
    {
        private static readonly object SyncLock = new object();

        /// <summary>
        /// Initializes static members of the <see cref="Board"/> class.
        /// </summary>
        static Board()
        {
            try
            {
                var config = Setup.GpioCfgGetInternals();

                // config &= ~(ConfigFlags.DebugLevel0 | ConfigFlags.DebugLevel1 | ConfigFlags.DebugLevel2 | ConfigFlags.DebugLevel3);
                // Setup.GpioCfgSetInternals(config | ConfigFlags.NoSignalHandler);
                var initResultCode = Setup.GpioInitialise();
                IsAvailable = initResultCode == ResultCode.Ok;
            }
            catch { IsAvailable = false; }

            // Populate basic information
            HardwareRevision = Utilities.GpioHardwareRevision();
            LibraryVersion = Utilities.GpioVersion();
            BoardType = Constants.GetBoardType(HardwareRevision);

            // Instantiate collections and services.
            Pins = new GpioPinCollection();
            GpioPads = new GpioPadCollection();
            BankA = new GpioBank(1);
            BankB = new GpioBank(2);
            Timing = new GpioTimingService();
            Peripherals = new PeripheralsService();

            // AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            // {
            //    if (IsAvailable == false)
            //        return;
            //    Setup.GpioTerminate();
            // };
        }

        /// <summary>
        /// Gets a value indicating whether the board has been initialized
        /// </summary>
        public static bool IsAvailable { get; }

        /// <summary>
        /// Gets the hardware revision number.
        /// </summary>
        public static long HardwareRevision { get; }

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
        /// Provides timing and date functions
        /// </summary>
        public static GpioTimingService Timing { get; }

        /// <summary>
        /// Provides peripheral communication buses available to the board
        /// </summary>
        public static PeripheralsService Peripherals { get; }
    }
}
