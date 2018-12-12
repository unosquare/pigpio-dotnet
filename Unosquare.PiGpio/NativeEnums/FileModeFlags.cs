namespace Unosquare.PiGpio.NativeEnums
{
    using System;

    /// <summary>
    /// Enumerates the different file acces modes.
    /// </summary>
    [Flags]
    public enum FileModeFlags : int
    {
        /// <summary>
        /// The read mode flag
        /// </summary>
        Read = 1,

        /// <summary>
        /// The write mode flag
        /// </summary>
        Write = 2,

        /// <summary>
        /// The append mode flag
        /// </summary>
        Append = 4,

        /// <summary>
        /// The create mode flag
        /// </summary>
        Create = 8,

        /// <summary>
        /// The truncate mode flag
        /// </summary>
        Truncate = 16,
    }
}
