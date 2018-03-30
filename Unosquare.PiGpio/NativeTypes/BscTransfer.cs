namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// A data structure representing a BSC transfer.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class BscTransfer
    {
        private uint m_Control;

        private int m_ReceiveCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        private byte[] m_ReceiveBuffer;

        private int m_SendCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        private byte[] m_SendBuffer;

        /// <summary>
        /// Write
        /// </summary>
        public uint Control { get => m_Control; set => m_Control = value; }

        /// <summary>
        /// The rx count
        /// </summary>
        public int ReceiveCount { get => m_ReceiveCount; set => m_ReceiveCount = value; }

        /// <summary>
        /// Read only
        /// </summary>
        public byte[] ReceiveBuffer { get => m_ReceiveBuffer; set => m_ReceiveBuffer = value; }

        /// <summary>
        /// Write
        /// </summary>
        public int SendCount { get => m_SendCount; set => m_SendCount = value; }

        /// <summary>
        /// Write
        /// </summary>
        public byte[] SendBuffer { get => m_SendBuffer; set => m_SendBuffer = value; }
    }
}
