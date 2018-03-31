namespace Unosquare.PiGpio.NativeEnums
{
    using System;

    /// <summary>
    /// Defines flags to enable or disable network
    /// interfaces
    /// </summary>
    [Flags]
    public enum InterfaceFlags
    {
        /// <summary>
        /// The disable FIFO interface
        /// </summary>
        DisableFifoInterface = 1,

        /// <summary>
        /// The disable socket interface
        /// </summary>
        DisableSocketInterface = 2,

        /// <summary>
        /// The localhost interface
        /// </summary>
        LocalhostInterface = 4,
    }
}
