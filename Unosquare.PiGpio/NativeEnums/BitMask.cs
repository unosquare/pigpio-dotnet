namespace Unosquare.PiGpio.NativeEnums
{
    using System;

    /// <summary>
    /// Provides an enumeration of a 32-bit mask
    /// </summary>
    [Flags]
    public enum BitMask : uint
    {
        /// <summary>An empty bitmask</summary>
        None = 0x00000000,

        /// <summary>An full bitmask</summary>
        All = 0xFFFFFFFF,

        /// <summary>The bit at index 00, from right LSB to left MSB</summary>
        Bit00 = 0x00000001,

        /// <summary>The bit at index 01, from right LSB to left MSB</summary>
        Bit01 = 0x00000002,

        /// <summary>The bit at index 02, from right LSB to left MSB</summary>
        Bit02 = 0x00000004,

        /// <summary>The bit at index 03, from right LSB to left MSB</summary>
        Bit03 = 0x00000008,

        /// <summary>The bit at index 04, from right LSB to left MSB</summary>
        Bit04 = 0x00000010,

        /// <summary>The bit at index 05, from right LSB to left MSB</summary>
        Bit05 = 0x00000020,

        /// <summary>The bit at index 06, from right LSB to left MSB</summary>
        Bit06 = 0x00000040,

        /// <summary>The bit at index 07, from right LSB to left MSB</summary>
        Bit07 = 0x00000080,

        /// <summary>The bit at index 08, from right LSB to left MSB</summary>
        Bit08 = 0x00000100,

        /// <summary>The bit at index 09, from right LSB to left MSB</summary>
        Bit09 = 0x00000200,

        /// <summary>The bit at index 10, from right LSB to left MSB</summary>
        Bit10 = 0x00000400,

        /// <summary>The bit at index 11, from right LSB to left MSB</summary>
        Bit11 = 0x00000800,

        /// <summary>The bit at index 12, from right LSB to left MSB</summary>
        Bit12 = 0x00001000,

        /// <summary>The bit at index 13, from right LSB to left MSB</summary>
        Bit13 = 0x00002000,

        /// <summary>The bit at index 14, from right LSB to left MSB</summary>
        Bit14 = 0x00004000,

        /// <summary>The bit at index 15, from right LSB to left MSB</summary>
        Bit15 = 0x00008000,

        /// <summary>The bit at index 16, from right LSB to left MSB</summary>
        Bit16 = 0x00010000,

        /// <summary>The bit at index 17, from right LSB to left MSB</summary>
        Bit17 = 0x00020000,

        /// <summary>The bit at index 18, from right LSB to left MSB</summary>
        Bit18 = 0x00040000,

        /// <summary>The bit at index 19, from right LSB to left MSB</summary>
        Bit19 = 0x00080000,

        /// <summary>The bit at index 20, from right LSB to left MSB</summary>
        Bit20 = 0x00100000,

        /// <summary>The bit at index 21, from right LSB to left MSB</summary>
        Bit21 = 0x00200000,

        /// <summary>The bit at index 22, from right LSB to left MSB</summary>
        Bit22 = 0x00400000,

        /// <summary>The bit at index 23, from right LSB to left MSB</summary>
        Bit23 = 0x00800000,

        /// <summary>The bit at index 24, from right LSB to left MSB</summary>
        Bit24 = 0x01000000,

        /// <summary>The bit at index 25, from right LSB to left MSB</summary>
        Bit25 = 0x02000000,

        /// <summary>The bit at index 26, from right LSB to left MSB</summary>
        Bit26 = 0x04000000,

        /// <summary>The bit at index 27, from right LSB to left MSB</summary>
        Bit27 = 0x08000000,

        /// <summary>The bit at index 28, from right LSB to left MSB</summary>
        Bit28 = 0x10000000,

        /// <summary>The bit at index 29, from right LSB to left MSB</summary>
        Bit29 = 0x20000000,

        /// <summary>The bit at index 30, from right LSB to left MSB</summary>
        Bit30 = 0x40000000,

        /// <summary>The bit at index 31, from right LSB to left MSB</summary>
        Bit31 = 0x80000000,
    }
}
