namespace Unosquare.PiGpio.NativeMethods.InProcess
{
    using System;
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.InProcess.DllImports;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeTypes;

    /// <summary>
    /// Utility Service In Process strategy pattern implementation.
    /// </summary>
    public class UtilityServiceInProcess : IUtilityService
    {
        /// <inheritdoc />
        public uint GpioHardwareRevision()
        {
            return Utilities.GpioHardwareRevision();
        }

        /// <inheritdoc />
        public uint GpioVersion()
        {
            return Utilities.GpioVersion();
        }

        /// <inheritdoc />
        public uint GpioTick()
        {
            return Utilities.GpioTick();
        }

        /// <inheritdoc />
        public int GpioTime(TimeType timeType, out int seconds, out int microseconds)
        {
            return Utilities.GpioTime(timeType, out seconds, out microseconds);
        }

        /// <inheritdoc />
        public double TimeTime()
        {
            return Utilities.TimeTime();
        }

        /// <inheritdoc />
        public int GetBitInBytes(int bitPos, byte[] buf, int numBits)
        {
            return Utilities.GetBitInBytes(bitPos, buf, numBits);
        }

        /// <inheritdoc />
        public void PutBitInBytes(int bitPos, byte[] buf, int bit)
        {
            Utilities.PutBitInBytes(bitPos, buf, bit);
        }

        /// <inheritdoc />
        public ResultCode Shell(string scriptName, string scriptString)
        {
            return Utilities.Shell(scriptName, scriptString);
        }

        /// <inheritdoc />
        public ResultCode GpioSetSignalFunc(uint signalNumber, PiGpioSignalDelegate f)
        {
            return Utilities.GpioSetSignalFunc(signalNumber, f);
        }

        /// <inheritdoc />
        public ResultCode GpioSetSignalFuncEx(uint signalNumber, PiGpioSignalExDelegate callback, UIntPtr userData)
        {
            return Utilities.GpioSetSignalFuncEx(signalNumber, callback, userData);
        }

        /// <inheritdoc />
        public int RaiseSignal(int signalNumber)
        {
            return Utilities.RaiseSignal(signalNumber);
        }
    }
}