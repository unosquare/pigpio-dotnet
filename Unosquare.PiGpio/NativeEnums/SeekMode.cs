namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Defines the different file seek modes.
    /// </summary>
    public enum SeekMode
    {
        /// <summary>
        /// From the start of the file
        /// </summary>
        FromStart = 0,

        /// <summary>
        /// From the current file position
        /// </summary>
        FromCurrent = 1,

        /// <summary>
        /// From the end position (backwards)
        /// </summary>
        FromEnd = 2,
    }
}
