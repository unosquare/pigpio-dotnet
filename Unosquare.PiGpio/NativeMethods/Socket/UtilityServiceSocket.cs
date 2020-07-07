namespace Unosquare.PiGpio.NativeMethods.Socket
{
    using System;
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeTypes;

    /// <summary>
    /// Utility Service Socket strategy pattern.
    /// </summary>
    internal class UtilityServiceSocket : IUtilityService
    {
        public uint GpioHardwareRevision()
        {
            throw new NotImplementedException();
        }

        public uint GpioVersion()
        {
            throw new NotImplementedException();
        }

        public uint GpioTick()
        {
            throw new NotImplementedException();
        }

        public int GpioTime(TimeType timeType, out int seconds, out int microseconds)
        {
            throw new NotImplementedException();
        }

        public double TimeTime()
        {
            throw new NotImplementedException();
        }

        public int GetBitInBytes(int bitPos, byte[] buf, int numBits)
        {
            throw new NotImplementedException();
        }

        public void PutBitInBytes(int bitPos, byte[] buf, int bit)
        {
            throw new NotImplementedException();
        }

        public ResultCode Shell(string scriptName, string scriptString)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioSetSignalFunc(uint signalNumber, PiGpioSignalDelegate f)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioSetSignalFuncEx(uint signalNumber, PiGpioSignalExDelegate callback, UIntPtr userData)
        {
            throw new NotImplementedException();
        }

        public int RaiseSignal(int signalNumber)
        {
            throw new NotImplementedException();
        }
    }
}