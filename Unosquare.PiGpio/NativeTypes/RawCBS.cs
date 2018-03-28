namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The Raw CBS (Linux Control Block)
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RawCBS
    {
        /// <summary>
        /// The information
        /// </summary>
        public uint Info;

        /// <summary>
        /// The source
        /// </summary>
        public uint Source;

        /// <summary>
        /// The destination
        /// </summary>
        public uint Destination;

        /// <summary>
        /// The length
        /// </summary>
        public uint Length;

        /// <summary>
        /// The stride
        /// </summary>
        public uint Stride;

        /// <summary>
        /// The next
        /// </summary>
        public uint Next;

        /// <summary>
        /// The pad
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] Pad;
    }
}
