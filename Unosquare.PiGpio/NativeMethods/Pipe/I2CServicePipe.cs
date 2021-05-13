namespace Unosquare.PiGpio.NativeMethods.Pipe
{
    using System;
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure;
    using Unosquare.PiGpio.NativeTypes;

    /// <summary>
    /// Waves Service Pipe strategy pattern.
    /// </summary>
    internal class I2CServicePipe : II2CService
    {
        private readonly PigpioPipe _pigpioPipe;

        /// <inheritdoc/>
        public I2CServicePipe(PigpioPipe pipe)
        {
            _pigpioPipe = pipe;
        }

        /// <inheritdoc/>
        public UIntPtr I2cOpen(uint i2cBus, uint i2cAddress)
        {
            var handle = _pigpioPipe.SendCommandWithUIntResult($"i2co {i2cBus} 0x{Convert.ToString(i2cAddress, 16)} 0");
            return new UIntPtr(handle);
        }

        /// <inheritdoc/>
        public ResultCode I2cClose(UIntPtr handle)
        {
            return _pigpioPipe.SendCommandWithResultCode($"i2cc {handle.ToUInt32()}");
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteQuick(UIntPtr handle, bool bit)
        {
            return _pigpioPipe.SendCommandWithResultCode($"i2cwq {handle.ToUInt32()} {(bit ? 1 : 0)}");
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteByte(UIntPtr handle, byte value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public byte I2cReadByte(UIntPtr handle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteByteData(UIntPtr handle, byte register, byte value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteWordData(UIntPtr handle, byte register, ushort word)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public byte I2cReadByteData(UIntPtr handle, byte register)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ushort I2cReadWordData(UIntPtr handle, byte register)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ushort I2cProcessCall(UIntPtr handle, byte register, ushort word)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteBlockData(UIntPtr handle, byte register, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public byte[] I2cReadBlockData(UIntPtr handle, byte register)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public uint I2cBlockProcessCall(UIntPtr handle, byte register, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public byte[] I2cReadI2cBlockData(UIntPtr handle, byte register, int count)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteI2cBlockData(UIntPtr handle, byte register, byte[] buffer, int count)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public uint I2cReadDevice(UIntPtr handle, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public byte[] I2cReadDevice(UIntPtr handle, int count)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteDevice(UIntPtr handle, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void I2cSwitchCombined(int setting)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int I2cSegments(UIntPtr handle, I2CMessageSegment[] segments, uint numSegs)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int I2cZip(UIntPtr handle, byte[] inputBuffer, uint inputLength, byte[] outputBuffer, uint outLength)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ResultCode BbI2COpen(UserGpio sdaPin, UserGpio sclPin, uint baudRate)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ResultCode BbI2CClose(UserGpio sdaPin)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public int BbI2CZip(UserGpio sdaPin, byte[] inputBuffer, uint inputLength, byte[] outputBuffer, uint outputLength)
        {
            throw new NotImplementedException();
        }
    }
}