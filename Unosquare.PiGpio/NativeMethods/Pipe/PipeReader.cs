namespace Unosquare.PiGpio.NativeMethods.Pipe
{
    using System;
    using System.IO;
    using System.Text;

    internal class PipeReader: IDisposable
    {
        private readonly FileStream _stream;

        public PipeReader(string pipeName)
        {
            // Named Pipe classes would be easier but they don't work on Linux in .Net Core
            _stream = new FileStream(pipeName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            Reader = new StreamReader(_stream, Encoding.ASCII);
        }

        public StreamReader Reader { get; }

        public void Dispose()
        {
            Reader?.Dispose();
            _stream?.Dispose();
        }
    }
}