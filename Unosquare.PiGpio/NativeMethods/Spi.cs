namespace Unosquare.PiGpio.NativeMethods
{
    using NativeEnums;
    using NativeTypes;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Provides methods for SPI bus management.
    /// </summary>
    public static class Spi
    {
        #region Hardware SPI

        /// <summary>
        /// This function returns a handle for the SPI device on the channel.
        /// Data will be transferred at baud bits per second.  The flags may
        /// be used to modify the default behaviour of 4-wire operation, mode 0,
        /// active low chip select.
        ///
        /// An auxiliary SPI device is available on all models but the
        /// A and B and may be selected by setting the A bit in the flags.
        /// The auxiliary device has 3 chip selects and a selectable word
        /// size in bits.
        ///
        /// spiFlags consists of the least significant 22 bits.
        ///
        /// mm defines the SPI mode.
        ///
        /// Warning: modes 1 and 3 do not appear to work on the auxiliary device.
        ///
        /// px is 0 if CEx is active low (default) and 1 for active high.
        ///
        /// ux is 0 if the CEx GPIO is reserved for SPI (default) and 1 otherwise.
        ///
        /// A is 0 for the standard SPI device, 1 for the auxiliary SPI.
        ///
        /// W is 0 if the device is not 3-wire, 1 if the device is 3-wire.  Standard
        /// SPI device only.
        ///
        /// nnnn defines the number of bytes (0-15) to write before switching
        /// the MOSI line to MISO to read data.  This field is ignored
        /// if W is not set.  Standard SPI device only.
        ///
        /// T is 1 if the least significant bit is transmitted on MOSI first, the
        /// default (0) shifts the most significant bit out first.  Auxiliary SPI
        /// device only.
        ///
        /// R is 1 if the least significant bit is received on MISO first, the
        /// default (0) receives the most significant bit first.  Auxiliary SPI
        /// device only.
        ///
        /// bbbbbb defines the word size in bits (0-32).  The default (0)
        /// sets 8 bits per word.  Auxiliary SPI device only.
        ///
        /// The <see cref="SpiRead"/>, <see cref="SpiWrite"/>, and <see cref="SpiXfer"/> functions
        /// transfer data packed into 1, 2, or 4 bytes according to
        /// the word size in bits.
        ///
        /// For bits 1-8 there will be one byte per word.
        /// For bits 9-16 there will be two bytes per word.
        /// For bits 17-32 there will be four bytes per word.
        ///
        /// Multi-byte transfers are made in least significant byte first order.
        ///
        /// E.g. to transfer 32 11-bit words buf should contain 64 bytes
        /// and count should be 64.
        ///
        /// E.g. to transfer the 14 bit value 0x1ABC send the bytes 0xBC followed
        /// by 0x1A.
        ///
        /// The other bits in flags should be set to zero.
        /// </summary>
        /// <remarks>
        /// 21 20 19 18 17 16 15 14 13 12 11 10  9  8  7  6  5  4  3  2  1  0
        ///  b  b  b  b  b  b  R  T  n  n  n  n  W  A u2 u1 u0 p2 p1 p0  m  m
        /// Mode POL PHA
        ///  0    0   0
        ///  1    0   1
        ///  2    1   0
        ///  3    1   1.
        /// </remarks>
        /// <param name="spiChannel">0-1 (0-2 for the auxiliary SPI device).</param>
        /// <param name="baudRate">32K-125M (values above 30M are unlikely to work).</param>
        /// <param name="spiFlags">see below.</param>
        /// <returns>Returns a handle (&gt;=0) if OK, otherwise PI_BAD_SPI_CHANNEL, PI_BAD_SPI_SPEED, PI_BAD_FLAGS, PI_NO_AUX_SPI, or PI_SPI_OPEN_FAILED.</returns>
        public static UIntPtr SpiOpen(SpiChannelId spiChannel, int baudRate, SpiFlags spiFlags)
        {
            var result = BoardException.ValidateResult(SpiOpenUnmanaged(spiChannel, Convert.ToUInt32(baudRate), spiFlags));
            return new UIntPtr(Convert.ToUInt32(result));
        }

        /// <summary>
        /// This functions closes the SPI device identified by the handle.
        ///
        /// </summary>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="SpiOpen"/>.</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "spiClose")]
        public static extern ResultCode SpiClose(UIntPtr handle);

        /// <summary>
        /// This function reads count bytes of data from the SPI
        /// device associated with the handle.
        ///
        /// PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.
        /// </summary>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="SpiOpen"/>.</param>
        /// <param name="count">The max number of bytes to read.</param>
        /// <returns>Returns the number of bytes transferred if OK, otherwise PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.</returns>
        public static byte[] SpiRead(UIntPtr handle, int count)
        {
            var buffer = new byte[count];
            var result = BoardException.ValidateResult(SpiReadUnmanaged(handle, buffer, Convert.ToUInt32(count)));
            if (result == buffer.Length)
                return buffer;

            var output = new byte[result];
            Buffer.BlockCopy(buffer, 0, output, 0, result);
            return output;
        }

        /// <summary>
        /// This function writes count bytes of data from buf to the SPI
        /// device associated with the handle.
        ///
        /// PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.
        /// </summary>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="SpiOpen"/>.</param>
        /// <param name="buffer">the data bytes to write.</param>
        /// <returns>Returns the number of bytes transferred if OK, otherwise PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.</returns>
        public static int SpiWrite(UIntPtr handle, byte[] buffer)
        {
            var reuslt = BoardException.ValidateResult(SpiWriteUnmanaged(handle, buffer, Convert.ToUInt32(buffer.Length)));
            return reuslt;
        }

        /// <summary>
        /// This function transfers count bytes of data from txBuf to the SPI
        /// device associated with the handle. Simultaneously count bytes of
        /// data are read from the device and placed in rxBuf.
        ///
        /// PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.
        /// </summary>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="SpiOpen"/>.</param>
        /// <param name="transmitBuffer">the data bytes to write.</param>
        /// <returns>Returns the number of bytes transferred if OK, otherwise PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.</returns>
        public static byte[] SpiXfer(UIntPtr handle, byte[] transmitBuffer)
        {
            var receiveBuffer = new byte[transmitBuffer.Length];
            var result = BoardException.ValidateResult(SpiXferUnmanaged(handle, transmitBuffer, receiveBuffer, Convert.ToUInt32(receiveBuffer.Length)));
            if (result == receiveBuffer.Length)
                return receiveBuffer;

            var output = new byte[result];
            Buffer.BlockCopy(receiveBuffer, 0, output, 0, result);
            return output;
        }

        #endregion

        /// <summary>
        /// This function selects a set of GPIO for bit banging SPI with
        /// a specified baud rate and mode.
        ///
        /// spiFlags consists of the least significant 22 bits.
        ///
        /// mm defines the SPI mode, defaults to 0
        ///
        /// p is 0 if CS is active low (default) and 1 for active high.
        ///
        /// T is 1 if the least significant bit is transmitted on MOSI first, the
        /// default (0) shifts the most significant bit out first.
        ///
        /// R is 1 if the least significant bit is received on MISO first, the
        /// default (0) receives the most significant bit first.
        ///
        /// The other bits in flags should be set to zero.
        ///
        /// If more than one device is connected to the SPI bus (defined by
        /// SCLK, MOSI, and MISO) each must have its own CS.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// bbSPIOpen(10, MISO, MOSI, SCLK, 10000, 0); // device 1
        /// bbSPIOpen(11, MISO, MOSI, SCLK, 20000, 3); // device 2
        /// </code>
        /// </example>
        /// <remarks>
        /// 21 20 19 18 17 16 15 14 13 12 11 10  9  8  7  6  5  4  3  2  1  0
        ///  0  0  0  0  0  0  R  T  0  0  0  0  0  0  0  0  0  0  0  p  m  m
        /// Mode CPOL CPHA
        ///  0    0    0
        ///  1    0    1
        ///  2    1    0
        ///  3    1    1.
        /// </remarks>
        /// <param name="csPin">0-31.</param>
        /// <param name="misoPin">MISO 0-31.</param>
        /// <param name="mosiPin">MOSI 0-31.</param>
        /// <param name="clockPin">CLOCK 0-31.</param>
        /// <param name="baudRate">50-250000.</param>
        /// <param name="spiFlags">see below.</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, PI_BAD_SPI_BAUD, or PI_GPIO_IN_USE.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbSPIOpen")]
        public static extern ResultCode BbSPIOpen(UserGpio csPin, UserGpio misoPin, UserGpio mosiPin, UserGpio clockPin, uint baudRate, SoftSpiFlags spiFlags);

        /// <summary>
        /// This function stops bit banging SPI on a set of GPIO
        /// opened with <see cref="BbSPIOpen"/>.
        ///
        /// </summary>
        /// <param name="csPin">0-31, the CS GPIO used in a prior call to <see cref="BbSPIOpen"/>.</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, or PI_NOT_SPI_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbSPIClose")]
        public static extern ResultCode BbSPIClose(UserGpio csPin);

        /// <summary>
        /// This function executes a bit banged SPI transfer.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// // gcc -Wall -pthread -o bbSPIx_test bbSPIx_test.c -lpigpio
        /// // sudo ./bbSPIx_test
        ///
        ///
        /// #include &lt;stdio.h&gt;
        ///
        /// #include "pigpio.h"
        ///
        /// #define CE0 5
        /// #define CE1 6
        /// #define MISO 13
        /// #define MOSI 19
        /// #define SCLK 12
        ///
        /// int main(int argc, char *argv[])
        /// {
        ///    int i, count, set_val, read_val;
        ///    unsigned char inBuf[3];
        ///    char cmd1[] = {0, 0};
        ///    char cmd2[] = {12, 0};
        ///    char cmd3[] = {1, 128, 0};
        ///
        ///    if (gpioInitialise() &lt; 0)
        ///    {
        ///       fprintf(stderr, "pigpio initialisation failed.\n");
        ///       return 1;
        ///    }
        ///
        ///    bbSPIOpen(CE0, MISO, MOSI, SCLK, 10000, 0); // MCP4251 DAC
        ///    bbSPIOpen(CE1, MISO, MOSI, SCLK, 20000, 3); // MCP3008 ADC
        ///
        ///    for (i=0; i&lt;256; i++)
        ///    {
        ///       cmd1[1] = i;
        ///
        ///       count = bbSPIXfer(CE0, cmd1, (char *)inBuf, 2); // &gt; DAC
        ///
        ///       if (count == 2)
        ///       {
        ///          count = bbSPIXfer(CE0, cmd2, (char *)inBuf, 2); // &lt; DAC
        ///
        ///          if (count == 2)
        ///          {
        ///             set_val = inBuf[1];
        ///
        ///             count = bbSPIXfer(CE1, cmd3, (char *)inBuf, 3); // &lt; ADC
        ///
        ///             if (count == 3)
        ///             {
        ///                read_val = ((inBuf[1]&amp;3)&lt;&lt;8) | inBuf[2];
        ///                printf("%d %d\n", set_val, read_val);
        ///             }
        ///          }
        ///       }
        ///    }
        ///
        ///    bbSPIClose(CE0);
        ///    bbSPIClose(CE1);
        ///
        ///    gpioTerminate();
        ///
        ///    return 0;
        /// }
        /// </code>
        /// </example>
        /// <param name="csPin">0-31 (as used in a prior call to <see cref="BbSPIOpen"/>).</param>
        /// <param name="inputBuffer">pointer to buffer to hold data to be sent.</param>
        /// <param name="outputBuffer">pointer to buffer to hold returned data.</param>
        /// <param name="count">size of data transfer, which is the same as the data received.</param>
        /// <returns>Returns &gt;= 0 if OK (the number of bytes read), otherwise PI_BAD_USER_GPIO, PI_NOT_SPI_GPIO or PI_BAD_POINTER.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bbSPIXfer")]
        public static extern int BbSPIXfer(UserGpio csPin, [In, MarshalAs(UnmanagedType.LPArray)] byte[] inputBuffer, [In, MarshalAs(UnmanagedType.LPArray)] byte[] outputBuffer, uint count);

        #region Methods: I2C/SPI Slave

        /// <summary>
        /// This function provides a low-level interface to the
        /// SPI/I2C Slave peripheral.  This peripheral allows the
        /// Pi to act as a slave device on an I2C or SPI bus.
        ///
        /// I can't get SPI to work properly.  I tried with a
        /// control word of 0x303 and swapped MISO and MOSI.
        ///
        /// The function sets the BSC mode, writes any data in
        /// the transmit buffer to the BSC transmit FIFO, and
        /// copies any data in the BSC receive FIFO to the
        /// receive buffer.
        ///
        /// To start a transfer set control (see below) and copy the bytes to
        /// be sent (if any) to txBuf and set the byte count in txCnt.
        ///
        /// Upon return rxCnt will be set to the number of received bytes placed
        /// in rxBuf.
        ///
        /// Note that the control word sets the BSC mode.  The BSC will stay in
        /// that mode until a different control word is sent.
        ///
        /// The BSC peripheral uses GPIO 18 (SDA) and 19 (SCL) in I2C mode
        /// and GPIO 18 (MOSI), 19 (SCLK), 20 (MISO), and 21 (CE) in SPI mode.  You
        /// need to swap MISO/MOSI between master and slave.
        ///
        /// When a zero control word is received GPIO 18-21 will be reset
        /// to INPUT mode.
        ///
        /// The returned function value is the status of the transfer (see below).
        ///
        /// If there was an error the status will be less than zero
        /// (and will contain the error code).
        ///
        /// The most significant word of the returned status contains the number
        /// of bytes actually copied from txBuf to the BSC transmit FIFO (may be
        /// less than requested if the FIFO already contained untransmitted data).
        ///
        /// control consists of the following bits.
        ///
        /// Bits 0-13 are copied unchanged to the BSC CR register.  See
        /// pages 163-165 of the Broadcom peripherals document for full
        /// details.
        ///
        /// aaaaaaa @ defines the I2C slave address (only relevant in I2C mode)
        /// IT      @ invert transmit status flags
        /// HC      @ enable host control
        /// TF      @ enable test FIFO
        /// IR      @ invert receive status flags
        /// RE      @ enable receive
        /// TE      @ enable transmit
        /// BK      @ abort operation and clear FIFOs
        /// EC      @ send control register as first I2C byte
        /// ES      @ send status register as first I2C byte
        /// PL      @ set SPI polarity high
        /// PH      @ set SPI phase high
        /// I2      @ enable I2C mode
        /// SP      @ enable SPI mode
        /// EN      @ enable BSC peripheral
        ///
        /// The returned status has the following format
        ///
        /// Bits 0-15 are copied unchanged from the BSC FR register.  See
        /// pages 165-166 of the Broadcom peripherals document for full
        /// details.
        ///
        /// SSSSS @ number of bytes successfully copied to transmit FIFO
        /// RRRRR @ number of bytes in receieve FIFO
        /// TTTTT @ number of bytes in transmit FIFO
        /// RB    @ receive busy
        /// TE    @ transmit FIFO empty
        /// RF    @ receive FIFO full
        /// TF    @ transmit FIFO full
        /// RE    @ receive FIFO empty
        /// TB    @ transmit busy
        ///
        /// The following example shows how to configure the BSC peripheral as
        /// an I2C slave with address 0x13 and send four bytes.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// bsc_xfer_t xfer;
        ///
        /// xfer.control = (0x13&lt;&lt;16) | 0x305;
        ///
        /// memcpy(xfer.txBuf, "ABCD", 4);
        /// xfer.txCnt = 4;
        ///
        /// status = bscXfer(&amp;xfer);
        ///
        /// if (status &gt;= 0)
        /// {
        ///    // process transfer
        /// }
        /// </code>
        /// </example>
        /// <remarks>
        ///
        /// typedef struct
        /// {
        ///    uint control;          // Write
        ///    int rxCnt;                 // Read only
        ///    char rxBuf[BSC_FIFO_SIZE]; // Read only
        ///    int txCnt;                 // Write
        ///    char txBuf[BSC_FIFO_SIZE]; // Write
        /// } bsc_xfer_t;
        /// 22 21 20 19 18 17 16 15 14 13 12 11 10  9  8  7  6  5  4  3  2  1  0
        ///  a  a  a  a  a  a  a  -  - IT HC TF IR RE TE BK EC ES PL PH I2 SP EN
        /// 20 19 18 17 16 15 14 13 12 11 10  9  8  7  6  5  4  3  2  1  0
        ///  S  S  S  S  S  R  R  R  R  R  T  T  T  T  T RB TE RF TF RE TB.
        /// </remarks>
        /// <param name="bscTransfer">= a structure defining the transfer.</param>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "bscXfer")]
        public static extern int BscXfer(BscTransfer bscTransfer);

        #endregion

        #region Unmanaged Methods

        /// <summary>
        /// This function returns a handle for the SPI device on the channel.
        /// Data will be transferred at baud bits per second.  The flags may
        /// be used to modify the default behaviour of 4-wire operation, mode 0,
        /// active low chip select.
        ///
        /// An auxiliary SPI device is available on all models but the
        /// A and B and may be selected by setting the A bit in the flags.
        /// The auxiliary device has 3 chip selects and a selectable word
        /// size in bits.
        ///
        /// spiFlags consists of the least significant 22 bits.
        ///
        /// mm defines the SPI mode.
        ///
        /// Warning: modes 1 and 3 do not appear to work on the auxiliary device.
        ///
        /// px is 0 if CEx is active low (default) and 1 for active high.
        ///
        /// ux is 0 if the CEx GPIO is reserved for SPI (default) and 1 otherwise.
        ///
        /// A is 0 for the standard SPI device, 1 for the auxiliary SPI.
        ///
        /// W is 0 if the device is not 3-wire, 1 if the device is 3-wire.  Standard
        /// SPI device only.
        ///
        /// nnnn defines the number of bytes (0-15) to write before switching
        /// the MOSI line to MISO to read data.  This field is ignored
        /// if W is not set.  Standard SPI device only.
        ///
        /// T is 1 if the least significant bit is transmitted on MOSI first, the
        /// default (0) shifts the most significant bit out first.  Auxiliary SPI
        /// device only.
        ///
        /// R is 1 if the least significant bit is received on MISO first, the
        /// default (0) receives the most significant bit first.  Auxiliary SPI
        /// device only.
        ///
        /// bbbbbb defines the word size in bits (0-32).  The default (0)
        /// sets 8 bits per word.  Auxiliary SPI device only.
        ///
        /// The <see cref="SpiRead"/>, <see cref="SpiWrite"/>, and <see cref="SpiXfer"/> functions
        /// transfer data packed into 1, 2, or 4 bytes according to
        /// the word size in bits.
        ///
        /// For bits 1-8 there will be one byte per word.
        /// For bits 9-16 there will be two bytes per word.
        /// For bits 17-32 there will be four bytes per word.
        ///
        /// Multi-byte transfers are made in least significant byte first order.
        ///
        /// E.g. to transfer 32 11-bit words buf should contain 64 bytes
        /// and count should be 64.
        ///
        /// E.g. to transfer the 14 bit value 0x1ABC send the bytes 0xBC followed
        /// by 0x1A.
        ///
        /// The other bits in flags should be set to zero.
        /// </summary>
        /// <remarks>
        /// 21 20 19 18 17 16 15 14 13 12 11 10  9  8  7  6  5  4  3  2  1  0
        ///  b  b  b  b  b  b  R  T  n  n  n  n  W  A u2 u1 u0 p2 p1 p0  m  m
        /// Mode POL PHA
        ///  0    0   0
        ///  1    0   1
        ///  2    1   0
        ///  3    1   1.
        /// </remarks>
        /// <param name="spiChannel">0-1 (0-2 for the auxiliary SPI device).</param>
        /// <param name="baudRate">32K-125M (values above 30M are unlikely to work).</param>
        /// <param name="spiFlags">see below.</param>
        /// <returns>Returns a handle (&gt;=0) if OK, otherwise PI_BAD_SPI_CHANNEL, PI_BAD_SPI_SPEED, PI_BAD_FLAGS, PI_NO_AUX_SPI, or PI_SPI_OPEN_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "spiOpen")]
        private static extern int SpiOpenUnmanaged(SpiChannelId spiChannel, uint baudRate, SpiFlags spiFlags);

        /// <summary>
        /// This function reads count bytes of data from the SPI
        /// device associated with the handle.
        ///
        /// PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.
        /// </summary>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="SpiOpen"/>.</param>
        /// <param name="buffer">an array to receive the read data bytes.</param>
        /// <param name="count">the number of bytes to read.</param>
        /// <returns>Returns the number of bytes transferred if OK, otherwise PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "spiRead")]
        private static extern int SpiReadUnmanaged(UIntPtr handle, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

        /// <summary>
        /// This function writes count bytes of data from buf to the SPI
        /// device associated with the handle.
        ///
        /// PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.
        /// </summary>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="SpiOpen"/>.</param>
        /// <param name="buffer">the data bytes to write.</param>
        /// <param name="count">the number of bytes to write.</param>
        /// <returns>Returns the number of bytes transferred if OK, otherwise PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "spiWrite")]
        private static extern int SpiWriteUnmanaged(UIntPtr handle, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

        /// <summary>
        /// This function transfers count bytes of data from txBuf to the SPI
        /// device associated with the handle.  Simultaneously count bytes of
        /// data are read from the device and placed in rxBuf.
        ///
        /// PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.
        /// </summary>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="SpiOpen"/>.</param>
        /// <param name="transmitBuffer">the data bytes to write.</param>
        /// <param name="receiveBuffer">the received data bytes.</param>
        /// <param name="count">the number of bytes to transfer.</param>
        /// <returns>Returns the number of bytes transferred if OK, otherwise PI_BAD_HANDLE, PI_BAD_SPI_COUNT, or PI_SPI_XFER_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "spiXfer")]
        private static extern int SpiXferUnmanaged(UIntPtr handle, [In, MarshalAs(UnmanagedType.LPArray)] byte[] transmitBuffer, [In, MarshalAs(UnmanagedType.LPArray)] byte[] receiveBuffer, uint count);

        #endregion
    }
}
