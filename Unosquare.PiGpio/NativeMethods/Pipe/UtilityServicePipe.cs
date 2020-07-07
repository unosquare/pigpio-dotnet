namespace Unosquare.PiGpio.NativeMethods.Pipe
{
    using System;
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure;
    using Unosquare.PiGpio.NativeTypes;

    /// <summary>
    /// Utility Service Pipe strategy pattern.
    /// </summary>
    internal class UtilityServicePipe : IUtilityService
    {
        private readonly IPigpioPipe _pigpioPipe;

        internal UtilityServicePipe(IPigpioPipe pipe)
        {
            _pigpioPipe = pipe;
        }

        public uint GpioHardwareRevision()
        {
            return _pigpioPipe.SendCommandWithUIntResult("hwver");
        }

        public uint GpioVersion()
        {
            return _pigpioPipe.SendCommandWithUIntResult("pigpv");
        }

        public uint GpioTick()
        {
            return _pigpioPipe.SendCommandWithUIntResult("t");
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
            if (bitPos > numBits)
            {
                return 0;
            }

            var index = bitPos / 8;
            var mask = 1 << (bitPos % 8);
            return ((buf[index] & mask) > 0) ? 1 : 0;
        }

        public void PutBitInBytes(int bitPos, byte[] buf, int bit)
        {
            var index = bitPos / 8;
            byte mask = (byte)(1 << (bitPos % 8));
            if (bit == 0)
            {
                var reset = 0xff ^ mask; // invert the mask with xor
                buf[index] = (byte)(buf[index] & reset);
            }
            else 
            {
                buf[index] = (byte)(buf[index] | mask);
            }
        }

        public ResultCode Shell(string scriptName, string scriptString)
        {
            return _pigpioPipe.SendCommandWithResultCode($"shell {scriptName} {scriptString}");
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