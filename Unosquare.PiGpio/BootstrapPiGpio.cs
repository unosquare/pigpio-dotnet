namespace Unosquare.PiGpio
{
    using System;
    using CommsStrategies;
    using RaspberryIO.Abstractions;
    using Swan.DependencyInjection;

    /// <summary>
    /// Represents the bootstrap process to use PiGPio.
    /// </summary>
    /// <typeparam name="TCommsStrategy">Represents the communication method to pigpio, in process or to the daemon.</typeparam>
    /// <seealso cref="IBootstrap" />
    public class BootstrapPiGpio<TCommsStrategy> : IBootstrap
        where TCommsStrategy : IPiGpioCommsStrategy
    {
        private readonly object _syncLock = new object();

        /// <inheritdoc />
        public void Bootstrap()
        {
            lock (_syncLock)
            {
                Resources.EmbeddedResources.ExtractAll();

                var commsStrategy = Activator.CreateInstance<TCommsStrategy>() as IPiGpioCommsStrategy;
                if (commsStrategy == null)
                {
                    throw new Exception("Invalid commsStrategy in bootstrap");
                }

                commsStrategy.RegisterServices();
                Board.IsAvailable = commsStrategy.Initialize();

                DependencyContainer.Current.Register<IGpioController>(new GpioController());
                DependencyContainer.Current.Register<ISpiBus>(new SpiBus());
                DependencyContainer.Current.Register<II2CBus>(new I2CBus());
                DependencyContainer.Current.Register<ISystemInfo>(new SystemInfo());
                DependencyContainer.Current.Register<ITiming>(new Timing());
                DependencyContainer.Current.Register<IThreading>(new PigpioThreading());
            }
        }
    }
}
