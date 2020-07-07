namespace Unosquare.PiGpio.NativeMethods.Pipe
{
    using System;
    using System.IO;
    using System.Text;

    internal class PipeWriter : IDisposable
    {
        private readonly FileStream _stream;

        public PipeWriter(string pipeName)
        {
            // Named Pipe classes would be easier but they don't work on Linux in .Net Core
            _stream = new FileStream(pipeName, FileMode.Open, FileAccess.Write, FileShare.ReadWrite, 4096, FileOptions.WriteThrough);
            Writer = new StreamWriter(_stream, Encoding.ASCII) { AutoFlush = true };
        }

        public StreamWriter Writer { get; }

        public void Dispose()
        {
            Writer?.Dispose();
            _stream?.Dispose();
        }
    }
}