namespace Unosquare.PiGpio
{
    using RaspberryIO.Abstractions;
    using System;

    /// <summary>
    /// Use this class to access threading methods using interop.
    /// </summary>
    /// <seealso cref="IThreading" />
    public class Threading : IThreading
    {
        /// <inheritdoc />
        public void StartThread(Action worker)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public UIntPtr StartThreadEx(Action<UIntPtr> worker, UIntPtr userData)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void StopThreadEx(UIntPtr handle)
        {
            throw new NotImplementedException();
        }
    }
}
