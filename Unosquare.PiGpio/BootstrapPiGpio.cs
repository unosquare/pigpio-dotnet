namespace Unosquare.PiGpio
{
    using RaspberryIO.Abstractions;

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
                // TODO: Include extract resource
                // Resources.EmbeddedResources.ExtractAll();
            }
        }
    }
}
