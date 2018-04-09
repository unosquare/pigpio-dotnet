namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a peripheral connected via the I2C/SM bus.
    /// </summary>
    /// <seealso cref="IDisposable" />
    public sealed class I2cDevice : IDisposable
    {
        /// <summary>
        /// The default bus is I2C bus 1.
        /// </summary>
        internal const I2cBusId DefaultBus = I2cBusId.Bus1;

        private bool IsDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="I2cDevice" /> class.
        /// </summary>
        /// <param name="bus">The bus.</param>
        /// <param name="address">The address.</param>
        internal I2cDevice(I2cBusId bus, byte address)
        {
            Handle = I2c.I2cOpen(Convert.ToUInt32(bus), address);
            Address = address;
        }

        /// <summary>
        /// This sets the I2C (i2c-bcm2708) module "use combined transactions"
        /// parameter on or off.
        ///
        /// NOTE: when the flag is on a write followed by a read to the same
        /// slave address will use a repeated start (rather than a stop/start).
        /// </summary>
        public static bool UseCombinedTransactions
        {
            set
            {
                I2c.I2cSwitchCombined(value ? 1 : 0);
            }
        }

        /// <summary>
        /// Gets the devide address on the bus.
        /// </summary>
        public byte Address { get; }

        /// <summary>
        /// Gets the device handle.
        /// </summary>
        public UIntPtr Handle { get; private set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => Dispose(true);

        /// <summary>
        /// This sends a single bit (in the Rd/Wr bit) to the device associated
        /// with handle.
        /// </summary>
        /// <param name="mode">The mode (write is 0, read is 1)</param>
        public void SetMode(I2cQuickMode mode) => BoardException.ValidateResult(I2c.I2cWriteQuick(Handle, mode));

        /// <summary>
        /// This sends a single byte to the device associated with handle.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Write(byte value) => BoardException.ValidateResult(I2c.I2cWriteByte(Handle, value));

        /// <summary>
        /// This writes a single byte to the specified register of the device
        /// associated with handle.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="value">The value.</param>
        public void Write(byte register, byte value) => BoardException.ValidateResult(I2c.I2cWriteByteData(Handle, register, value));

        /// <summary>
        /// This writes a single 16 bit word to the specified register of the device
        /// associated with handle.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="value">The value.</param>
        public void Write(byte register, ushort value) => BoardException.ValidateResult(I2c.I2cWriteWordData(Handle, register, value));

        /// <summary>
        /// This writes a single 16 bit word to the specified register of the device
        /// associated with handle.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="value">The value.</param>
        public void Write(byte register, short value) => BoardException.ValidateResult(I2c.I2cWriteWordData(Handle, register, unchecked((ushort)value)));

        /// <summary>
        /// This writes up to 32 bytes to the specified register of the device
        /// associated with handle.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="buffer">The buffer.</param>
        public void Write(byte register, byte[] buffer) => BoardException.ValidateResult(I2c.I2cWriteBlockData(Handle, register, buffer));

        /// <summary>
        /// This writes 1 to 32 bytes to the specified register of the device
        /// associated with handle.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="count">The count.</param>
        public void Write(byte register, byte[] buffer, int count) => BoardException.ValidateResult(I2c.I2cWriteI2cBlockData(Handle, register, buffer, count));

        /// <summary>
        /// This writes count bytes from buf to the raw device.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public void WriteRaw(byte[] buffer) => BoardException.ValidateResult(I2c.I2cWriteDevice(Handle, buffer));

        /// <summary>
        /// This reads a single byte from the device associated with handle.
        /// </summary>
        /// <returns>The value read</returns>
        public byte ReadByte() => I2c.I2cReadByte(Handle);

        /// <summary>
        /// This reads a single byte from the specified register of the device
        /// associated with handle.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <returns>The read value</returns>
        public byte ReadByte(byte register) => I2c.I2cReadByteData(Handle, register);

        /// <summary>
        /// This reads a single 16 bit word from the specified register of the device
        /// associated with handle.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <returns>The word data</returns>
        public ushort ReadWord(byte register) => I2c.I2cReadWordData(Handle, register);

        /// <summary>
        /// This reads a block of up to 32 bytes from the specified register of
        /// the device associated with handle.
        /// The amount of returned data is set by the device.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <returns>The data read from the device</returns>
        public byte[] ReadBlock(byte register) => I2c.I2cReadBlockData(Handle, register);

        /// <summary>
        /// This reads count bytes from the specified register of the device
        /// associated with handle .  The count may be 1-32.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="count">The count.</param>
        /// <returns>The data read from the device</returns>
        public byte[] ReadBlock(byte register, int count) => I2c.I2cReadI2cBlockData(Handle, register, count);

        /// <summary>
        /// This reads count bytes from the raw device into buf.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>The raw byte data</returns>
        public byte[] ReadRaw(int count) => I2c.I2cReadDevice(Handle, count);

        /// <summary>
        /// This writes data bytes to the specified register of the device
        /// associated with handle and reads a device specified number
        /// of bytes of data in return.
        ///
        /// The SMBus 2.0 documentation states that a minimum of 1 byte may be
        /// sent and a minimum of 1 byte may be received.  The total number of
        /// bytes sent/received must be 32 or less.
        /// </summary>
        /// <param name="register">The register.</param>
        /// <param name="buffer">The buffer.</param>
        /// <returns>The bytes that were read</returns>
        public byte[] Transfer(byte register, byte[] buffer)
        {
            if (buffer.Length == 2)
            {
                var word = I2c.I2cProcessCall(Handle, register, BitConverter.ToUInt16(buffer, 0));
                return BitConverter.GetBytes(word);
            }

            var output = new byte[buffer.Length];
            Buffer.BlockCopy(buffer, 0, output, 0, buffer.Length);
            var count = BoardException.ValidateResult(I2c.I2cBlockProcessCall(Handle, register, output));
            if (count == output.Length)
                return output;

            var result = new byte[count];
            Buffer.BlockCopy(output, 0, result, 0, count);
            return result;
        }

        /// <summary>
        /// Scans the bus for available devices.
        /// </summary>
        /// <param name="bus">The bus.</param>
        /// <returns>The devices that were found on the given bus</returns>
        internal static byte[] ScanBus(I2cBusId bus)
        {
            const byte startAddress = 0;
            const byte endAddress = 127; // MSB is reserved for read/write bit
            var result = new List<byte>();

            for (var address = startAddress; address <= endAddress; address++)
            {
                try
                {
                    using (var device = new I2cDevice(bus, address))
                    {
                        if ((address >= 0x30 && address <= 0x37) ||
                            (address >= 0x50 && address <= 0x5f))
                        {
                            // Quick Mode detection
                            // device.ReadByte();
                            device.SetMode(I2cQuickMode.Write);
                        }
                        else
                        {
                            // read byte detection
                            device.ReadByte();
                        }
                    }

                    result.Add(address);
                }
                catch
                {
                    // swallow
                }
            }

            return result.ToArray();
        }

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
                if (Handle != UIntPtr.Zero)
                    I2c.I2cClose(Handle);

                Handle = UIntPtr.Zero;
            }
        }
    }
}
