using Unosquare.PiGpio.NativeMethods.InProcess.DllImports;

namespace Unosquare.PiGpio.NativeMethods.InProcess
{
    using System;
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeTypes;

    /// <summary>
    /// I2C Service In Process strategy pattern.
    /// </summary>
    internal class I2CServiceInProcess : II2CService
    {
        /// <inheritdoc/>
        public UIntPtr I2cOpen(uint i2cBus, uint i2cAddress)
        {
            return I2c.I2cOpen(i2cBus, i2cAddress);
        }

        /// <inheritdoc/>
        public ResultCode I2cClose(UIntPtr handle)
        {
            return I2c.I2cClose(handle);
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteQuick(UIntPtr handle, I2cQuickMode bit)
        {
            return I2c.I2cWriteQuick(handle, bit);
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteByte(UIntPtr handle, byte value)
        {
            return I2c.I2cWriteByte(handle, value);
        }

        /// <inheritdoc/>
        public byte I2cReadByte(UIntPtr handle)
        {
            return I2c.I2cReadByte(handle);
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteByteData(UIntPtr handle, byte register, byte value)
        {
            return I2c.I2cWriteByteData(handle, register, value);
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteWordData(UIntPtr handle, byte register, ushort word)
        {
            return I2c.I2cWriteWordData(handle, register, word);
        }

        /// <inheritdoc/>
        public byte I2cReadByteData(UIntPtr handle, byte register)
        {
            return I2c.I2cReadByteData(handle, register);
        }

        /// <inheritdoc/>
        public ushort I2cReadWordData(UIntPtr handle, byte register)
        {
            return I2c.I2cReadWordData(handle, register);
        }

        /// <inheritdoc/>
        public ushort I2cProcessCall(UIntPtr handle, byte register, ushort word)
        {
            return I2c.I2cProcessCall(handle, register, word);
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteBlockData(UIntPtr handle, byte register, byte[] buffer)
        {
            return I2c.I2cWriteBlockData(handle, register, buffer);
        }

        /// <inheritdoc/>
        public byte[] I2cReadBlockData(UIntPtr handle, byte register)
        {
            return I2c.I2cReadBlockData(handle, register);
        }

        /// <inheritdoc/>
        public int I2cBlockProcessCall(UIntPtr handle, byte register, byte[] buffer)
        {
            return I2c.I2cBlockProcessCall(handle, register, buffer);
        }

        /// <inheritdoc/>
        public byte[] I2cReadI2cBlockData(UIntPtr handle, byte register, int count)
        {
            return I2c.I2cReadI2cBlockData(handle, register, count);
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteI2cBlockData(UIntPtr handle, byte register, byte[] buffer, int count)
        {
            return I2c.I2cWriteI2cBlockData(handle, register, buffer, count);
        }

        /// <inheritdoc/>
        public int I2cReadDevice(UIntPtr handle, byte[] buffer)
        {
            return I2c.I2cReadDevice(handle, buffer);
        }

        /// <inheritdoc/>
        public byte[] I2cReadDevice(UIntPtr handle, int count)
        {
            return I2c.I2cReadDevice(handle, count);
        }

        /// <inheritdoc/>
        public ResultCode I2cWriteDevice(UIntPtr handle, byte[] buffer)
        {
            return I2c.I2cWriteDevice(handle, buffer);
        }

        /// <inheritdoc/>
        public void I2cSwitchCombined(int setting)
        {
            I2c.I2cSwitchCombined(setting);
        }

        /// <inheritdoc/>
        public int I2cSegments(UIntPtr handle, I2CMessageSegment[] segments, uint numSegs)
        {
            return I2c.I2cSegments(handle, segments, numSegs);
        }

        /// <inheritdoc/>
        public int I2cZip(UIntPtr handle, byte[] inputBuffer, uint inputLength, byte[] outputBuffer, uint outLength)
        {
            return I2c.I2cZip(handle, inputBuffer, inputLength, outputBuffer, outLength);
        }

        /// <inheritdoc/>
        public ResultCode BbI2COpen(UserGpio sdaPin, UserGpio sclPin, uint baudRate)
        {
            return I2c.BbI2COpen(sdaPin, sclPin, baudRate);
        }

        /// <inheritdoc/>
        public ResultCode BbI2CClose(UserGpio sdaPin)
        {
            return I2c.BbI2CClose(sdaPin);
        }

        /// <inheritdoc/>
        public int BbI2CZip(UserGpio sdaPin, byte[] inputBuffer, uint inputLength, byte[] outputBuffer, uint outputLength)
        {
            return I2c.BbI2CZip(sdaPin, inputBuffer, inputLength, outputBuffer, outputLength);
        }
    }
}