using Unosquare.PiGpio.NativeEnums;

namespace Unosquare.PiGpio
{
    using RaspberryIO.Abstractions;
    using Swan.DependencyInjection;

    /// <summary>
    /// Represents the bootstrap process to use PiGPio.
    /// </summary>
    /// <seealso cref="IBootstrap" />
    public class BootstrapPiGpio : IBootstrap
    {
        private static readonly object SyncLock = new object();

        /// <inheritdoc />
        public void Bootstrap()
        {
            lock (SyncLock)
            {

                Resources.EmbeddedResources.ExtractAll();

                DependencyContainer.Current.Register<IGpioController>(new GpioController(ControllerMode.Direct));
                DependencyContainer.Current.Register<ISpiBus>(new SpiBus());
                DependencyContainer.Current.Register<II2CBus>(new I2CBus());
                DependencyContainer.Current.Register<ISystemInfo>(new SystemInfo());
                DependencyContainer.Current.Register<ITiming>(new Timing());
                DependencyContainer.Current.Register<IThreading>(new PigpioThreading());
            }
        }
    }
}
