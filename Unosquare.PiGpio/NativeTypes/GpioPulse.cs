namespace Unosquare.PiGpio.NativeTypes
{
    using NativeEnums;
    using System.Runtime.InteropServices;

    /// <summary>
    /// A pulse representing microseconds in the high position,
    /// microseconds in the low position, and a delay measure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GpioPulse
    {
        /// <summary>
        /// The GPIO pins to turn on.
        /// </summary>
        public BitMask GpioOn;

        /// <summary>
        /// The gpio pins to turn off.
        /// </summary>
        public BitMask GpioOff;

        /// <summary>
        /// The duration in microseconds.
        /// </summary>
        public uint DurationMicroSecs;
    }
}
