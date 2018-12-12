namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents sGPIO ample data.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class GpioSample
    {
        private uint m_Tick;
        private uint m_Level;

        /// <summary>
        /// The ticks in microseconds. Wraps every ~72 minutes.
        /// </summary>
        public uint Tick { get => m_Tick; set => m_Tick = value; }

        /// <summary>
        /// The level (0 or 1).
        /// </summary>
        public uint Level { get => m_Level; set => m_Level = value; }
    }
}
