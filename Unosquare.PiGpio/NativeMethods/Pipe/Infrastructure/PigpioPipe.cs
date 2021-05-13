using System.Linq;

namespace Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure
{
    using System;
    using System.Globalization;
    using System.IO;
    using Unosquare.PiGpio.NativeEnums;

    internal class PigpioPipe : IPigpioPipe, IDisposable
    {
        private readonly PipeWriter _pipeWriter;
        private readonly PipeReader _pipeReader;
        private readonly PipeReader _errorReader;
        private readonly StreamWriter PipeWriter;
        private readonly StreamReader PipeReader;
        private readonly StreamReader ErrorReader;
        private readonly object _syncLock = new object();

        public PigpioPipe()
        {
            _pipeWriter = new PipeWriter(Constants.CommandPipeName);
            PipeWriter = _pipeWriter.Writer;
            _pipeReader = new PipeReader(Constants.ResultPipeName);
            PipeReader = _pipeReader.Reader;
            _errorReader = new PipeReader(Constants.ErrorPipeName);
            ErrorReader = _errorReader.Reader;
        }

        public void SendCommand(string cmd)
        {
            lock (_syncLock)
            {
                PipeWriter.WriteLine(cmd);
                PipeReader.DiscardBufferedData();
            }
        }

        public string SendCommandWithResult(string cmd)
        {
            lock (_syncLock)
            {
                PipeWriter.WriteLine(cmd);
                return PipeReader.ReadLine();
            }
        }

        public int SendCommandWithIntResult(string cmd)
        {
            lock (_syncLock)
            {
                PipeWriter.WriteLine(cmd);
                return Convert.ToInt32(PipeReader.ReadLine(), CultureInfo.InvariantCulture);
            }
        }

        public uint SendCommandWithUIntResult(string cmd)
        {
            lock (_syncLock)
            {
                PipeWriter.WriteLine(cmd);
                return Convert.ToUInt32(PipeReader.ReadLine(), CultureInfo.InvariantCulture);
            }
        }

        public ResultCode SendCommandWithResultCode(string cmd)
        {
            lock (_syncLock)
            {
                PipeWriter.WriteLine(cmd);
                if (PipeReader.EndOfStream)
                {
                    return ResultCode.Ok;
                }

                var result = PipeReader.ReadLine();
                var code = Convert.ToInt32(result, CultureInfo.InvariantCulture);
                return (ResultCode)code;
            }
        }

        public byte[] SendCommandWithResultBlob(string cmd)
        {
            lock (_syncLock)
            {
                PipeWriter.WriteLine(cmd);
                if (PipeReader.EndOfStream)
                {
                    return Array.Empty<byte>();
                }

                var text = PipeReader.ReadLine();
                var numbers = ((text != null) ? text.Split(' ') : Array.Empty<string>())
                    .Select(t => Convert.ToByte(t, CultureInfo.InvariantCulture))
                    .ToArray();

                return numbers;
            }
        }

        public void Dispose()
        {
            _pipeWriter.Dispose();
            _pipeReader.Dispose();
            _errorReader.Dispose();
        }
    }
}