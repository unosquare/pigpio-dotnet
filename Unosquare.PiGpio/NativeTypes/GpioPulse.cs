namespace Unosquare.PiGpio.NativeTypes
{
    using NativeEnums;
    using System.Runtime.InteropServices;

    /// <summary>
    /// A pulse representing microseconds in the high position,
    /// microseconds in the low position, and a delay measure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class GpioPulse
    {
        private BitMask m_GpioOn;
        private BitMask m_GpioOff;
        private uint m_DelayMicroseconds;

        /// <summary>
        /// The GPIO pins to turn on
        /// </summary>
        public BitMask GpioOn { get => m_GpioOn; set => m_GpioOn = value; }

        /// <summary>
        /// The gpio pins to turn off
        /// </summary>
        public BitMask GpioOff { get => m_GpioOff; set => m_GpioOff = value; }

        /// <summary>
        /// The duration in microseconds
        /// </summary>
        public uint DurationMicroSecs { get => m_DelayMicroseconds; set => m_DelayMicroseconds = value; }
    }
}
