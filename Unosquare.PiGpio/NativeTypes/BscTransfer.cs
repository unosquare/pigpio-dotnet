namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class BscTransfer
    {
        /// <summary>
        /// Write
        /// </summary>
        public uint Control;

        /// <summary>
        /// The rx count
        /// </summary>
        public int ReceiveCount;

        /// <summary>
        /// Read only
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] ReceiveBuffer;

        /// <summary>
        /// Write
        /// </summary>
        public int SendCount;

        /// <summary>
        /// Write
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
        public byte[] SendBuffer;
    }
}
