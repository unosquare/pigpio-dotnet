namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// A pulse representing microseconds in the high position,
    /// microseconds in the low position, and a delay measure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class GpioPulse
    {
        private uint m_GpioOn;
        private uint m_GpioOff;
        private uint m_DelayMicroseconds;

        /// <summary>
        /// The gpio on
        /// </summary>
        public uint GpioOn { get => m_GpioOn; set => m_GpioOn = value; }

        /// <summary>
        /// The gpio off
        /// </summary>
        public uint GpioOff { get => m_GpioOff; set => m_GpioOff = value; }

        /// <summary>
        /// The delay microseconds
        /// </summary>
        public uint DelayMicroseconds { get => m_DelayMicroseconds; set => m_DelayMicroseconds = value; }
    }
}
