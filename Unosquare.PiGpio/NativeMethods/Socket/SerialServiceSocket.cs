namespace Unosquare.PiGpio.NativeMethods.Socket
{
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.Interfaces;

    /// <summary>
    /// Serial Service Socket strategy pattern.
    /// </summary>
    internal class SerialServiceSocket : ISerialService
    {
        /// <inheritdoc />
        public ResultCode GpioSerialReadOpen(UserGpio userGpio, uint baudRate, uint dataBits)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSerialReadInvert(UserGpio userGpio, bool invert)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public uint GpioSerialRead(UserGpio userGpio, byte[] buffer, int readLength)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public byte[] GpioSerialRead(UserGpio userGpio, int readLength)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public ResultCode GpioSerialReadClose(UserGpio userGpio)
        {
            throw new System.NotImplementedException();
        }
    }
}