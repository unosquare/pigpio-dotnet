namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// An I2C Message Segment
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class I2CMessageSegment
    {
        /// <summary>
        /// slave address 
        /// </summary>
        public ushort Address;

        /// <summary>
        /// The flags
        /// </summary>
        public ushort Flags;

        /// <summary>
        /// msg length
        /// </summary>
        public ushort Length;

        /// <summary>
        /// pointer to msg data
        /// </summary>
        [MarshalAs(UnmanagedType.LPArray)]
        public byte[] Buffer;
    }
}
