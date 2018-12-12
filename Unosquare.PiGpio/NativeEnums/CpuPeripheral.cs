namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Defines the 2 different CPU peripherals for DMA.
    /// </summary>
    public enum CpuPeripheral
    {
        /// <summary>
        /// The Pulse-Width modulation peripheral
        /// </summary>
        Pwm = 0,

        /// <summary>
        /// The Pulse-Code Modulation peripheral
        /// </summary>
        Pcm = 1,
    }
}
