using System;

namespace Unosquare.PiGpio
{
    using ManagedModel;
    using RaspberryIO.Abstractions;

    /// <inheritdoc />
    public class I2CDevice : II2CDevice
    {
        private readonly I2cDevice _inner;

        internal I2CDevice(I2cDevice inner)
        {
            _inner = inner;
        }

        /// <inheritdoc />
        public byte Read() => _inner.ReadByte();

        /// <inheritdoc />
        public byte[] Read(int length) => _inner.ReadRaw(length);

        /// <inheritdoc />
        public void Write(byte data) => _inner.Write(data);

        /// <inheritdoc />
        public void Write(byte[] data) => _inner.Write(data);

        /// <inheritdoc />
        public void WriteAddressByte(int address, byte data) => _inner.Write((byte) address, data);

        /// <inheritdoc />
        public void WriteAddressWord(int address, ushort data) => _inner.Write((byte) address, data);

        /// <inheritdoc />
        public byte ReadAddressByte(int address) => _inner.ReadByte((byte) address);

        /// <inheritdoc />
        public ushort ReadAddressWord(int address) => _inner.ReadWord((byte) address);

        /// <inheritdoc />
        public int DeviceId => _inner.Address;

        /// <inheritdoc />
        public int FileDescriptor => throw new NotImplementedException();
    }
}