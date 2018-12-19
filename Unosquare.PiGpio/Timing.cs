namespace Unosquare.PiGpio
{
    using RaspberryIO.Abstractions;

    /// <summary>
    /// Provides access to timing and threading properties and methods.
    /// </summary>
    public class Timing : ITiming
    {
        /// <inheritdoc />
        public uint Milliseconds { get; }
        
        /// <inheritdoc />
        public uint Microseconds { get; }

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
    }
}
