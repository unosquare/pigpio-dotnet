namespace Unosquare.PiGpio.NativeTypes
{
    using NativeEnums;
    using System.Runtime.InteropServices;

    /// <summary>
    /// A waveform entry consisting of GPIO bits to turn on, GPIO bits to turn off, and a delay before the next waveform entry.
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
