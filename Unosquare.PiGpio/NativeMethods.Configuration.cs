namespace Unosquare.PiGpio
{
    using System;
    using System.Runtime.InteropServices;

    public static partial class NativeMethods
    {
        /// <summary>
        /// Configures pigpio to buffer cfgMillis milliseconds of GPIO samples.
        ///
        /// This function is only effective if called before [*gpioInitialise*].
        ///
        /// The default setting is 120 milliseconds.
        ///
        /// The intention is to allow for bursts of data and protection against
        /// other processes hogging cpu time.
        ///
        /// I haven't seen a process locked out for more than 100 milliseconds.
        ///
        /// Making the buffer bigger uses a LOT of memory at the more frequent
        /// sampling rates as shown in the following table in MBs.
        ///
        /// </summary>
        /// <remarks>
        ///                      buffer milliseconds
        ///                120 250 500 1sec 2sec 4sec 8sec
        ///
        ///          1      16  31  55  107  ---  ---  ---
        ///          2      10  18  31   55  107  ---  ---
        /// sample   4       8  12  18   31   55  107  ---
        ///  rate    5       8  10  14   24   45   87  ---
        ///  (us)    8       6   8  12   18   31   55  107
        ///         10       6   8  10   14   24   45   87
        /// </remarks>
        /// <param name="cfgMillis">100-10000</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgBufferSize")]
        public static extern int GpioCfgBufferSize(uint cfgMillis);

        /// <summary>
        /// Configures pigpio to use a particular sample rate timed by a specified
        /// peripheral.
        ///
        /// This function is only effective if called before [*gpioInitialise*].
        ///
        /// The timings are provided by the specified peripheral (PWM or PCM).
        ///
        /// The default setting is 5 microseconds using the PCM peripheral.
        ///
        /// The approximate CPU percentage used for each sample rate is:
        ///
        /// A sample rate of 5 microseconds seeems to be the sweet spot.
        /// </summary>
        /// <remarks>
        /// sample  cpu
        ///  rate    %
        ///
        ///   1     25
        ///   2     16
        ///   4     11
        ///   5     10
        ///   8     15
        ///  10     14
        /// </remarks>
        /// <param name="cfgMicros">1, 2, 4, 5, 8, 10</param>
        /// <param name="cfgPeripheral">0 (PWM), 1 (PCM)</param>
        /// <param name="cfgSource">deprecated, value is ignored</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgClock")]
        public static extern int GpioCfgClock(uint cfgMicros, uint cfgPeripheral, uint cfgSource);

        /// <summary>
        /// Configures pigpio to use the specified DMA channel.
        ///
        /// This function is only effective if called before [*gpioInitialise*].
        ///
        /// The default setting is to use channel 14.
        /// </summary>
        /// <remarks>
        /// DMAchannel: 0-14
        /// </remarks>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgDMAchannel")]
        [Obsolete]
        public static extern int GpioCfgDMAchannel(uint DMAchannel);

        /// <summary>
        /// Configures pigpio to use the specified DMA channels.
        ///
        /// This function is only effective if called before [*gpioInitialise*].
        ///
        /// The default setting is to use channel 14 for the primary channel and
        /// channel 6 for the secondary channel.
        ///
        /// The secondary channel is only used for the transmission of waves.
        ///
        /// If possible use one of channels 0 to 6 for the secondary channel
        /// (a full channel).
        ///
        /// A full channel only requires one DMA control block regardless of the
        /// length of a pulse delay.  Channels 7 to 14 (lite channels) require
        /// one DMA control block for each 16383 microseconds of delay.  I.e.
        /// a 10 second pulse delay requires one control block on a full channel
        /// and 611 control blocks on a lite channel.
        /// </summary>
        /// <param name="primaryChannel">0-14</param>
        /// <param name="secondaryChannel">0-14</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgDMAchannels")]
        public static extern int GpioCfgDMAchannels(uint primaryChannel, uint secondaryChannel);

        /// <summary>
        /// Configures pigpio to restrict GPIO updates via the socket or pipe
        /// interfaces to the GPIO specified by the mask.  Programs directly
        /// calling the pigpio library (i.e. linked with -lpigpio are not
        /// affected).  A GPIO update is a write to a GPIO or a GPIO mode
        /// change or any function which would force such an action.
        ///
        /// This function is only effective if called before [*gpioInitialise*].
        ///
        /// The default setting depends upon the Pi model. The user GPIO are
        /// added to the mask.
        ///
        /// If the board revision is not recognised then GPIO 2-27 are allowed.
        ///
        /// Unknown board @ PI_DEFAULT_UPDATE_MASK_UNKNOWN @ 0x0FFFFFFC
        /// Type 1 board  @ PI_DEFAULT_UPDATE_MASK_B1 @ 0x03E6CF93
        /// Type 2 board  @ PI_DEFAULT_UPDATE_MASK_A_B2 @ 0xFBC6CF9C
        /// Type 3 board  @ PI_DEFAULT_UPDATE_MASK_R3 @ 0x0FFFFFFC
        /// </summary>
        /// <param name="updateMask">bit (1&lt;&lt;n) is set for each GPIO n which may be updated</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgPermissions")]
        public static extern int GpioCfgPermissions(ulong updateMask);

        /// <summary>
        /// Configures pigpio to use the specified socket port.
        ///
        /// This function is only effective if called before [*gpioInitialise*].
        ///
        /// The default setting is to use port 8888.
        /// </summary>
        /// <param name="port">1024-32000</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgSocketPort")]
        public static extern int GpioCfgSocketPort(uint port);

        /// <summary>
        /// Configures pigpio support of the fifo and socket interfaces.
        ///
        /// This function is only effective if called before [*gpioInitialise*].
        ///
        /// The default setting (0) is that both interfaces are enabled.
        ///
        /// Or in PI_DISABLE_FIFO_IF to disable the pipe interface.
        ///
        /// Or in PI_DISABLE_SOCK_IF to disable the socket interface.
        ///
        /// Or in PI_LOCALHOST_SOCK_IF to disable remote socket
        /// access (this means that the socket interface is only
        /// usable from the local Pi).
        /// </summary>
        /// <param name="ifFlags">0-7</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgInterfaces")]
        public static extern int GpioCfgInterfaces(uint ifFlags);

        /// <summary>
        /// Selects the method of DMA memory allocation.
        ///
        /// This function is only effective if called before [*gpioInitialise*].
        ///
        /// There are two methods of DMA memory allocation.  The original method
        /// uses the /proc/self/pagemap file to allocate bus memory.  The new
        /// method uses the mailbox property interface to allocate bus memory.
        ///
        /// Auto will use the mailbox method unless a larger than default buffer
        /// size is requested with [*gpioCfgBufferSize*].
        /// </summary>
        /// <param name="memAllocMode">0-2</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgMemAlloc")]
        public static extern int GpioCfgMemAlloc(uint memAllocMode);

        /// <summary>
        /// Sets the network addresses which are allowed to talk over the
        /// socket interface.
        ///
        /// This function is only effective if called before [*gpioInitialise*].
        ///
        /// </summary>
        /// <param name="numSockAddr">0-256 (0 means all addresses allowed)</param>
        /// <param name="sockAddr">an array of permitted network addresses.</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgNetAddr")]
        public static extern int GpioCfgNetAddr(int numSockAddr, uint[] sockAddr);

        /// <summary>
        /// Used to tune internal settings.
        ///
        /// </summary>
        /// <param name="cfgWhat">see source code</param>
        /// <param name="cfgVal">see source code</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgInternals")]
        public static extern int GpioCfgInternals(uint cfgWhat, uint cfgVal);

        /// <summary>
        /// This function returns the current library internal configuration
        /// settings.
        /// </summary>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgGetInternals")]
        public static extern uint GpioCfgGetInternals();

        /// <summary>
        /// This function sets the current library internal configuration
        /// settings.
        ///
        /// </summary>
        /// <param name="cfgVal">see source code</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCfgSetInternals")]
        public static extern int GpioCfgSetInternals(uint cfgVal);
    }
}
