namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The Raw CBS (Linux Control Block)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RawCBS
    {
        #region Fields

        private uint m_Info;

        private uint m_Source;

        private uint m_Destination;

        private uint m_Length;

        private uint m_Stride;

        private uint m_Next;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        private uint[] m_Pad;

        #endregion

        /// <summary>
        /// The information
        /// </summary>
        public uint Info { get => m_Info; set => m_Info = value; }

        /// <summary>
        /// The source
        /// </summary>
        public uint Source { get => m_Source; set => m_Source = value; }

        /// <summary>
        /// The destination
        /// </summary>
        public uint Destination { get => m_Destination; set => m_Destination = value; }

        /// <summary>
        /// The length
        /// </summary>
        public uint Length { get => m_Length; set => m_Length = value; }

        /// <summary>
        /// The stride
        /// </summary>
        public uint Stride { get => m_Stride; set => m_Stride = value; }

        /// <summary>
        /// The next
        /// </summary>
        public uint Next { get => m_Next; set => m_Next = value; }

        /// <summary>
        /// The pad
        /// </summary>
        public uint[] Pad { get => m_Pad; set => m_Pad = value; }
    }
}
