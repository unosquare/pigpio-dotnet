namespace Unosquare.PiGpio.NativeMethods
{
    using NativeEnums;
    using NativeTypes;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Provides Methods for both, hardware based I2C and Bit-bang (Software) I2C bus communication
    /// </summary>
    public static class I2c
    {
        /// <summary>
        /// This returns a handle for the device at the address on the I2C bus.
        ///
        /// No flags are currently defined.  This parameter should be set to zero.
        ///
        /// Physically buses 0 and 1 are available on the Pi.  Higher numbered buses
        /// will be available if a kernel supported bus multiplexor is being used.
        ///
        /// For the SMBus commands the low level transactions are shown at the end
        /// of the function description.  The following abbreviations are used.
        ///
        /// </summary>
        /// <remarks>
        /// S      (1 bit) : Start bit
        /// P      (1 bit) : Stop bit
        /// Rd/Wr  (1 bit) : Read/Write bit. Rd equals 1, Wr equals 0.
        /// A, NA  (1 bit) : Accept and not accept bit.
        /// Addr   (7 bits): I2C 7 bit address.
        /// i2cReg (8 bits): Command byte, a byte which often selects a register.
        /// Data   (8 bits): A data byte.
        /// Count  (8 bits): A byte defining the length of a block operation.
        ///
        /// [..]: Data sent by the device.
        /// </remarks>
        /// <param name="i2cBus">&gt;=0</param>
        /// <param name="i2cAddress">0-0x7F</param>
        /// <returns>Returns a handle (&gt;=0) if OK, otherwise PI_BAD_I2C_BUS, PI_BAD_I2C_ADDR, PI_BAD_FLAGS, PI_NO_HANDLE, or PI_I2C_OPEN_FAILED.</returns>
        public static UIntPtr I2cOpen(uint i2cBus, uint i2cAddress)
        {
            var result = BoardException.ValidateResult(I2cOpenUnmanaged(i2cBus, i2cAddress, 0));
            return new UIntPtr((uint)result);
        }

        /// <summary>
        /// This closes the I2C device associated with the handle.
        ///
        /// </summary>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cClose")]
        public static extern ResultCode I2cClose(UIntPtr handle);

        /// <summary>
        /// This sends a single bit (in the Rd/Wr bit) to the device associated
        /// with handle.
        ///
        /// Quick command. SMBus 2.0 5.5.1
        /// </summary>
        /// <remarks>
        /// S Addr bit [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="bit">0-1, the value to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        public static ResultCode I2cWriteQuick(UIntPtr handle, I2cQuickMode bit)
        {
            return I2cWriteQuickUnmanaged(handle, (uint)bit);
        }

        /// <summary>
        /// This sends a single byte to the device associated with handle.
        ///
        /// Send byte. SMBus 2.0 5.5.2
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] bVal [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="value">0-0xFF, the value to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        public static ResultCode I2cWriteByte(UIntPtr handle, byte value)
        {
            return I2cWriteByteUnmanaged(handle, value);
        }

        /// <summary>
        /// This reads a single byte from the device associated with handle.
        ///
        /// Receive byte. SMBus 2.0 5.5.3
        /// </summary>
        /// <remarks>
        /// S Addr Rd [A] [Data] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <returns>Returns the byte read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, or PI_I2C_READ_FAILED.</returns>
        public static byte I2cReadByte(UIntPtr handle)
        {
            var result = BoardException.ValidateResult(I2cReadByteUnmanaged(handle));
            return (byte)result;
        }

        /// <summary>
        /// This writes a single byte to the specified register of the device
        /// associated with handle.
        ///
        /// Write byte. SMBus 2.0 5.5.4
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] bVal [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to write</param>
        /// <param name="value">0-0xFF, the value to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        public static ResultCode I2cWriteByteData(UIntPtr handle, byte register, byte value)
        {
            return I2cWriteByteDataUnmanaged(handle, register, value);
        }

        /// <summary>
        /// This writes a single 16 bit word to the specified register of the device
        /// associated with handle.
        ///
        /// Write word. SMBus 2.0 5.5.4
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] wValLow [A] wValHigh [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to write</param>
        /// <param name="word">0-0xFFFF, the value to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        public static ResultCode I2cWriteWordData(UIntPtr handle, byte register, ushort word)
        {
            return I2cWriteWordDataUnmanaged(handle, register, word);
        }

        /// <summary>
        /// This reads a single byte from the specified register of the device
        /// associated with handle.
        ///
        /// Read byte. SMBus 2.0 5.5.5
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] S Addr Rd [A] [Data] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to read</param>
        /// <returns>Returns the byte read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        public static byte I2cReadByteData(UIntPtr handle, byte register)
        {
            var result = BoardException.ValidateResult(I2cReadByteDataUnmanaged(handle, register));
            return Convert.ToByte(result);
        }

        /// <summary>
        /// This reads a single 16 bit word from the specified register of the device
        /// associated with handle.
        ///
        /// Read word. SMBus 2.0 5.5.5
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] S Addr Rd [A] [DataLow] A [DataHigh] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to read</param>
        /// <returns>Returns the word read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        public static ushort I2cReadWordData(UIntPtr handle, byte register)
        {
            var result = BoardException.ValidateResult(I2cReadWordDataUnmanaged(handle, register));
            return Convert.ToUInt16(result);
        }

        /// <summary>
        /// This writes 16 bits of data to the specified register of the device
        /// associated with handle and reads 16 bits of data in return.
        ///
        /// Process call. SMBus 2.0 5.5.6
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] wValLow [A] wValHigh [A]
        ///    S Addr Rd [A] [DataLow] A [DataHigh] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to write/read</param>
        /// <param name="word">0-0xFFFF, the value to write</param>
        /// <returns>Returns the word read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        public static ushort I2cProcessCall(UIntPtr handle, byte register, ushort word)
        {
            var result = BoardException.ValidateResult(I2cProcessCallUnmanaged(handle, register, word));
            return Convert.ToUInt16(result);
        }

        /// <summary>
        /// This writes up to 32 bytes to the specified register of the device
        /// associated with handle.
        ///
        /// Block write. SMBus 2.0 5.5.7
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] count [A]
        ///    buf0 [A] buf1 [A] ... [A] bufn [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to write</param>
        /// <param name="buffer">an array with the data to send</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        public static ResultCode I2cWriteBlockData(UIntPtr handle, byte register, byte[] buffer)
        {
            return I2cWriteBlockDataUnmanaged(handle, register, buffer, Convert.ToUInt32(buffer.Length));
        }

        /// <summary>
        /// This reads a block of up to 32 bytes from the specified register of
        /// the device associated with handle.
        ///
        /// The amount of returned data is set by the device.
        ///
        /// Block read. SMBus 2.0 5.5.7
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A]
        ///    S Addr Rd [A] [Count] A [buf0] A [buf1] A ... A [bufn] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to read</param>
        /// <returns>Returns the number of bytes read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        public static byte[] I2cReadBlockData(UIntPtr handle, byte register)
        {
            var buffer = new byte[32];
            var count = BoardException.ValidateResult(I2cReadBlockDataUnmanaged(handle, register, buffer));
            if (count == buffer.Length)
                return buffer;

            var output = new byte[count];
            Buffer.BlockCopy(buffer, 0, output, 0, output.Length);
            return output;
        }

        /// <summary>
        /// This writes data bytes to the specified register of the device
        /// associated with handle and reads a device specified number
        /// of bytes of data in return.
        ///
        /// The SMBus 2.0 documentation states that a minimum of 1 byte may be
        /// sent and a minimum of 1 byte may be received.  The total number of
        /// bytes sent/received must be 32 or less.
        ///
        /// Block write-block read. SMBus 2.0 5.5.8
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] count [A] buf0 [A] ... bufn [A]
        ///    S Addr Rd [A] [Count] A [buf0] A ... [bufn] A P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to write/read</param>
        /// <param name="buffer">an array with the data to send and to receive the read data</param>
        /// <returns>Returns the number of bytes read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        public static int I2cBlockProcessCall(UIntPtr handle, byte register, byte[] buffer)
        {
            var result = BoardException.ValidateResult(I2cBlockProcessCallUnmanaged(handle, register, buffer, Convert.ToUInt32(buffer.Length)));
            return result;
        }

        /// <summary>
        /// This reads count bytes from the specified register of the device
        /// associated with handle .  The count may be 1-32.
        ///
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A]
        ///    S Addr Rd [A] [buf0] A [buf1] A ... A [bufn] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to read</param>
        /// <param name="count">The amount of bytes to read from 1 to 32</param>
        /// <returns>Returns the number of bytes read (&gt;0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        public static byte[] I2cReadI2cBlockData(UIntPtr handle, byte register, int count)
        {
            var buffer = new byte[count];
            var result = BoardException.ValidateResult(I2cReadI2cBlockDataUnmanaged(handle, register, buffer, Convert.ToUInt32(buffer.Length)));
            if (result == buffer.Length)
                return buffer;

            var output = new byte[result];
            Buffer.BlockCopy(buffer, 0, output, 0, result);
            return output;
        }

        /// <summary>
        /// This writes 1 to 32 bytes to the specified register of the device
        /// associated with handle.
        ///
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] buf0 [A] buf1 [A] ... [A] bufn [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to write</param>
        /// <param name="buffer">the data to write</param>
        /// <param name="count">The amount of bytes to write (from 1 to 32)</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        public static ResultCode I2cWriteI2cBlockData(UIntPtr handle, byte register, byte[] buffer, int count)
        {
            return I2cWriteI2cBlockDataUnmanaged(handle, register, buffer, Convert.ToUInt32(count));
        }

        /// <summary>
        /// This reads count bytes from the raw device into buf.
        ///
        /// </summary>
        /// <remarks>
        /// S Addr Rd [A] [buf0] A [buf1] A ... A [bufn] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="buffer">an array to receive the read data bytes</param>
        /// <returns>Returns count (&gt;0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        public static int I2cReadDevice(UIntPtr handle, byte[] buffer)
        {
            return BoardException.ValidateResult(I2cReadDeviceUnmanaged(handle, buffer, Convert.ToUInt32(buffer.Length)));
        }

        /// <summary>
        /// This reads count bytes from the raw device into buf.
        ///
        /// </summary>
        /// <remarks>
        /// S Addr Rd [A] [buf0] A [buf1] A ... A [bufn] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="count">Read upt to this many bytes</param>
        /// <returns>Returns count (&gt;0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        public static byte[] I2cReadDevice(UIntPtr handle, int count)
        {
            var buffer = new byte[count];
            var result = BoardException.ValidateResult(I2cReadDeviceUnmanaged(handle, buffer, Convert.ToUInt32(buffer.Length)));
            if (result == buffer.Length)
                return buffer;

            var output = new byte[result];
            Buffer.BlockCopy(buffer, 0, output, 0, result);
            return output;
        }

        /// <summary>
        /// This writes count bytes from buf to the raw device.
        ///
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] buf0 [A] buf1 [A] ... [A] bufn [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="buffer">an array containing the data bytes to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        public static ResultCode I2cWriteDevice(UIntPtr handle, byte[] buffer)
        {
            return I2cWriteDeviceUnmanaged(handle, buffer, Convert.ToUInt32(buffer.Length));
        }

        /// <summary>
        /// This sets the I2C (i2c-bcm2708) module "use combined transactions"
        /// parameter on or off.
        ///
        /// NOTE: when the flag is on a write followed by a read to the same
        /// slave address will use a repeated start (rather than a stop/start).
        /// </summary>
        /// <param name="setting">0 to set the parameter off, non-zero to set it on</param>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cSwitchCombined")]
        public static extern void I2cSwitchCombined(int setting);

        /// <summary>
        /// This function executes multiple I2C segments in one transaction by
        /// calling the I2C_RDWR ioctl.
        ///
        /// </summary>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="segments">an array of I2C segments</param>
        /// <param name="numSegs">&gt;0, the number of I2C segments</param>
        /// <returns>Returns the number of segments if OK, otherwise PI_BAD_I2C_SEG.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cSegments")]
        public static extern int I2cSegments(UIntPtr handle, [In, MarshalAs(UnmanagedType.LPArray)] I2CMessageSegment[] segments, uint numSegs);

        /// <summary>
        /// This function executes a sequence of I2C operations.  The
        /// operations to be performed are specified by the contents of inBuf
        /// which contains the concatenated command codes and associated data.
        ///
        /// The following command codes are supported:
        ///
        /// Name    @ Cmd &amp; Data @ Meaning
        /// End     @ 0          @ No more commands
        /// Escape  @ 1          @ Next P is two bytes
        /// On      @ 2          @ Switch combined flag on
        /// Off     @ 3          @ Switch combined flag off
        /// Address @ 4 P        @ Set I2C address to P
        /// Flags   @ 5 lsb msb  @ Set I2C flags to lsb + (msb &lt;&lt; 8)
        /// Read    @ 6 P        @ Read P bytes of data
        /// Write   @ 7 P ...    @ Write P bytes of data
        ///
        /// The address, read, and write commands take a parameter P.
        /// Normally P is one byte (0-255).  If the command is preceded by
        /// the Escape command then P is two bytes (0-65535, least significant
        /// byte first).
        ///
        /// The address defaults to that associated with the handle.
        /// The flags default to 0.  The address and flags maintain their
        /// previous value until updated.
        ///
        /// The returned I2C data is stored in consecutive locations of outBuf.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// Set address 0x53, write 0x32, read 6 bytes
        /// Set address 0x1E, write 0x03, read 6 bytes
        /// Set address 0x68, write 0x1B, read 8 bytes
        /// End
        ///
        /// 0x04 0x53   0x07 0x01 0x32   0x06 0x06
        /// 0x04 0x1E   0x07 0x01 0x03   0x06 0x06
        /// 0x04 0x68   0x07 0x01 0x1B   0x06 0x08
        /// 0x00
        /// </code>
        /// </example>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="inputBuffer">pointer to the concatenated I2C commands, see below</param>
        /// <param name="inputLength">size of command buffer</param>
        /// <param name="outputBuffer">pointer to buffer to hold returned data</param>
        /// <param name="outLength">size of output buffer</param>
        /// <returns>Returns &gt;= 0 if OK (the number of bytes read), otherwise PI_BAD_HANDLE, PI_BAD_POINTER, PI_BAD_I2C_CMD, PI_BAD_I2C_RLEN. PI_BAD_I2C_WLEN, or PI_BAD_I2C_SEG.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cZip")]
        public static extern int I2cZip(UIntPtr handle, [In, MarshalAs(UnmanagedType.LPArray)] byte[] inputBuffer, uint inputLength, [In, MarshalAs(UnmanagedType.LPArray)] byte[] outputBuffer, uint outLength);

        /// <summary>
        /// This function selects a pair of GPIO for bit banging I2C at a
        /// specified baud rate.
        ///
        /// Bit banging I2C allows for certain operations which are not possible
        /// with the standard I2C driver.
        ///
        /// o baud rates as low as 50
        /// o repeated starts
        /// o clock stretching
        /// o I2C on any pair of spare GPIO
        ///
        /// NOTE:
        ///
        /// The GPIO used for SDA and SCL must have pull-ups to 3V3 connected.  As
        /// a guide the hardware pull-ups on pins 3 and 5 are 1k8 in value.
        /// </summary>
        /// <param name="sdaPin">SDA 0-31</param>
        /// <param name="sclPin">SCL 0-31</param>
        /// <param name="baudRate">50-500000</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, PI_BAD_I2C_BAUD, or PI_GPIO_IN_USE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "bbI2COpen")]
        public static extern ResultCode BbI2COpen(UserGpio sdaPin, UserGpio sclPin, uint baudRate);

        /// <summary>
        /// This function stops bit banging I2C on a pair of GPIO previously
        /// opened with <see cref="BbI2COpen"/>.
        ///
        /// </summary>
        /// <param name="sdaPin">0-31, the SDA GPIO used in a prior call to <see cref="BbI2COpen"/></param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, or PI_NOT_I2C_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "bbI2CClose")]
        public static extern ResultCode BbI2CClose(UserGpio sdaPin);

        /// <summary>
        /// This function executes a sequence of bit banged I2C operations.  The
        /// operations to be performed are specified by the contents of inBuf
        /// which contains the concatenated command codes and associated data.
        ///
        /// The following command codes are supported:
        ///
        /// Name    @ Cmd &amp; Data   @ Meaning
        /// End     @ 0            @ No more commands
        /// Escape  @ 1            @ Next P is two bytes
        /// Start   @ 2            @ Start condition
        /// Stop    @ 3            @ Stop condition
        /// Address @ 4 P          @ Set I2C address to P
        /// Flags   @ 5 lsb msb    @ Set I2C flags to lsb + (msb &lt;&lt; 8)
        /// Read    @ 6 P          @ Read P bytes of data
        /// Write   @ 7 P ...      @ Write P bytes of data
        ///
        /// The address, read, and write commands take a parameter P.
        /// Normally P is one byte (0-255).  If the command is preceded by
        /// the Escape command then P is two bytes (0-65535, least significant
        /// byte first).
        ///
        /// The address and flags default to 0.  The address and flags maintain
        /// their previous value until updated.
        ///
        /// No flags are currently defined.
        ///
        /// The returned I2C data is stored in consecutive locations of outBuf.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// Set address 0x53
        /// start, write 0x32, (re)start, read 6 bytes, stop
        /// Set address 0x1E
        /// start, write 0x03, (re)start, read 6 bytes, stop
        /// Set address 0x68
        /// start, write 0x1B, (re)start, read 8 bytes, stop
        /// End
        ///
        /// 0x04 0x53
        /// 0x02 0x07 0x01 0x32   0x02 0x06 0x06 0x03
        ///
        /// 0x04 0x1E
        /// 0x02 0x07 0x01 0x03   0x02 0x06 0x06 0x03
        ///
        /// 0x04 0x68
        /// 0x02 0x07 0x01 0x1B   0x02 0x06 0x08 0x03
        ///
        /// 0x00
        /// </code>
        /// </example>
        /// <param name="sdaPin">0-31 (as used in a prior call to <see cref="BbI2COpen"/>)</param>
        /// <param name="inputBuffer">pointer to the concatenated I2C commands, see below</param>
        /// <param name="inputLength">size of command buffer</param>
        /// <param name="outputBuffer">pointer to buffer to hold returned data</param>
        /// <param name="outputLength">size of output buffer</param>
        /// <returns>Returns &gt;= 0 if OK (the number of bytes read), otherwise PI_BAD_USER_GPIO, PI_NOT_I2C_GPIO, PI_BAD_POINTER, PI_BAD_I2C_CMD, PI_BAD_I2C_RLEN, PI_BAD_I2C_WLEN, PI_I2C_READ_FAILED, or PI_I2C_WRITE_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "bbI2CZip")]
        public static extern int BbI2CZip(UserGpio sdaPin, [In, MarshalAs(UnmanagedType.LPArray)] byte[] inputBuffer, uint inputLength, [In, MarshalAs(UnmanagedType.LPArray)] byte[] outputBuffer, uint outputLength);

        #region Unmanaged Methods

        /// <summary>
        /// This returns a handle for the device at the address on the I2C bus.
        ///
        /// No flags are currently defined.  This parameter should be set to zero.
        ///
        /// Physically buses 0 and 1 are available on the Pi.  Higher numbered buses
        /// will be available if a kernel supported bus multiplexor is being used.
        ///
        /// For the SMBus commands the low level transactions are shown at the end
        /// of the function description.  The following abbreviations are used.
        ///
        /// </summary>
        /// <remarks>
        /// S      (1 bit) : Start bit
        /// P      (1 bit) : Stop bit
        /// Rd/Wr  (1 bit) : Read/Write bit. Rd equals 1, Wr equals 0.
        /// A, NA  (1 bit) : Accept and not accept bit.
        /// Addr   (7 bits): I2C 7 bit address.
        /// i2cReg (8 bits): Command byte, a byte which often selects a register.
        /// Data   (8 bits): A data byte.
        /// Count  (8 bits): A byte defining the length of a block operation.
        ///
        /// [..]: Data sent by the device.
        /// </remarks>
        /// <param name="i2cBus">&gt;=0</param>
        /// <param name="i2cAddr">0-0x7F</param>
        /// <param name="i2cFlags">0</param>
        /// <returns>Returns a handle (&gt;=0) if OK, otherwise PI_BAD_I2C_BUS, PI_BAD_I2C_ADDR, PI_BAD_FLAGS, PI_NO_HANDLE, or PI_I2C_OPEN_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cOpen")]
        private static extern int I2cOpenUnmanaged(uint i2cBus, uint i2cAddr, uint i2cFlags);

        /// <summary>
        /// This sends a single bit (in the Rd/Wr bit) to the device associated
        /// with handle.
        ///
        /// Quick command. SMBus 2.0 5.5.1
        /// </summary>
        /// <remarks>
        /// S Addr bit [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="bit">0-1, the value to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cWriteQuick")]
        private static extern ResultCode I2cWriteQuickUnmanaged(UIntPtr handle, uint bit);

        /// <summary>
        /// This sends a single byte to the device associated with handle.
        ///
        /// Send byte. SMBus 2.0 5.5.2
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] bVal [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="bVal">0-0xFF, the value to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cWriteByte")]
        private static extern ResultCode I2cWriteByteUnmanaged(UIntPtr handle, uint bVal);

        /// <summary>
        /// This reads a single byte from the device associated with handle.
        ///
        /// Receive byte. SMBus 2.0 5.5.3
        /// </summary>
        /// <remarks>
        /// S Addr Rd [A] [Data] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <returns>Returns the byte read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, or PI_I2C_READ_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cReadByte")]
        private static extern int I2cReadByteUnmanaged(UIntPtr handle);

        /// <summary>
        /// This writes a single byte to the specified register of the device
        /// associated with handle.
        ///
        /// Write byte. SMBus 2.0 5.5.4
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] bVal [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="i2cReg">0-255, the register to write</param>
        /// <param name="bVal">0-0xFF, the value to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cWriteByteData")]
        private static extern ResultCode I2cWriteByteDataUnmanaged(UIntPtr handle, uint i2cReg, uint bVal);

        /// <summary>
        /// This writes a single 16 bit word to the specified register of the device
        /// associated with handle.
        ///
        /// Write word. SMBus 2.0 5.5.4
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] wValLow [A] wValHigh [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to write</param>
        /// <param name="word">0-0xFFFF, the value to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cWriteWordData")]
        private static extern ResultCode I2cWriteWordDataUnmanaged(UIntPtr handle, uint register, uint word);

        /// <summary>
        /// This reads a single byte from the specified register of the device
        /// associated with handle.
        ///
        /// Read byte. SMBus 2.0 5.5.5
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] S Addr Rd [A] [Data] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to read</param>
        /// <returns>Returns the byte read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cReadByteData")]
        private static extern int I2cReadByteDataUnmanaged(UIntPtr handle, uint register);

        /// <summary>
        /// This reads a single 16 bit word from the specified register of the device
        /// associated with handle.
        ///
        /// Read word. SMBus 2.0 5.5.5
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] S Addr Rd [A] [DataLow] A [DataHigh] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to read</param>
        /// <returns>Returns the word read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cReadWordData")]
        private static extern int I2cReadWordDataUnmanaged(UIntPtr handle, uint register);

        /// <summary>
        /// This writes 16 bits of data to the specified register of the device
        /// associated with handle and reads 16 bits of data in return.
        ///
        /// Process call. SMBus 2.0 5.5.6
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] wValLow [A] wValHigh [A]
        ///    S Addr Rd [A] [DataLow] A [DataHigh] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to write/read</param>
        /// <param name="word">0-0xFFFF, the value to write</param>
        /// <returns>Returns the word read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cProcessCall")]
        private static extern int I2cProcessCallUnmanaged(UIntPtr handle, uint register, uint word);

        /// <summary>
        /// This writes up to 32 bytes to the specified register of the device
        /// associated with handle.
        ///
        /// Block write. SMBus 2.0 5.5.7
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] count [A]
        ///    buf0 [A] buf1 [A] ... [A] bufn [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to write</param>
        /// <param name="buffer">an array with the data to send</param>
        /// <param name="count">1-32, the number of bytes to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cWriteBlockData")]
        private static extern ResultCode I2cWriteBlockDataUnmanaged(UIntPtr handle, uint register, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

        /// <summary>
        /// This reads a block of up to 32 bytes from the specified register of
        /// the device associated with handle.
        ///
        /// The amount of returned data is set by the device.
        ///
        /// Block read. SMBus 2.0 5.5.7
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A]
        ///    S Addr Rd [A] [Count] A [buf0] A [buf1] A ... A [bufn] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to read</param>
        /// <param name="buffer">an array to receive the read data</param>
        /// <returns>Returns the number of bytes read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cReadBlockData")]
        private static extern int I2cReadBlockDataUnmanaged(UIntPtr handle, uint register, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer);

        /// <summary>
        /// This writes data bytes to the specified register of the device
        /// associated with handle and reads a device specified number
        /// of bytes of data in return.
        ///
        /// The SMBus 2.0 documentation states that a minimum of 1 byte may be
        /// sent and a minimum of 1 byte may be received.  The total number of
        /// bytes sent/received must be 32 or less.
        ///
        /// Block write-block read. SMBus 2.0 5.5.8
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] count [A] buf0 [A] ... bufn [A]
        ///    S Addr Rd [A] [Count] A [buf0] A ... [bufn] A P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to write/read</param>
        /// <param name="buffer">an array with the data to send and to receive the read data</param>
        /// <param name="count">1-32, the number of bytes to write</param>
        /// <returns>Returns the number of bytes read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cBlockProcessCall")]
        private static extern int I2cBlockProcessCallUnmanaged(UIntPtr handle, uint register, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

        /// <summary>
        /// This reads count bytes from the specified register of the device
        /// associated with handle .  The count may be 1-32.
        ///
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A]
        ///    S Addr Rd [A] [buf0] A [buf1] A ... A [bufn] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to read</param>
        /// <param name="buffer">an array to receive the read data</param>
        /// <param name="count">1-32, the number of bytes to read</param>
        /// <returns>Returns the number of bytes read (&gt;0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cReadI2CBlockData")]
        private static extern int I2cReadI2cBlockDataUnmanaged(UIntPtr handle, uint register, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

        /// <summary>
        /// This writes 1 to 32 bytes to the specified register of the device
        /// associated with handle.
        ///
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] i2cReg [A] buf0 [A] buf1 [A] ... [A] bufn [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="register">0-255, the register to write</param>
        /// <param name="buffer">the data to write</param>
        /// <param name="count">1-32, the number of bytes to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cWriteI2CBlockData")]
        private static extern ResultCode I2cWriteI2cBlockDataUnmanaged(UIntPtr handle, uint register, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

        /// <summary>
        /// This reads count bytes from the raw device into buf.
        ///
        /// </summary>
        /// <remarks>
        /// S Addr Rd [A] [buf0] A [buf1] A ... A [bufn] NA P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="buffer">an array to receive the read data bytes</param>
        /// <param name="count">&gt;0, the number of bytes to read</param>
        /// <returns>Returns count (&gt;0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_READ_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cReadDevice")]
        private static extern int I2cReadDeviceUnmanaged(UIntPtr handle, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

        /// <summary>
        /// This writes count bytes from buf to the raw device.
        ///
        /// </summary>
        /// <remarks>
        /// S Addr Wr [A] buf0 [A] buf1 [A] ... [A] bufn [A] P
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="I2cOpen"/></param>
        /// <param name="buffer">an array containing the data bytes to write</param>
        /// <param name="count">&gt;0, the number of bytes to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, or PI_I2C_WRITE_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "i2cWriteDevice")]
        private static extern ResultCode I2cWriteDeviceUnmanaged(UIntPtr handle, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

        #endregion
    }
}
