namespace Unosquare.PiGpio.CommsStrategies
{
    using Swan.DependencyInjection;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeMethods.Socket;

    public class SocketCommsStrategy : IPiGpioCommsStrategy
    {
        /// <inheritdoc />
        public CommsStrategy CommsStrategy => CommsStrategy.Socket;

        /// <inheritdoc />
        public void RegisterServices()
        {
            DependencyContainer.Current.Register<IIOService>(new IOServiceSocket());
            DependencyContainer.Current.Register<IThreadsService>(new ThreadsServiceSocket());
            DependencyContainer.Current.Register<IUtilityService>(new UtilityServiceSocket());

        }

        /// <inheritdoc />
        public bool Initialize()
        {
            return true;
        }
    }
}