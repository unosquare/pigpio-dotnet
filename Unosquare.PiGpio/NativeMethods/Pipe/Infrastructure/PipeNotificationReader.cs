using System;
using System.Threading;
using System.Threading.Tasks;

namespace Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure
{
    internal class PipeNotificationReader : PipeReader
    {
        private const int BufferLen = 1200;

        private readonly byte[] _buffer = new byte[BufferLen];
        private readonly CancellationTokenSource _cancelListening = new CancellationTokenSource();

        private Task _listenTask;

        public PipeNotificationReader(string pipeName)
            : base(pipeName)
        {
            Listen();
        }

        public event Action<PigpioNotification[]> OnNotification;

        public override void Dispose()
        {
            _cancelListening.Cancel();
            _listenTask.Wait();
            _cancelListening.Dispose();
            base.Dispose();
        }

        private void Listen()
        {
            _listenTask = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        // read at least one byte, and up to BUFFER_LEN bytes, as much as is currently in the pipe
                        var count = Stream.Read(_buffer, 0, BufferLen);

                        var notifications = ParseBuffer(count);

                        _cancelListening.Token.ThrowIfCancellationRequested();
                        OnNotification?.Invoke(notifications);

                        _cancelListening.Token.ThrowIfCancellationRequested();
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                        // swallow errors - this is probably where we end up when everything is disposed
                    }
                }
            }, _cancelListening.Token);
            _listenTask.Start();
        }

        private PigpioNotification[] ParseBuffer(int count)
        {
            var result = new PigpioNotification[count / 12];
            var i = 0;
            var start = 0;
            while (start < count)
            {
                result[i].SequenceNumber = BitConverter.ToUInt16(_buffer, start);
                result[i].Flags = BitConverter.ToUInt16(_buffer, start + 2);
                result[i].Tick = BitConverter.ToUInt32(_buffer, start + 4);
                result[i].Level = BitConverter.ToUInt32(_buffer, start + 8);
                i++;
                start += 12;
            }

            return result;
        }
    }
}