namespace Unosquare.PiGpio.CommsStrategies
{
    using Swan.DependencyInjection;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeMethods.Pipe;
    using Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure;

    public class PipeCommsStrategy : IPiGpioCommsStrategy
    {
        /// <inheritdoc />
        public CommsStrategy CommsStrategy => CommsStrategy.Pipe;

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Reference held by services")]
        public void RegisterServices()
        {
            var pipe = new PigpioPipe();
            DependencyContainer.Current.Register<IIOService>(new IOServicePipe(pipe));
            DependencyContainer.Current.Register<IThreadsService>(new ThreadsServicePipe(pipe));
            DependencyContainer.Current.Register<IUtilityService>(new UtilityServicePipe(pipe));
        }

        /// <inheritdoc />
        public bool Initialize()
        {
            return true;
        }
    }
}