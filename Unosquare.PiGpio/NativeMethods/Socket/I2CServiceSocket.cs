namespace Unosquare.PiGpio.NativeMethods.Socket
{
    using System;
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeTypes;

    /// <summary>
    /// I2C Service Socket strategy pattern.
    /// </summary>
    internal class I2CServiceSocket : II2CService
    {
        public UIntPtr I2cOpen(uint i2cBus, uint i2cAddress)
        {
            throw new NotImplementedException();
        }

        public ResultCode I2cClose(UIntPtr handle)
        {
            throw new NotImplementedException();
        }

        public ResultCode I2cWriteQuick(UIntPtr handle, bool bit)
        {
            throw new NotImplementedException();
        }

        public ResultCode I2cWriteByte(UIntPtr handle, byte value)
        {
            throw new NotImplementedException();
        }

        public byte I2cReadByte(UIntPtr handle)
        {
            throw new NotImplementedException();
        }

        public ResultCode I2cWriteByteData(UIntPtr handle, byte register, byte value)
        {
            throw new NotImplementedException();
        }

        public ResultCode I2cWriteWordData(UIntPtr handle, byte register, ushort word)
        {
            throw new NotImplementedException();
        }

        public byte I2cReadByteData(UIntPtr handle, byte register)
        {
            throw new NotImplementedException();
        }

        public ushort I2cReadWordData(UIntPtr handle, byte register)
        {
            throw new NotImplementedException();
        }

        public ushort I2cProcessCall(UIntPtr handle, byte register, ushort word)
        {
            throw new NotImplementedException();
        }

        public ResultCode I2cWriteBlockData(UIntPtr handle, byte register, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public byte[] I2cReadBlockData(UIntPtr handle, byte register)
        {
            throw new NotImplementedException();
        }

        public int I2cBlockProcessCall(UIntPtr handle, byte register, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public byte[] I2cReadI2cBlockData(UIntPtr handle, byte register, int count)
        {
            throw new NotImplementedException();
        }

        public ResultCode I2cWriteI2cBlockData(UIntPtr handle, byte register, byte[] buffer, int count)
        {
            throw new NotImplementedException();
        }

        public int I2cReadDevice(UIntPtr handle, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public byte[] I2cReadDevice(UIntPtr handle, int count)
        {
            throw new NotImplementedException();
        }

        public ResultCode I2cWriteDevice(UIntPtr handle, byte[] buffer)
        {
            throw new NotImplementedException();
        }

        public void I2cSwitchCombined(int setting)
        {
            throw new NotImplementedException();
        }

        public int I2cSegments(UIntPtr handle, I2CMessageSegment[] segments, uint numSegs)
        {
            throw new NotImplementedException();
        }

        public int I2cZip(UIntPtr handle, byte[] inputBuffer, uint inputLength, byte[] outputBuffer, uint outLength)
        {
            throw new NotImplementedException();
        }

        public ResultCode BbI2COpen(UserGpio sdaPin, UserGpio sclPin, uint baudRate)
        {
            throw new NotImplementedException();
        }

        public ResultCode BbI2CClose(UserGpio sdaPin)
        {
            throw new NotImplementedException();
        }

        public int BbI2CZip(UserGpio sdaPin, byte[] inputBuffer, uint inputLength, byte[] outputBuffer, uint outputLength)
        {
            throw new NotImplementedException();
        }
    }
}