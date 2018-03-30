namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents raw waveform data
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public sealed class RawWave
    {
        private uint m_GpioOn;
        private uint m_GpioOff;
        private uint m_DelayMicroseconds;
        private uint m_Flags;

        /// <summary>
        /// The gpio on
        /// </summary>
        public uint GpioOn
        {
            get => m_GpioOn;
            set => m_GpioOn = value;
        }

        /// <summary>
        /// The gpio off
        /// </summary>
        public uint GpioOff
        {
            get => m_GpioOff;
            set => m_GpioOff = value;
        }

        /// <summary>
        /// The delay microseconds
        /// </summary>
        public uint DelayMicroseconds
        {
            get => m_DelayMicroseconds;
            set => m_DelayMicroseconds = value;
        }

        /// <summary>
        /// The flags
        /// </summary>
        public uint Flags
        {
            get => m_Flags;
            set => m_Flags = value;
        }
    }
}
