namespace Unosquare.PiGpio.NativeTypes
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents Raw SPI channel data
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RawSpiData
    {
        /// <summary>
        /// GPIO for clock
        /// </summary>
        public int ClockPin;

        /// <summary>
        /// GPIO for MOSI
        /// </summary>
        public int MosiPin;

        /// <summary>
        /// GPIO for MISO
        /// </summary>
        public int MisoPin;

        /// <summary>
        /// slave select off state
        /// </summary>
        public int SlaveSelectOffState;

        /// <summary>
        /// delay after slave select
        /// </summary>
        public int SlaveSelectDelayMicroseconds;

        /// <summary>
        /// clock off state
        /// </summary>
        public int ClockOffState;

        /// <summary>
        /// clock phase
        /// </summary>
        public int ClockPhase;

        /// <summary>
        /// clock micros
        /// </summary>
        public int ClockMicroseconds;
    }
}
