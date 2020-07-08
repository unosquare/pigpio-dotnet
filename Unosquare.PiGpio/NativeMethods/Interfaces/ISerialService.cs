namespace Unosquare.PiGpio.NativeMethods.Interfaces
{
    using Unosquare.PiGpio.NativeEnums;

    public interface ISerialService
    {
        /// <summary>
        /// This function opens a GPIO for bit bang reading of serial data.
        ///
        /// The serial data is returned in a cyclic buffer and is read using
        /// <see cref="GpioSerialRead(UserGpio, int)"/>.
        ///
        /// It is the caller's responsibility to read data from the cyclic buffer
        /// in a timely fashion.
        /// </summary>
        /// <param name="userGpio">0-31.</param>
        /// <param name="baudRate">50-250000.</param>
        /// <param name="dataBits">1-32.</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, PI_BAD_WAVE_BAUD, PI_BAD_DATABITS, or PI_GPIO_IN_USE.</returns>
        ResultCode GpioSerialReadOpen(UserGpio userGpio, uint baudRate, uint dataBits);

        /// <summary>
        /// This function configures the level logic for bit bang serial reads.
        ///
        /// Use PI_BB_SER_INVERT to invert the serial logic and PI_BB_SER_NORMAL for
        /// normal logic.  Default is PI_BB_SER_NORMAL.
        ///
        /// The GPIO must be opened for bit bang reading of serial data using
        /// <see cref="GpioSerialReadOpen"/> prior to calling this function.
        /// </summary>
        /// <param name="userGpio">0-31.</param>
        /// <param name="invert">0-1.</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, PI_GPIO_IN_USE, PI_NOT_SERIAL_GPIO, or PI_BAD_SER_INVERT.</returns>
        ResultCode GpioSerialReadInvert(UserGpio userGpio, bool invert);

        /// <summary>
        /// This function copies up to bufSize bytes of data read from the
        /// bit bang serial cyclic buffer to the buffer starting at buf.
        ///
        /// The bytes returned for each character depend upon the number of
        /// data bits data bits specified in the <see cref="GpioSerialReadOpen"/> command.
        ///
        /// For data bits 1-8 there will be one byte per character.
        /// For data bits 9-16 there will be two bytes per character.
        /// For data bits 17-32 there will be four bytes per character.
        /// </summary>
        /// <param name="userGpio">The user gpio.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="readLength">Length of the read.</param>
        /// <returns>The amount of bytes read.</returns>
        int GpioSerialRead(UserGpio userGpio, byte[] buffer, int readLength);

        /// <summary>
        /// This function copies up to bufSize bytes of data read from the
        /// bit bang serial cyclic buffer to the buffer starting at buf.
        ///
        /// The bytes returned for each character depend upon the number of
        /// data bits data bits specified in the <see cref="GpioSerialReadOpen"/> command.
        ///
        /// For data bits 1-8 there will be one byte per character.
        /// For data bits 9-16 there will be two bytes per character.
        /// For data bits 17-32 there will be four bytes per character.
        /// </summary>
        /// <param name="userGpio">The user gpio.</param>
        /// <param name="readLength">Length of the read.</param>
        /// <returns>The array containing the bytes that were read.</returns>
        byte[] GpioSerialRead(UserGpio userGpio, int readLength);

        /// <summary>
        /// This function closes a GPIO for bit bang reading of serial data.
        ///
        /// </summary>
        /// <param name="userGpio">0-31, previously opened with <see cref="GpioSerialReadOpen"/>.</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, or PI_NOT_SERIAL_GPIO.</returns>
        ResultCode GpioSerialReadClose(UserGpio userGpio);
    }
}