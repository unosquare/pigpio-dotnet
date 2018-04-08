namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;

    /// <summary>
    /// Provides methods to open communication links on the available
    /// buses such as SPI, I2C, and UART
    /// </summary>
    public sealed class PeripheralsService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PeripheralsService"/> class.
        /// </summary>
        internal PeripheralsService()
        {
            // placeholder
        }

        #endregion

        #region SPI

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

        #endregion

        #region I2C

        /// <summary>
        /// Scans the I2C bus for devices.
        /// </summary>
        /// <param name="bus">The bus.</param>
        /// <returns>A list of device addresses.</returns>
        public byte[] ScanI2cBus(I2cBusId bus)
        {
            return I2cDevice.ScanBus(bus);
        }

        /// <summary>
        /// Opens an I2C device on the given bus.
        /// </summary>
        /// <param name="bus">The bus.</param>
        /// <param name="address">The address.</param>
        /// <returns>The I2C device</returns>
        public I2cDevice OpenI2cDevice(I2cBusId bus, byte address)
        {
            return new I2cDevice(bus, address);
        }

        /// <summary>
        /// Opens an I2C device on Bus 1 (the default I2C Bus).
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The I2C device</returns>
        public I2cDevice OpenI2cDevice(byte address)
        {
            return new I2cDevice(I2cDevice.DefaultBus, address);
        }

        #endregion
    }
}