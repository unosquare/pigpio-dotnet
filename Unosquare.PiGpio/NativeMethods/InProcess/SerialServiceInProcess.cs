namespace Unosquare.PiGpio.NativeMethods.InProcess
{
    using Unosquare.PiGpio.NativeEnums;
    using Unosquare.PiGpio.NativeMethods.InProcess.DllImports;
    using Unosquare.PiGpio.NativeMethods.Interfaces;

    /// <summary>
    /// Serial Service In Process strategy pattern implementation.
    /// </summary>
    public class SerialServiceInProcess : ISerialService
    {
        /// <inheritdoc />
        public ResultCode GpioSerialReadOpen(UserGpio userGpio, uint baudRate, uint dataBits)
        {
            return Serial.GpioSerialReadOpen(userGpio, baudRate, dataBits);
        }

        /// <inheritdoc />
        public ResultCode GpioSerialReadInvert(UserGpio userGpio, bool invert)
        {
            return Serial.GpioSerialReadInvert(userGpio, invert);
        }

        /// <inheritdoc />
        public int GpioSerialRead(UserGpio userGpio, byte[] buffer, int readLength)
        {
            return Serial.GpioSerialRead(userGpio, buffer, readLength);
        }

        /// <inheritdoc />
        public byte[] GpioSerialRead(UserGpio userGpio, int readLength)
        {
            return Serial.GpioSerialRead(userGpio, readLength);
        }

        /// <inheritdoc />
        public ResultCode GpioSerialReadClose(UserGpio userGpio)
        {
            return Serial.GpioSerialReadClose(userGpio);
        }
    }
}