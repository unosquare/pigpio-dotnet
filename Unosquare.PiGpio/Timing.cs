namespace Unosquare.PiGpio
{
    using RaspberryIO.Abstractions;

    /// <summary>
    /// Represents a class with Timing related methods.
    /// </summary>
    /// <seealso cref="ITiming" />
    public class Timing : ITiming
    {
        /// <inheritdoc />
        public void SleepMilliseconds(uint millis)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void SleepMicroseconds(uint micros)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public uint Milliseconds { get; }

        /// <inheritdoc />
        public uint Microseconds { get; }
    }
}
