namespace Unosquare.PiGpio.NativeMethods
{
    using NativeEnums;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Exposes methods to read from a serial transmission using bit-banging (software reads).
    /// </summary>
    public static class Serial
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
        /// <param name="userGpio">0-31</param>
        /// <param name="baudRate">50-250000</param>
        /// <param name="dataBits">1-32</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, PI_BAD_WAVE_BAUD, PI_BAD_DATABITS, or PI_GPIO_IN_USE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSerialReadOpen")]
        public static extern ResultCode GpioSerialReadOpen(UserGpio userGpio, uint baudRate, uint dataBits);

        /// <summary>
        /// This function configures the level logic for bit bang serial reads.
        ///
        /// Use PI_BB_SER_INVERT to invert the serial logic and PI_BB_SER_NORMAL for
        /// normal logic.  Default is PI_BB_SER_NORMAL.
        ///
        /// The GPIO must be opened for bit bang reading of serial data using
        /// <see cref="GpioSerialReadOpen"/> prior to calling this function.
        /// </summary>
        /// <param name="userGpio">0-31</param>
        /// <param name="invert">0-1</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, PI_GPIO_IN_USE, PI_NOT_SERIAL_GPIO, or PI_BAD_SER_INVERT.</returns>
        public static ResultCode GpioSerialReadInvert(UserGpio userGpio, bool invert)
        {
            return GpioSerialReadInvertUnmanaged(userGpio, invert ? DigitalValue.True : DigitalValue.False);
        }

        /// <summary>
        /// Wrapper for the native <see cref="GpioSerialReadUnmanaged"/>
        /// </summary>
        /// <param name="userGpio">The user gpio.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="readLength">Length of the read.</param>
        /// <returns>The amount of bytes read</returns>
        public static int GpioSerialRead(UserGpio userGpio, byte[] buffer, int readLength)
        {
            var result = BoardException.ValidateResult(GpioSerialReadUnmanaged(userGpio, buffer, (uint)readLength));
            return result;
        }

        /// <summary>
        /// Wrapper for the native <see cref="GpioSerialReadUnmanaged"/>
        /// </summary>
        /// <param name="userGpio">The user gpio.</param>
        /// <param name="readLength">Length of the read.</param>
        /// <returns>The array containing the bytes that were read</returns>
        public static byte[] GpioSerialRead(UserGpio userGpio, int readLength)
        {
            var buffer = new byte[readLength];
            var result = BoardException.ValidateResult(GpioSerialReadUnmanaged(userGpio, buffer, (uint)readLength));
            var outputBuffer = new byte[result];
            Buffer.BlockCopy(buffer, 0, outputBuffer, 0, result);
            return outputBuffer;
        }

        /// <summary>
        /// This function closes a GPIO for bit bang reading of serial data.
        ///
        /// </summary>
        /// <param name="userGpio">0-31, previously opened with <see cref="GpioSerialReadOpen"/></param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, or PI_NOT_SERIAL_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSerialReadClose")]
        public static extern ResultCode GpioSerialReadClose(UserGpio userGpio);

        #region Unmanaged Methods

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
        /// <param name="userGpio">0-31, previously opened with <see cref="GpioSerialReadOpen"/></param>
        /// <param name="buffer">an array to receive the read bytes</param>
        /// <param name="bufferSize">&gt;=0</param>
        /// <returns>Returns the number of bytes copied if OK, otherwise PI_BAD_USER_GPIO or PI_NOT_SERIAL_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSerialRead")]
        private static extern int GpioSerialReadUnmanaged(UserGpio userGpio, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint bufferSize);

        /// <summary>
        /// This function configures the level logic for bit bang serial reads.
        ///
        /// Use PI_BB_SER_INVERT to invert the serial logic and PI_BB_SER_NORMAL for
        /// normal logic.  Default is PI_BB_SER_NORMAL.
        ///
        /// The GPIO must be opened for bit bang reading of serial data using
        /// <see cref="GpioSerialReadOpen"/> prior to calling this function.
        /// </summary>
        /// <param name="userGpio">0-31</param>
        /// <param name="invert">0-1</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, PI_GPIO_IN_USE, PI_NOT_SERIAL_GPIO, or PI_BAD_SER_INVERT.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSerialReadInvert")]
        private static extern ResultCode GpioSerialReadInvertUnmanaged(UserGpio userGpio, DigitalValue invert);

        #endregion
    }
}
