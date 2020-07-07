namespace Unosquare.PiGpio.ManagedModel
{
    using System;
    using NativeEnums;
    using NativeMethods.InProcess.DllImports;

    /// <summary>
    /// Provides access to the Hardware SPI channels.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public sealed class SpiChannel : IDisposable
    {
        private bool _isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpiChannel"/> class.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <param name="baudRate">The baud rate.</param>
        /// <param name="flags">The flags.</param>
        internal SpiChannel(SpiChannelId channel, int baudRate, SpiFlags flags)
        {
            BaudRate = baudRate;
            Channel = channel;
            Handle = Spi.SpiOpen(channel, baudRate, flags);
        }

        /// <summary>
        /// Gets the baud rate in bits per second.
        /// </summary>
        public int BaudRate { get; }

        /// <summary>
        /// Gets the SPI channel identifier.
        /// </summary>
        public SpiChannelId Channel { get; }

        /// <summary>
        /// Gets the SPI flags this channel was opened with.
        /// </summary>
        public SpiFlags Flags { get; }

        /// <summary>
        /// Gets the SPI channel handle.
        /// </summary>
        public UIntPtr Handle { get; private set; }

        /// <summary>
        /// Reads up to one tenth of the byte rate.
        /// </summary>
        /// <returns>The bytes that were read.</returns>
        public byte[] Read() => Read(BaudRate / 8 / 10);

        /// <summary>
        /// Reads up to the specified number of bytes.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The bytes read.</returns>
        public byte[] Read(int count) => Spi.SpiRead(Handle, count);

        /// <summary>
        /// Reads int the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        /// <returns>The number of bytes read into the buffer.</returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            var data = Spi.SpiRead(Handle, count);
            Buffer.BlockCopy(data, 0, buffer, offset, data.Length);
            return data.Length;
        }

        /// <summary>
        /// Writes the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>The number of bytes written.</returns>
        public int Write(byte[] buffer) => Spi.SpiWrite(Handle, buffer);

        /// <summary>
        /// Writes the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count">The count.</param>
        /// <returns>The number of bytes written.</returns>
        public int Write(byte[] buffer, int offset, int count)
        {
            var data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            return Spi.SpiWrite(Handle, data);
        }

        /// <summary>
        /// Transfers the specified buffer and simultaneously reads the same amount of buyes in that send buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>The bytes that were read.</returns>
        public byte[] Transfer(byte[] buffer) => Spi.SpiXfer(Handle, buffer);

        /// <inheritdoc />
        public void Dispose() => Dispose(true);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="alsoManaged"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool alsoManaged)
        {
            if (_isDisposed) return;

            if (alsoManaged)
            {
                Spi.SpiClose(Handle);
                Handle = UIntPtr.Zero;
            }

            _isDisposed = true;
        }
    }
}
