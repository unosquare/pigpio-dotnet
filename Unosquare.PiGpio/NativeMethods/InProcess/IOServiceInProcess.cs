namespace Unosquare.PiGpio.NativeMethods.InProcess
{
    using System;
    using DllImports;
    using Interfaces;
    using NativeEnums;
    using NativeTypes;

    /// <summary>
    /// IO Service In Process strategy pattern implementation.
    /// </summary>
    public class IOServiceInProcess : IIOService
    {
        /// <inheritdoc />
        public ControllerMode Mode => ControllerMode.InProcess;

        /// <inheritdoc />
        public bool IsConnected { get; }

        /// <inheritdoc />
        public ResultCode GpioSetMode(SystemGpio gpio, PigpioPinMode mode)
        {
            return PiIO.GpioSetMode(gpio, mode);
        }

        /// <inheritdoc />
        public PigpioPinMode GpioGetMode(SystemGpio gpio)
        {
            return PiIO.GpioGetMode(gpio);
        }

        /// <inheritdoc />
        public ResultCode GpioSetPullUpDown(SystemGpio gpio, GpioPullMode pullMode)
        {
            return PiIO.GpioSetPullUpDown(gpio, pullMode);
        }

        /// <inheritdoc />
        public bool GpioRead(SystemGpio gpio)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioWrite(SystemGpio gpio, bool value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioTrigger(UserGpio userGpio, uint pulseLength, bool value)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public uint GpioReadBits00To31()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public uint GpioReadBits32To53()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioWriteBits00To31Clear(BitMask bits)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioWriteBits32To53Clear(BitMask bits)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioWriteBits00To31Set(BitMask bits)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioWriteBits32To53Set(BitMask bits)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetAlertFunc(UserGpio userGpio, PiGpioAlertDelegate callback)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetAlertFuncEx(UserGpio userGpio, PiGpioAlertExDelegate callback, UIntPtr userData)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetWatchdog(UserGpio userGpio, uint timeoutMilliseconds)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetIsrFuncEx(SystemGpio gpio, EdgeDetection edge, int timeout, PiGpioIsrExDelegate callback, UIntPtr userData)
        {
            return PiIO.GpioSetIsrFuncEx(gpio, edge, timeout, callback, userData);
        }

        /// <inheritdoc />
        public ResultCode GpioSetIsrFunc(SystemGpio gpio, EdgeDetection edge, int timeout, PiGpioIsrDelegate callback)
        {
            return PiIO.GpioSetIsrFunc(gpio, edge, timeout, callback);
        }

        /// <inheritdoc />
        public ResultCode GpioSetGetSamplesFuncEx(PiGpioGetSamplesExDelegate callback, BitMask bits, UIntPtr userData)
        {
            return PiIO.GpioSetGetSamplesFuncEx(callback, bits, userData);
        }

        /// <inheritdoc />
        public ResultCode GpioSetGetSamplesFunc(PiGpioGetSamplesDelegate callback, BitMask bits)
        {
            return PiIO.GpioSetGetSamplesFunc(callback, bits);
        }

        /// <inheritdoc />
        public ResultCode GpioGlitchFilter(UserGpio userGpio, uint steadyMicroseconds)
        {
            return PiIO.GpioGlitchFilter(userGpio, steadyMicroseconds);
        }

        /// <inheritdoc />
        public ResultCode GpioNoiseFilter(UserGpio userGpio, uint steadyMicroseconds, uint activeMicroseconds)
        {
            return PiIO.GpioNoiseFilter(userGpio, steadyMicroseconds, activeMicroseconds);
        }

        /// <inheritdoc />
        public GpioPadStrength GpioGetPad(GpioPadId pad)
        {
            return PiIO.GpioGetPad(pad);
        }

        /// <inheritdoc />
        public ResultCode GpioSetPad(GpioPadId pad, GpioPadStrength padStrength)
        {
            return PiIO.GpioSetPad(pad, padStrength);
        }

        /// <inheritdoc />
        public int GpioReadUnmanaged(SystemGpio gpio)
        {
            return PiIO.GpioReadUnmanaged(gpio);
        }

        /// <inheritdoc />
        public ResultCode GpioWriteUnmanaged(SystemGpio gpio, DigitalValue value)
        {
            return PiIO.GpioWriteUnmanaged(gpio, value);
        }

        /// <inheritdoc />
        public int GpioGetPadUnmanaged(GpioPadId pad)
        {
            return PiIO.GpioGetPadUnmanaged(pad);
        }

        /// <inheritdoc />
        public int GpioGetModeUnmanaged(SystemGpio gpio)
        {
            return PiIO.GpioGetModeUnmanaged(gpio);
        }

        /// <inheritdoc />
        public ResultCode GpioTriggerUnmanaged(UserGpio userGpio, uint pulseLength, DigitalValue value)
        {
            return PiIO.GpioTriggerUnmanaged(userGpio, pulseLength, value);
        }
    }
}