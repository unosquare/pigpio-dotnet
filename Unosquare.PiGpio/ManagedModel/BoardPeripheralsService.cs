namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;

    /// <summary>
    /// Provides methods to open communication links on the available
    /// buses such as SPI, I2C, and UART
    /// </summary>
    public sealed class BoardPeripheralsService
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardPeripheralsService"/> class.
        /// </summary>
        internal BoardPeripheralsService()
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
        public SpiChannel OpenSpiChannel(SpiChannelId channel, int baudRate, SpiFlags flags) =>
            new SpiChannel(channel, baudRate, flags);

        /// <summary>
        /// Opens the given SPI channel using the default flags.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <returns>The peripheral service</returns>
        public SpiChannel OpenSpiChannel(SpiChannelId channel, int baudRate) =>
            new SpiChannel(channel, baudRate, SpiFlags.Default);

        /// <summary>
        /// Opens the given SPI channel using the default flags and a baud rate of 512k bits per second.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>The peripheral service</returns>
        public SpiChannel OpenSpiChannel(SpiChannelId channel) =>
            new SpiChannel(channel, 512000, SpiFlags.Default);

        #endregion

        #region Soft SPI

        /// <summary>
        /// Opens a software based (bit-banged) SPI channel
        /// </summary>
        /// <param name="csPin">The cs pin.</param>
        /// <param name="misoPin">The miso pin.</param>
        /// <param name="mosiPin">The mosi pin.</param>
        /// <param name="clockPin">The clock pin.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>The SPI channel</returns>
        public SoftSpiChannel OpenSoftSpiChannel(GpioPin csPin, GpioPin misoPin, GpioPin mosiPin, GpioPin clockPin, int baudRate, SoftSpiFlags flags)
            => new SoftSpiChannel(csPin, misoPin, mosiPin, clockPin, baudRate, flags);

        /// <summary>
        /// Opens a software based (bit-banged) SPI channel
        /// </summary>
        /// <param name="csPin">The cs pin.</param>
        /// <param name="misoPin">The miso pin.</param>
        /// <param name="mosiPin">The mosi pin.</param>
        /// <param name="clockPin">The clock pin.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <returns>The SPI channel</returns>
        public SoftSpiChannel OpenSoftSpiChannel(GpioPin csPin, GpioPin misoPin, GpioPin mosiPin, GpioPin clockPin, int baudRate)
            => new SoftSpiChannel(csPin, misoPin, mosiPin, clockPin, baudRate, SoftSpiFlags.Default);

        /// <summary>
        /// Opens a software based (bit-banged) SPI channel
        /// </summary>
        /// <param name="csPin">The cs pin.</param>
        /// <param name="misoPin">The miso pin.</param>
        /// <param name="mosiPin">The mosi pin.</param>
        /// <param name="clockPin">The clock pin.</param>
        /// <returns>The SPI channel</returns>
        public SoftSpiChannel OpenSoftSpiChannel(GpioPin csPin, GpioPin misoPin, GpioPin mosiPin, GpioPin clockPin)
            => new SoftSpiChannel(csPin, misoPin, mosiPin, clockPin, 500000, SoftSpiFlags.Default);

        #endregion

        #region I2C

        /// <summary>
        /// Scans the I2C bus for devices.
        /// </summary>
        /// <param name="bus">The bus.</param>
        /// <returns>A list of device addresses.</returns>
        public byte[] ScanI2cBus(I2cBusId bus) =>
            I2cDevice.ScanBus(bus);

        /// <summary>
        /// Scans the default I2C bus for devices.
        /// </summary>
        /// <returns>The found device addresses</returns>
        public byte[] ScanI2cBus() =>
            I2cDevice.ScanBus(I2cDevice.DefaultBus);

        /// <summary>
        /// Opens an I2C device on the given bus.
        /// </summary>
        /// <param name="bus">The bus.</param>
        /// <param name="address">The address.</param>
        /// <returns>The I2C device</returns>
        public I2cDevice OpenI2cDevice(I2cBusId bus, byte address) =>
            new I2cDevice(bus, address);

        /// <summary>
        /// Opens an I2C device on Bus 1 (the default I2C Bus).
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>The I2C device</returns>
        public I2cDevice OpenI2cDevice(byte address) =>
            new I2cDevice(I2cDevice.DefaultBus, address);

        #endregion

        #region Soft I2C

        /// <summary>
        /// Opens a software based I2C bus.
        /// </summary>
        /// <param name="dataPin">The data pin.</param>
        /// <param name="clockPin">The clock pin.</param>
        /// <param name="baudRate">The baud rate from 50 t 500,000 bits per second.</param>
        /// <returns>I2C Bus Accessor</returns>
        public SoftI2cBus OpenSoftI2cBus(GpioPin dataPin, GpioPin clockPin, int baudRate) =>
            new SoftI2cBus(dataPin, clockPin, baudRate);

        /// <summary>
        /// Opens a software based I2C bus.
        /// </summary>
        /// <param name="dataPin">The data pin.</param>
        /// <param name="clockPin">The clock pin.</param>
        /// <returns>I2C Bus Accessor</returns>
        public SoftI2cBus OpenSoftI2cBus(GpioPin dataPin, GpioPin clockPin) =>
            new SoftI2cBus(dataPin, clockPin, SoftI2cBus.DefaultBaudRate);

        #endregion

        #region UART

        /// <summary>
        /// Opens the specified UART port.
        /// </summary>
        /// <param name="portName">Name of the port.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <returns>The UART port object</returns>
        public UartPort OpenUartPort(string portName, UartRate baudRate) =>
            new UartPort(portName, baudRate);

        /// <summary>
        /// Lists the UART port names.
        /// </summary>
        /// <returns>The port names</returns>
        public string[] ListUartPortNames() =>
            UartPort.ListPortNames();

        #endregion

        #region Soft Serial

        /// <summary>
        /// Opens a software (bit-banged) serial port.
        /// </summary>
        /// <param name="receivePin">The receive pin.</param>
        /// <param name="transmitPin">The transmit pin.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <param name="dataBits">The data bits.</param>
        /// <param name="invert">if set to <c>true</c> [invert].</param>
        /// <returns>The software serial port</returns>
        public SoftSerialPort OpenSoftSerialPort(GpioPin receivePin, GpioPin transmitPin, UartRate baudRate, int dataBits, bool invert) =>
            new SoftSerialPort(receivePin, transmitPin, baudRate, dataBits, invert);

        /// <summary>
        /// Opens a software (bit-banged) serial port.
        /// </summary>
        /// <param name="receivePin">The receive pin.</param>
        /// <param name="transmitPin">The transmit pin.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <returns>The software serial port</returns>
        public SoftSerialPort OpenSoftSerialPort(GpioPin receivePin, GpioPin transmitPin, UartRate baudRate) =>
            new SoftSerialPort(receivePin, transmitPin, baudRate, 8, false);

        #endregion
    }
}