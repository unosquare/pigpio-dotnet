namespace Unosquare.PiGpio.NativeMethods.Pipe
{
    using System;
    using System.IO;
    using System.Text;

    internal class PipeReader : IDisposable
    {
        protected readonly FileStream Stream;

        public PipeReader(string pipeName)
        {
            // Named Pipe classes would be easier but they don't work on Linux in .Net Core
            Stream = new FileStream(pipeName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            Reader = new StreamReader(Stream, Encoding.ASCII);
        }

        public StreamReader Reader { get; }

        public virtual void Dispose()
        {
            Reader?.Dispose();
            Stream?.Dispose();
        }
    }
}