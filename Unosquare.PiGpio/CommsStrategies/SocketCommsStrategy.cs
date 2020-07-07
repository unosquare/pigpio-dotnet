using Swan.DependencyInjection;
using Unosquare.PiGpio.NativeMethods.Interfaces;
using Unosquare.PiGpio.NativeMethods.Socket;

namespace Unosquare.PiGpio.CommsStrategies
{
    public class SocketCommsStrategy : IPiGpioCommsStrategy
    {
        /// <inheritdoc />
        public CommsStrategy CommsStrategy => CommsStrategy.Socket;

        public void RegisterServices()
        {
            DependencyContainer.Current.Register<IIOService>(new IOServiceSocket());
        }

        public bool Initialize()
        {
            return true;
        }
    }
}