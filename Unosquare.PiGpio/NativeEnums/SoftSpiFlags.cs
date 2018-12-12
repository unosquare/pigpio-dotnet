namespace Unosquare.PiGpio.NativeEnums
{
    using System;

    /// <summary>
    /// Defines the Software-based SPI flags.
    /// </summary>
    [Flags]
    public enum SoftSpiFlags
    {
        /// <summary>
        /// The default flags (all 0)
        /// </summary>
        Default = 0,

        /// <summary>
        /// For CPHA=1, the out side changes the data on the leading
        /// edge of the current clock cycle, while the in side captures
        /// the data on (or shortly after) the trailing edge of the clock cycle.
        /// The out side holds the data valid until the leading edge of the
        /// following clock cycle. For the last cycle, the slave holds
        /// the MISO line valid until slave select is deasserted.
        /// </summary>
        ClockPhaseLeadingEdge = 0b0_0_00000000000_0_0_1,

        /// <summary>
        /// CPOL=1 is a clock which idles at 1, and each cycle consists
        /// of a pulse of 0. That is, the leading edge is a falling edge,
        /// and the trailing edge is a rising edge.
        /// </summary>
        ClockPolarityIdleHigh = 0b0_0_00000000000_0_1_0,

        /// <summary>
        /// T is 1 if the least significant bit is transmitted on MOSI first, the
        /// default (0) shifts the most significant bit out first.
        /// </summary>
        MosiInvert = 0b0_1_00000000000_0_0_0,

        /// <summary>
        /// R is 1 if the least significant bit is received on MISO first, the
        /// default (0) receives the most significant bit first.
        /// </summary>
        MisoInvert = 0b1_0_00000000000_0_0_0,
    }
}
