namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;

    /// <summary>
    /// Provides methods to open communication links on the available
    /// buses such as SPI, I2C, and UART
    /// </summary>
    public sealed class GpioPeripheralsService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPeripheralsService"/> class.
        /// </summary>
        internal GpioPeripheralsService()
        {
            // placeholder
        }

        /// <summary>
        /// Opens the given SPI channel.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>The peripheral service</returns>
        public SpiChannel OpenSpiChannel(SpiChannelId channel, int baudRate, SpiFlags flags)
        {
            return new SpiChannel(channel, baudRate, flags);
        }

        /// <summary>
        /// Opens the given SPI channel using the default flags.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <returns>The peripheral service</returns>
        public SpiChannel OpenSpiChannel(SpiChannelId channel, int baudRate)
        {
            return new SpiChannel(channel, baudRate, SpiFlags.Default);
        }

        /// <summary>
        /// Opens the given SPI channel using the default flags and a baud rate of 512k bits per second.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>The peripheral service</returns>
        public SpiChannel OpenSpiChannel(SpiChannelId channel)
        {
            return new SpiChannel(channel, 512000, SpiFlags.Default);
        }
    }
}
