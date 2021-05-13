namespace Unosquare.PiGpio.NativeMethods.InProcess
{
    using System;
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.InProcess.DllImports;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeTypes;

    /// <summary>
    /// IO Service Pipe strategy pattern.
    /// </summary>
    public class ThreadsServiceInProcess : IThreadsService
    {
        /// <inheritdoc />
        public ResultCode GpioSetTimerFuncEx(TimerId timer, uint millisecondsTimeout, PiGpioTimerExDelegate callback, UIntPtr userData)
        {
            return Threads.GpioSetTimerFuncEx(timer, millisecondsTimeout, callback, userData);
        }

        /// <inheritdoc />
        public ResultCode GpioSetTimerFunc(TimerId timer, uint periodMilliseconds, PiGpioTimerDelegate callback)
        {
            return Threads.GpioSetTimerFunc(timer, periodMilliseconds, callback);
        }

        /// <inheritdoc />
        public UIntPtr GpioStartThread(PiGpioThreadDelegate callback, UIntPtr userData)
        {
            return Threads.GpioStartThread(callback, userData);
        }

        /// <inheritdoc />
        public void GpioStopThread(UIntPtr handle)
        {
            Threads.GpioStopThread(handle);
        }

        /// <inheritdoc />
        public uint GpioDelay(uint micros)
        {
            return Threads.GpioDelay(micros);
        }

        /// <inheritdoc />
        public ResultCode GpioSleep(TimeType timeType, int seconds, int micros)
        {
            return Threads.GpioSleep(timeType, seconds, micros);
        }

        /// <inheritdoc />
        public void TimeSleep(double seconds)
        {
            Threads.TimeSleep(seconds);
        }
    }
}