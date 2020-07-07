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

        public void Dispose()
        {
            _pipeWriter.Dispose();
            _pipeReader.Dispose();
            _errorReader.Dispose();
        }
    }
}