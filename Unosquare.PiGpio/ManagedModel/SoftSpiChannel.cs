namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;

    /// <summary>
    /// Provides a bit-banged version of a SPI channel.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public sealed class SoftSpiChannel : IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftSpiChannel"/> class.
        /// </summary>
        /// <param name="csPin">The cs pin.</param>
        /// <param name="misoPin">The miso pin.</param>
        /// <param name="mosiPin">The mosi pin.</param>
        /// <param name="clockPin">The clock pin.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <param name="flags">The flags.</param>
        internal SoftSpiChannel(GpioPin csPin, GpioPin misoPin, GpioPin mosiPin, GpioPin clockPin, int baudRate, SoftSpiFlags flags)
        {
            BoardException.ValidateResult(Spi.BbSPIOpen(
                (UserGpio)csPin.PinNumber,
                (UserGpio)misoPin.PinNumber,
                (UserGpio)mosiPin.PinNumber,
                (UserGpio)clockPin.PinNumber,
                Convert.ToUInt32(baudRate),
                flags));

            Handle = (UserGpio)csPin.PinNumber;
            ChipSelectPin = csPin;
            MosiPin = mosiPin;
            MisoPin = misoPin;
            ClockPin = clockPin;
            BaudRate = baudRate;
            Flags = flags;
        }

        /// <summary>
        /// Gets the handle.
        /// </summary>
        public UserGpio Handle { get; }

        /// <summary>
        /// Gets the chip select pin.
        /// </summary>
        public GpioPin ChipSelectPin { get; }

        /// <summary>
        /// Gets the MOSI pin.
        /// </summary>
        public GpioPin MosiPin { get; }

        /// <summary>
        /// Gets the MISO pin.
        /// </summary>
        public GpioPin MisoPin { get; }

        /// <summary>
        /// Gets the clock pin.
        /// </summary>
        public GpioPin ClockPin { get; }

        /// <summary>
        /// Gets the baud rate.
        /// </summary>
        public int BaudRate { get; }

        /// <summary>
        /// Gets the flags.
        /// </summary>
        public SoftSpiFlags Flags { get; }

        /// <summary>
        /// Transfers the specified transmit buffer and returns the read bytes in a new buffer.
        /// </summary>
        /// <param name="transmitBuffer">The transmit buffer.</param>
        /// <returns>The received bytes as a result of writing to the ring buffer</returns>
        public byte[] Transfer(byte[] transmitBuffer)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(SoftSpiChannel));

            var receiveBuffer = new byte[transmitBuffer.Length];
            var result = BoardException.ValidateResult(Spi.BbSPIXfer(Handle, transmitBuffer, receiveBuffer, Convert.ToUInt32(receiveBuffer.Length)));
            if (result == receiveBuffer.Length)
                return receiveBuffer;

            var output = new byte[result];
            Buffer.BlockCopy(receiveBuffer, 0, output, 0, result);
            return output;
        }

        /// <inheritdoc />
        public void Dispose() => Dispose(true);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="alsoManaged"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool alsoManaged)
        {
            if (_isDisposed) return;

            _isDisposed = true;
            if (alsoManaged)
            {
                Spi.BbSPIClose(Handle);
            }
        }
    }
}
