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
        private static readonly Destructor StaticDestructor = new Destructor();
        private static readonly object SyncLock = new object();

        /// <summary>
        /// Initializes static members of the <see cref="Board"/> class.
        /// </summary>
        static Board()
        {
            try { IsAvailable = Setup.GpioInitialise() == ResultCode.Ok; }
            catch { IsAvailable = false; }

            Pins = new GpioPinCollection();
            Pads = new GpioPadCollection();
            BankA = new GpioBank(1);
            BankB = new GpioBank(2);
            Timing = new TimingService();
        }

        /// <summary>
        /// Gets a value indicating whether the board has been initialized
        /// </summary>
        public static bool IsAvailable { get; }

        /// <summary>
        /// Provides access to the pin collection.
        /// </summary>
        public static GpioPinCollection Pins { get; }

        /// <summary>
        /// Provides access to the electrical pads.
        /// </summary>
        public static GpioPadCollection Pads { get; }

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
        public static TimingService Timing { get; }

        /// <summary>
        /// Defines a static destructor with the sole purpose
        /// of calling a finalizer when the process ends.
        /// </summary>
        private sealed class Destructor
        {
            /// <summary>
            /// Finalizes an instance of the <see cref="Destructor"/> class.
            /// </summary>
            ~Destructor()
            {
                Setup.GpioTerminate();
            }
        }
    }
}
