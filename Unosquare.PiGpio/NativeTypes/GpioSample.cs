namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class GpioSample
    {
        /// <summary>
        /// The tick
        /// </summary>
        public uint Tick;

        /// <summary>
        /// The level
        /// </summary>
        public uint Level;
    }
}
