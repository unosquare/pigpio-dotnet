using Swan.DependencyInjection;
using Unosquare.PiGpio.NativeMethods.Interfaces;
using Unosquare.PiGpio.NativeMethods.Pipe;

namespace Unosquare.PiGpio.CommsStrategies
{
    public class PipeCommsStrategy : IPiGpioCommsStrategy
    {
        /// <inheritdoc />
        public CommsStrategy CommsStrategy => CommsStrategy.Pipe;

        public void RegisterServices()
        {
            DependencyContainer.Current.Register<IIOService>(new IOServicePipe());
        }

        public bool Initialize()
        {
            return true;
        }
    }
}