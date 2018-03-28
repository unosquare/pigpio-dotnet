namespace Unosquare.PiGpio
{
    using NativeTypes;
    using System.Runtime.InteropServices;

    public static partial class NativeMethods
    {
        /// <summary>
        /// This function adds a waveform representing SPI data to the
        /// existing waveform (if any).
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="spi">a pointer to a spi object</param>
        /// <param name="offset">microseconds from the start of the waveform</param>
        /// <param name="spiSS">the slave select GPIO</param>
        /// <param name="buf">the bits to transmit, most significant bit first</param>
        /// <param name="spiTxBits">the number of bits to write</param>
        /// <param name="spiBitFirst">the first bit to read</param>
        /// <param name="spiBitLast">the last bit to read</param>
        /// <param name="spiBits">the number of bits to transfer</param>
        /// <returns>Returns the new total number of pulses in the current waveform if OK, otherwise PI_BAD_USER_GPIO, PI_BAD_SER_OFFSET, or PI_TOO_MANY_PULSES.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawWaveAddSPI")]
        public static extern int RawWaveAddSPI(RawSpiData spi, uint offset, uint spiSS, byte[] buf, uint spiTxBits, uint spiBitFirst, uint spiBitLast, uint spiBits);

        /// <summary>
        /// This function adds a number of pulses to the current waveform.
        ///
        /// The advantage of this function over gpioWaveAddGeneric is that it
        /// allows the setting of the flags field.
        ///
        /// The pulses are interleaved in time order within the existing waveform
        /// (if any).
        ///
        /// Merging allows the waveform to be built in parts, that is the settings
        /// for GPIO#1 can be added, and then GPIO#2 etc.
        ///
        /// If the added waveform is intended to start after or within the existing
        /// waveform then the first pulse should consist of a delay.
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="numPulses">the number of pulses</param>
        /// <param name="pulses">the array containing the pulses</param>
        /// <returns>Returns the new total number of pulses in the current waveform if OK, otherwise PI_TOO_MANY_PULSES.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawWaveAddGeneric")]
        public static extern int RawWaveAddGeneric(uint numPulses, [MarshalAs(UnmanagedType.LPArray)] RawWave[] pulses);

        /// <summary>
        /// Not intended for general use.
        /// </summary>
        /// <returns>Returns the number of the cb being currently output.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawWaveCB")]
        public static extern uint RawWaveCB();

        /// <summary>
        /// Return the (Linux) address of contol block cbNum.
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="cbNum">the cb of interest</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawWaveCBAdr")]
        public static extern RawCBS RawWaveCBAdr(int cbNum);

        /// <summary>
        /// Gets the OOL parameter stored at pos.
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="pos">the position of interest.</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawWaveGetOOL")]
        public static extern uint RawWaveGetOOL(int pos);

        /// <summary>
        /// Sets the OOL parameter stored at pos to value.
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="pos">the position of interest</param>
        /// <param name="lVal">the value to write</param>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawWaveSetOOL")]
        public static extern void RawWaveSetOOL(int pos, uint lVal);

        /// <summary>
        /// Gets the wave output parameter stored at pos.
        ///
        /// DEPRECATED: use rawWaveGetOOL instead.
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="pos">the position of interest.</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawWaveGetOut")]
        public static extern uint RawWaveGetOut(int pos);

        /// <summary>
        /// Sets the wave output parameter stored at pos to value.
        ///
        /// DEPRECATED: use rawWaveSetOOL instead.
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="pos">the position of interest</param>
        /// <param name="lVal">the value to write</param>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawWaveSetOut")]
        public static extern void RawWaveSetOut(int pos, uint lVal);

        /// <summary>
        /// Gets the wave input value parameter stored at pos.
        ///
        /// DEPRECATED: use rawWaveGetOOL instead.
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="pos">the position of interest</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawWaveGetIn")]
        public static extern uint RawWaveGetIn(int pos);

        /// <summary>
        /// Sets the wave input value stored at pos to value.
        ///
        /// DEPRECATED: use rawWaveSetOOL instead.
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="pos">the position of interest</param>
        /// <param name="lVal">the value to write</param>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawWaveSetIn")]
        public static extern void RawWaveSetIn(int pos, uint lVal);

        /// <summary>
        /// Gets details about the wave with id wave_id.
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="waveId">the wave of interest</param>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawWaveInfo")]
        [return: MarshalAs(UnmanagedType.Struct)]
        public static extern RawWaveInformation RawWaveInfo(int waveId);

        /// <summary>
        /// Used to print a readable version of the current waveform to stderr.
        ///
        /// Not intended for general use.
        /// </summary>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawDumpWave")]
        public static extern void RawDumpWave();

        /// <summary>
        /// Used to print a readable version of a script to stderr.
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="script_id">&gt;=0, a script_id returned by <see cref="GpioStoreScript"/></param>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "rawDumpScript")]
        public static extern void RawDumpScript(uint script_id);
    }
}
