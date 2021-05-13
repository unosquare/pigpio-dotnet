namespace Unosquare.PiGpio.NativeEnums
{
    using System;

    /// <summary>
    /// Defines the different pin capabilities.
    /// </summary>
    [Flags]
    public enum PinCapabilities
    {
        /// <summary>
        /// General Purpose capability: Digital and Analog Read/Write
        /// </summary>
        GP = 0x01,

        /// <summary>
        /// General Purpose Clock (not PWM)
        /// </summary>
        GPCLK = 0x02,

        /// <summary>
        /// i2c data channel
        /// </summary>
        I2CSDA = 0x04,

        /// <summary>
        /// i2c clock channel
        /// </summary>
        I2CSCL = 0x08,

        /// <summary>
        /// SPI Master Out, Slave In channel
        /// </summary>
        SPIMOSI = 0x10,

        /// <summary>
        /// SPI Master In, Slave Out channel
        /// </summary>
        SPIMISO = 0x20,

        /// <summary>
        /// SPI Clock channel
        /// </summary>
        SPICLK = 0x40,

        /// <summary>
        /// SPI Chip Select Channel
        /// </summary>
        SPICS = 0x80,

        /// <summary>
        /// UART Request to Send Channel
        /// </summary>
        UARTRTS = 0x100,

        /// <summary>
        /// UART Transmit Channel
        /// </summary>
        UARTTXD = 0x200,

        /// <summary>
        /// UART Receive Channel
        /// </summary>
        UARTRXD = 0x400,

        /// <summary>
        /// Hardware Pule Width Modulation
        /// </summary>
        PWM = 0x800,
    }
}