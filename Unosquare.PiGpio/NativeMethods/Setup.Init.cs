namespace Unosquare.PiGpio.NativeMethods
{
    using NativeEnums;
    using System.Runtime.InteropServices;

    public static partial class Setup
    {
        /// <summary>
        /// Initialises the library.
        ///
        /// gpioInitialise must be called before using the other library functions
        /// with the following exceptions:
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// if (gpioInitialise() &lt; 0)
        /// {
        ///    // pigpio initialisation failed.
        /// }
        /// else
        /// {
        ///    // pigpio initialised okay.
        /// }
        /// </code>
        /// </example>
        /// <remarks>
        /// <see cref="GpioCfg*"/>
        /// <see cref="GpioVersion"/>
        /// <see cref="GpioHardwareRevision"/>
        /// </remarks>
        /// <returns>Returns the pigpio version number if OK, otherwise PI_INIT_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioInitialise")]
        public static extern ResultCode GpioInitialise();

        /// <summary>
        /// Terminates the library.
        ///
        /// Returns nothing.
        ///
        /// Call before program exit.
        ///
        /// This function resets the used DMA channels, releases memory, and
        /// terminates any running threads.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioTerminate();
        /// </code>
        /// </example>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioTerminate")]
        public static extern void GpioTerminate();
    }
}
