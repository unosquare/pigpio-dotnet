namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents raw waveform data
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RawWave
    {
        /// <summary>
        /// The gpio on
        /// </summary>
        public uint GpioOn;

        /// <summary>
        /// The gpio off
        /// </summary>
        public uint GpioOff;

        /// <summary>
        /// The delay microseconds
        /// </summary>
        public uint DelayMicroseconds;

        /// <summary>
        /// The flags
        /// </summary>
        public uint Flags;
    }
}
