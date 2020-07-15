namespace Unosquare.PiGpio.NativeMethods.Pipe
{
    using System.Linq;
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.Interfaces;
    using Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure;

    /// <summary>
    /// Serial Service Pipe strategy pattern.
    /// </summary>
    internal class SerialServicePipe : ISerialService
    {
        private readonly IPigpioPipe _pigpioPipe;

        internal SerialServicePipe(IPigpioPipe pipe)
        {
            _pigpioPipe = pipe;
        }

        /// <inheritdoc />
        public ResultCode GpioSerialReadOpen(UserGpio userGpio, uint baudRate, uint dataBits)
        {
            return _pigpioPipe.SendCommandWithResultCode($"sero {(uint)userGpio} {baudRate} {dataBits}");
        }

        /// <inheritdoc />
        public ResultCode GpioSerialReadInvert(UserGpio userGpio, bool invert)
        {
            return _pigpioPipe.SendCommandWithResultCode($"slri {(uint)userGpio} {(invert ? 1 : 0)}");
        }

        /// <inheritdoc />
        public uint GpioSerialRead(UserGpio userGpio, byte[] buffer, int readLength)
        {
            var data = _pigpioPipe.SendCommandWithResultBlob($"serr {(uint)userGpio} {readLength}");
            if (data.Length == 0)
            {
                return 0;
            }

            // first byte is length and can be ignored
            for (var i = 1; i < data.Length; i++)
            {
                buffer[i - 1] = data[i];
            }

            return (uint)(data.Length - 1);
        }

        /// <inheritdoc />
        public byte[] GpioSerialRead(UserGpio userGpio, int readLength)
        {
            var data = _pigpioPipe.SendCommandWithResultBlob($"serr {(uint)userGpio} {readLength}");

            // first byte is length and can be ignored
            return data.Skip(1).ToArray();
        }

        /// <inheritdoc />
        public ResultCode GpioSerialReadClose(UserGpio userGpio)
        {
            return _pigpioPipe.SendCommandWithResultCode($"serc {(uint)userGpio}");
        }
    }
}