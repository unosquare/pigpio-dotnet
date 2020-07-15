namespace Unosquare.PiGpio
{
    using RaspberryIO.Abstractions;

    /// <summary>
    /// The SPI Bus containing the 2 SPI channels.
    /// </summary>
    public class SpiBus : ISpiBus
    {
        /// <inheritdoc />
        public int Channel0Frequency { get; set; }

        /// <inheritdoc />
        public int Channel1Frequency { get; set; }

        /// <inheritdoc />
        public int DefaultFrequency => 8000000;

        /// <inheritdoc />
        public ISpiChannel Channel0 { get; }

        /// <inheritdoc />
        public ISpiChannel Channel1 { get; }
    }
}
