namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Enumerates the different Raspberry Pi board types.
    /// </summary>
    public enum BoardType : int
    {
        /// <summary>
        /// Unknown board type
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Type 1 - Model B (original model)
        /// </summary>
        Type1 = 1,

        /// <summary>
        /// Type 2 - Model A and B (revision 2)
        /// </summary>
        Type2 = 2,

        /// <summary>
        /// Type 3 - Model A+, B+, Pi Zero, Pi2B, Pi3B
        /// </summary>
        Type3 = 3,
    }
}
