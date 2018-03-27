namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

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
