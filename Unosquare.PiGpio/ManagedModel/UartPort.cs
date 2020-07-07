namespace Unosquare.PiGpio.ManagedModel
{
    using System;
    using NativeEnums;
    using NativeMethods.InProcess.DllImports;

    /// <summary>
    /// Provides libpigpio implementation of a UART port.
    /// Alternatively you can use the System.IO.Ports.SerialPort implementation.
    /// </summary>
    public sealed class UartPort : IDisposable
    {
        private bool IsDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UartPort"/> class.
        /// </summary>
        /// <param name="portName">Name of the port.</param>
        /// <param name="baudRate">The baud rate.</param>
        internal UartPort(string portName, UartRate baudRate)
        {
            Handle = Uart.SerOpen(portName, baudRate);
            BaudRate = (int)baudRate;
            PortName = portName;
        }

        /// <summary>
        /// Gets the serial port handle.
        /// </summary>
        public UIntPtr Handle { get; private set; }

        /// <summary>
        /// Gets the baud rate.
        /// </summary>
        public int BaudRate { get; }

        /// <summary>
        /// Gets the name of the port.
        /// </summary>
        public string PortName { get; }

        /// <summary>
        /// Gets the number of available bytes to read in the hardware buffer.
        /// </summary>
        public int Available => BoardException.ValidateResult(Uart.SerDataAvailable(Handle));

        /// <summary>
        /// Reads the byte.
        /// </summary>
        /// <returns>The byte value. Null if no bytes were read.</returns>
        public byte? ReadByte()
        {
            var result = Uart.SerReadByte(Handle);
            if (result >= 0)
                return (byte)result;

            return default;
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns>A buffer containing the bytes.</returns>
        public byte[] Read() => Read(Available);

        /// <summary>
        /// Reads the specified number of bytes.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>A byte array of read bytes.</returns>
        public byte[] Read(int count)
        {
            var buffer = new byte[count];
            var result = BoardException.ValidateResult(Uart.SerRead(Handle, buffer, (uint)count));
            if (result == 0) return Array.Empty<byte>();
            if (result == count)
                return buffer;

            var output = new byte[result];
            Buffer.BlockCopy(buffer, 0, output, 0, result);
            return output;
        }

        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Write(byte value) =>
            BoardException.ValidateResult(Uart.SerWriteByte(Handle, value));

        /// <summary>
        /// Writes the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="count">The count.</param>
        public void Write(byte[] buffer, int count) =>
            BoardException.ValidateResult(Uart.SerWrite(Handle, buffer, (uint)count));

        /// <summary>
        /// Writes the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public void Write(byte[] buffer) =>
            Write(buffer, buffer.Length);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() =>
            Dispose(true);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="alsoManaged"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool alsoManaged)
        {
            if (IsDisposed) return;
            IsDisposed = true;

            if (alsoManaged)
            {
                Uart.SerClose(Handle);
                Handle = UIntPtr.Zero;
            }
        }
    }
}
