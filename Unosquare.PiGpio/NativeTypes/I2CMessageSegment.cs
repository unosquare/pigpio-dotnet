namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// An I2C Message Segment.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class I2CMessageSegment
    {
        #region Fields

        private ushort m_Address;

        private ushort m_Flags;

        private ushort m_Length;

        [MarshalAs(UnmanagedType.LPArray)]
        private byte[] m_Buffer;

        #endregion

        /// <summary>
        /// Slave address.
        /// </summary>
        public ushort Address { get => m_Address; set => m_Address = value; }

        /// <summary>
        /// The flags.
        /// </summary>
        public ushort Flags { get => m_Flags; set => m_Flags = value; }

        /// <summary>
        /// msg length.
        /// </summary>
        public ushort Length { get => m_Length; set => m_Length = value; }

        /// <summary>
        /// pointer to msg data.
        /// </summary>
        public byte[] Buffer { get => m_Buffer; set => m_Buffer = value; }
    }
}
