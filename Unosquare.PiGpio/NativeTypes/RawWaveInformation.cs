namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents raw waveform information
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RawWaveInformation
    {
        /// <summary>
        /// first CB used by wave
        /// </summary>
        public ushort BottomCB;

        /// <summary>
        /// last CB used by wave
        /// </summary>
        public ushort TopCB;

        /// <summary>
        /// last OOL used by wave
        /// </summary>
        public ushort BottomOOL;

        /// <summary>
        /// first OOL used by wave
        /// </summary>
        public ushort TopOOL;

        public ushort Deleted;

        public ushort NumberCB;

        public ushort NumberBOOL;

        public ushort NumberTOOL;
    }
}
