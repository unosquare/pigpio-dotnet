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
    }
}
