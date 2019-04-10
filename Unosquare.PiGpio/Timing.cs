namespace Unosquare.PiGpio
{
    using RaspberryIO.Abstractions;

    public class Timing : ITiming
    {
        public void SleepMilliseconds(uint millis)
        {
            throw new System.NotImplementedException();
        }

        public void SleepMicroseconds(uint micros)
        {
            throw new System.NotImplementedException();
        }

        public uint Milliseconds { get; }
        public uint Microseconds { get; }
    }
}
