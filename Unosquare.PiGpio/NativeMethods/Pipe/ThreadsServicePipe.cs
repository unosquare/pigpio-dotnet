namespace Unosquare.PiGpio.NativeMethods.Pipe
{
    using System;
    using System.Threading;
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure;
    using Unosquare.PiGpio.NativeTypes;

    /// <summary>
    /// IO Service Pipe strategy pattern.
    /// </summary>
    internal class ThreadsServicePipe : IThreadsService
    {
        private readonly IPigpioPipe _pigpioPipe;

        internal ThreadsServicePipe(IPigpioPipe pipe)
        {
            _pigpioPipe = pipe;
        }

        /// <inheritdoc />
        public ResultCode GpioSetTimerFuncEx(TimerId timer, uint millisecondsTimeout, PiGpioTimerExDelegate callback, UIntPtr userData)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSetTimerFunc(TimerId timer, uint periodMilliseconds, PiGpioTimerDelegate callback)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public UIntPtr GpioStartThread(PiGpioThreadDelegate callback, UIntPtr userData)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void GpioStopThread(UIntPtr handle)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public uint GpioDelay(uint micros)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSleep(TimeType timeType, int seconds, int micros)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void TimeSleep(double seconds)
        {
            Thread.Sleep(Convert.ToInt32(1000*seconds));
        }
    }
}