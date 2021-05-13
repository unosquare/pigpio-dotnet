using System;

namespace Unosquare.PiGpio.CommsStrategies
{
    using Swan.DependencyInjection;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeMethods.Pipe;
    using Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure;

    public class PipeCommsStrategy : IPiGpioCommsStrategy, IDisposable
    {
        private PigpioPipe _pipe;

        /// <inheritdoc />
        public CommsStrategy CommsStrategy => CommsStrategy.Pipe;

        /// <inheritdoc />
        public void RegisterServices()
        {
            _pipe = new PigpioPipe();
            DependencyContainer.Current.Register<IIOService>(new IOServicePipe(_pipe));
            DependencyContainer.Current.Register<IThreadsService>(new ThreadsServicePipe(_pipe));
            DependencyContainer.Current.Register<IUtilityService>(new UtilityServicePipe(_pipe));
            DependencyContainer.Current.Register<IPwmService>(new PwmServicePipe(_pipe));
            DependencyContainer.Current.Register<ISerialService>(new SerialServicePipe(_pipe));
            DependencyContainer.Current.Register<IWavesService>(new WavesServicePipe(_pipe));
            DependencyContainer.Current.Register<II2CService>(new I2CServicePipe(_pipe));
        }

        /// <inheritdoc />
        public bool Initialize()
        {
            return true;
        }

        /// <inheritdoc />
        public void Terminate()
        {
            _pipe.Dispose();
            _pipe = null;
        }

        public void Dispose()
        {
            ((IDisposable)_pipe)?.Dispose();
            _pipe = null;
        }
    }
}