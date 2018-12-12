namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;

    /// <summary>
    /// Provides a software based (bit-banged) I2C bus on 2 pins.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public sealed class SoftI2cBus : IDisposable
    {
        /// <summary>
        /// The default baud rate for a software-based I2C bus
        /// Baud rate can go up to 500kbits per second.
        /// </summary>
        public const int DefaultBaudRate = 100000; // 100kbits per second

        /// <summary>
        /// To detect redundant calls.
        /// </summary>
        private bool _isDisposed; // To detect redundant calls

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftI2cBus"/> class.
        /// </summary>
        /// <param name="dataPin">The data pin.</param>
        /// <param name="clockPin">The clock pin.</param>
        /// <param name="baudRate">The baud rate.</param>
        internal SoftI2cBus(GpioPin dataPin, GpioPin clockPin, int baudRate)
        {
            BoardException.ValidateResult(I2c.BbI2COpen((UserGpio)dataPin.PinNumber, (UserGpio)clockPin.PinNumber, Convert.ToUInt32(baudRate)));
            Handle = (UserGpio)dataPin.PinNumber;
            DataPin = dataPin;
            ClockPin = clockPin;
            BaudRate = baudRate;
        }

        /// <summary>
        /// Gets or the I2C bus handle. This points to the SDA (data) pin of the I2C bus.
        /// </summary>
        public UserGpio Handle { get; }

        /// <summary>
        /// Gets the data pin.
        /// </summary>
        public GpioPin DataPin { get; }

        /// <summary>
        /// Gets the clock pin.
        /// </summary>
        public GpioPin ClockPin { get; }

        /// <summary>
        /// Gets the baud rate.
        /// </summary>
        public int BaudRate { get; }

        /// <summary>
        /// Writes data to the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="buffer">The buffer. Recommended 32 bytes max.</param>
        public void Write(byte address, byte[] buffer)
        {
            var data = new byte[7 + buffer.Length];
            data[0] = 0x04; // Set Address
            data[1] = address; // Address Literal
            data[2] = 0x02; // Start Condition
            data[3] = 0x07; // Write Command
            data[4] = Convert.ToByte(data.Length); // data length;
            data[data.Length - 2] = 0x03; // Stop condition
            data[data.Length - 1] = 0x00; // End command;

            // copy buffer data to command
            Buffer.BlockCopy(buffer, 0, data, 5, data.Length);

            var output = new byte[32];
            BoardException.ValidateResult(
                I2c.BbI2CZip(Handle, data, Convert.ToUInt32(data.Length), output, Convert.ToUInt32(output.Length)));
        }

        /// <summary>
        /// Reads data from the specified address.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="count">The count. Recommended 32 as maximum.</param>
        /// <returns>The byte array that was read.</returns>
        public byte[] Read(byte address, int count)
        {
            var data = new byte[7];
            data[0] = 0x04; // Set Address
            data[1] = address; // Address Literal
            data[2] = 0x02; // Start Condition
            data[3] = 0x06; // Write Command
            data[4] = Convert.ToByte(count); // data length;
            data[5] = 0x03; // Stop condition
            data[6] = 0x00; // End command;

            var output = new byte[count];
            var outCount = BoardException.ValidateResult(
                I2c.BbI2CZip(Handle, data, Convert.ToUInt32(data.Length), output, Convert.ToUInt32(output.Length)));

            if (output.Length == outCount)
                return output;

            var result = new byte[outCount];
            Buffer.BlockCopy(output, 0, result, 0, outCount);
            return result;
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
                I2c.BbI2CClose(Handle);
            }
        }
    }
}
