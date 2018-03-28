namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class GpioSample
    {
        /// <summary>
        /// The ticks in microseconds. Wraps every ~72 minutes.
        /// </summary>
        public uint Tick;

        /// <summary>
        /// The level (0 or 1)
        /// </summary>
        public uint Level;
    }
}
