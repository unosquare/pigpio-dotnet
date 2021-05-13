namespace Unosquare.PiGpio.NativeMethods.Socket
{
    using Interfaces;
    using NativeEnums;
    using NativeTypes;
    using System;

    /// <summary>
    /// Socket strategy pattern
    /// </summary>
    public class IOServiceSocket : IIOService
    {
        public ControllerMode Mode => ControllerMode.Socket;
        public bool IsConnected { get; }

        /// <inheritdoc />
        public ResultCode GpioSetMode(SystemGpio gpio, PigpioPinMode mode)
        {
            throw new NotImplementedException();
        }

        public PigpioPinMode GpioGetMode(SystemGpio gpio)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioSetPullUpDown(SystemGpio gpio, GpioPullMode pullMode)
        {
            throw new NotImplementedException();
        }

        public bool GpioRead(SystemGpio gpio)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioWrite(SystemGpio gpio, bool value)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioTrigger(UserGpio userGpio, uint pulseLength, bool value)
        {
            throw new NotImplementedException();
        }

        public uint GpioReadBits00To31()
        {
            throw new NotImplementedException();
        }

        public uint GpioReadBits32To53()
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioWriteBits00To31Clear(BitMask bits)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioWriteBits32To53Clear(BitMask bits)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioWriteBits00To31Set(BitMask bits)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioWriteBits32To53Set(BitMask bits)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioSetAlertFunc(UserGpio userGpio, PiGpioAlertDelegate callback)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioSetAlertFuncEx(UserGpio userGpio, PiGpioAlertExDelegate callback, UIntPtr userData)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioSetWatchdog(UserGpio userGpio, uint timeoutMilliseconds)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioSetIsrFunc(SystemGpio gpio, EdgeDetection edge, int timeout, PiGpioIsrDelegate callback)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioSetIsrFuncEx(SystemGpio gpio, EdgeDetection edge, int timeout, PiGpioIsrExDelegate callback,
            UIntPtr userData)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioSetGetSamplesFunc(PiGpioGetSamplesDelegate callback, BitMask bits)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioSetGetSamplesFuncEx(PiGpioGetSamplesExDelegate callback, BitMask bits, UIntPtr userData)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioGlitchFilter(UserGpio userGpio, uint steadyMicroseconds)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioNoiseFilter(UserGpio userGpio, uint steadyMicroseconds, uint activeMicroseconds)
        {
            throw new NotImplementedException();
        }

        public GpioPadStrength GpioGetPad(GpioPadId pad)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioSetPad(GpioPadId pad, GpioPadStrength padStrength)
        {
            throw new NotImplementedException();
        }

        public int GpioReadUnmanaged(SystemGpio gpio)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioWriteUnmanaged(SystemGpio gpio, DigitalValue value)
        {
            throw new NotImplementedException();
        }

        public int GpioGetPadUnmanaged(GpioPadId pad)
        {
            throw new NotImplementedException();
        }

        public int GpioGetModeUnmanaged(SystemGpio gpio)
        {
            throw new NotImplementedException();
        }

        public ResultCode GpioTriggerUnmanaged(UserGpio userGpio, uint pulseLength, DigitalValue value)
        {
            throw new NotImplementedException();
        }
    }
}