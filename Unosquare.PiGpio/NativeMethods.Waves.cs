namespace Unosquare.PiGpio
{
    using System.Runtime.InteropServices;
    using NativeTypes;

    public static partial class NativeMethods
    {
        /// <summary>
        /// This function clears all waveforms and any data added by calls to the
        /// [*gpioWaveAdd**] functions.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioWaveClear();
        /// </code>
        /// </example>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveClear")]
        public static extern int GpioWaveClear();

        /// <summary>
        /// This function starts a new empty waveform.
        ///
        /// You wouldn't normally need to call this function as it is automatically
        /// called after a waveform is created with the [*gpioWaveCreate*] function.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioWaveAddNew();
        /// </code>
        /// </example>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveAddNew")]
        public static extern int GpioWaveAddNew();

        /// <summary>
        /// This function adds a number of pulses to the current waveform.
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
        /// </summary>
        /// <example>
        /// <code>
        /// // Construct and send a 30 microsecond square wave.
        ///
        /// gpioSetMode(gpio, PI_OUTPUT);
        ///
        /// pulse[0].gpioOn = (1&lt;&lt;gpio);
        /// pulse[0].gpioOff = 0;
        /// pulse[0].usDelay = 15;
        ///
        /// pulse[1].gpioOn = 0;
        /// pulse[1].gpioOff = (1&lt;&lt;gpio);
        /// pulse[1].usDelay = 15;
        ///
        /// gpioWaveAddNew();
        ///
        /// gpioWaveAddGeneric(2, pulse);
        ///
        /// wave_id = gpioWaveCreate();
        ///
        /// if (wave_id &gt;= 0)
        /// {
        ///    gpioWaveTxSend(wave_id, PI_WAVE_MODE_REPEAT);
        ///
        ///    // Transmit for 30 seconds.
        ///
        ///    sleep(30);
        ///
        ///    gpioWaveTxStop();
        /// }
        /// else
        /// {
        ///    // Wave create failed.
        /// }
        /// </code>
        /// </example>
        /// <param name="numPulses">the number of pulses</param>
        /// <param name="pulses">an array of pulses</param>
        /// <returns>Returns the new total number of pulses in the current waveform if OK, otherwise PI_TOO_MANY_PULSES.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveAddGeneric")]
        public static extern int GpioWaveAddGeneric(uint numPulses, [MarshalAs(UnmanagedType.LPArray)] GpioPulse[] pulses);

        /// <summary>
        /// This function adds a waveform representing serial data to the
        /// existing waveform (if any).  The serial data starts offset
        /// microseconds from the start of the waveform.
        ///
        /// NOTES:
        ///
        /// The serial data is formatted as one start bit, data_bits data bits, and
        /// stop_bits/2 stop bits.
        ///
        /// It is legal to add serial data streams with different baud rates to
        /// the same waveform.
        ///
        /// numBytes is the number of bytes of data in str.
        ///
        /// The bytes required for each character depend upon data_bits.
        ///
        /// For data_bits 1-8 there will be one byte per character.
        /// For data_bits 9-16 there will be two bytes per character.
        /// For data_bits 17-32 there will be four bytes per character.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// #define MSG_LEN 8
        ///
        /// int i;
        /// char *str;
        /// char data[MSG_LEN];
        ///
        /// str = "Hello world!";
        ///
        /// gpioWaveAddSerial(4, 9600, 8, 2, 0, strlen(str), str);
        ///
        /// for (i=0; i&lt;MSG_LEN; i++) data[i] = i;
        ///
        /// // Data added is offset 1 second from the waveform start.
        /// gpioWaveAddSerial(4, 9600, 8, 2, 1000000, MSG_LEN, data);
        /// </code>
        /// </example>
        /// <param name="user_gpio">0-31</param>
        /// <param name="baud">50-1000000</param>
        /// <param name="data_bits">1-32</param>
        /// <param name="stop_bits">2-8</param>
        /// <param name="offset">&gt;=0</param>
        /// <param name="numBytes">&gt;=1</param>
        /// <param name="str">an array of chars (which may contain nulls)</param>
        /// <returns>Returns the new total number of pulses in the current waveform if OK, otherwise PI_BAD_USER_GPIO, PI_BAD_WAVE_BAUD, PI_BAD_DATABITS, PI_BAD_STOPBITS, PI_TOO_MANY_CHARS, PI_BAD_SER_OFFSET, or PI_TOO_MANY_PULSES.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveAddSerial")]
        public static extern int GpioWaveAddSerial(uint user_gpio, uint baud, uint data_bits, uint stop_bits, uint offset, uint numBytes, byte[] str);

        /// <summary>
        /// This function creates a waveform from the data provided by the prior
        /// calls to the [*gpioWaveAdd**] functions.  Upon success a wave id
        /// greater than or equal to 0 is returned, otherwise PI_EMPTY_WAVEFORM,
        /// PI_TOO_MANY_CBS, PI_TOO_MANY_OOL, or PI_NO_WAVEFORM_ID.
        ///
        /// The data provided by the [*gpioWaveAdd**] functions is consumed by this
        /// function.
        ///
        /// As many waveforms may be created as there is space available.  The
        /// wave id is passed to [*gpioWaveTxSend*] to specify the waveform to transmit.
        ///
        /// Normal usage would be
        ///
        /// Step 1. [*gpioWaveClear*] to clear all waveforms and added data.
        ///
        /// Step 2. [*gpioWaveAdd**] calls to supply the waveform data.
        ///
        /// Step 3. [*gpioWaveCreate*] to create the waveform and get a unique id
        ///
        /// Repeat steps 2 and 3 as needed.
        ///
        /// Step 4. [*gpioWaveTxSend*] with the id of the waveform to transmit.
        ///
        /// A waveform comprises one of more pulses.  Each pulse consists of a
        /// [*gpioPulse_t*] structure.
        ///
        /// The fields specify
        ///
        /// 1) the GPIO to be switched on at the start of the pulse.
        /// 2) the GPIO to be switched off at the start of the pulse.
        /// 3) the delay in microseconds before the next pulse.
        ///
        /// Any or all the fields can be zero.  It doesn't make any sense to
        /// set all the fields to zero (the pulse will be ignored).
        ///
        /// When a waveform is started each pulse is executed in order with the
        /// specified delay between the pulse and the next.
        ///
        /// PI_NO_WAVEFORM_ID, PI_TOO_MANY_CBS, or PI_TOO_MANY_OOL.
        /// </summary>
        /// <remarks>
        /// typedef struct
        /// {
        ///    uint gpioOn;
        ///    uint gpioOff;
        ///    uint usDelay;
        /// } gpioPulse_t;
        /// </remarks>
        /// <returns>Returns the new waveform id if OK, otherwise PI_EMPTY_WAVEFORM, PI_NO_WAVEFORM_ID, PI_TOO_MANY_CBS, or PI_TOO_MANY_OOL.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveCreate")]
        public static extern int GpioWaveCreate();

        /// <summary>
        /// This function deletes the waveform with id wave_id.
        ///
        /// The wave is flagged for deletion.  The resources used by the wave
        /// will only be reused when either of the following apply.
        ///
        /// - all waves with higher numbered wave ids have been deleted or have
        /// been flagged for deletion.
        ///
        /// - a new wave is created which uses exactly the same resources as
        /// the current wave (see the C source for gpioWaveCreate for details).
        ///
        /// Wave ids are allocated in order, 0, 1, 2, etc.
        ///
        /// </summary>
        /// <param name="wave_id">&gt;=0, as returned by [*gpioWaveCreate*]</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_WAVE_ID.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveDelete")]
        public static extern int GpioWaveDelete(uint wave_id);

        /// <summary>
        /// This function transmits the waveform with id wave_id.  The mode
        /// determines whether the waveform is sent once or cycles endlessly.
        /// The SYNC variants wait for the current waveform to reach the
        /// end of a cycle or finish before starting the new waveform.
        ///
        /// WARNING: bad things may happen if you delete the previous
        /// waveform before it has been synced to the new waveform.
        ///
        /// NOTE: Any hardware PWM started by [*gpioHardwarePWM*] will be cancelled.
        ///
        /// otherwise PI_BAD_WAVE_ID, or PI_BAD_WAVE_MODE.
        /// </summary>
        /// <remarks>
        ///            PI_WAVE_MODE_ONE_SHOT_SYNC, PI_WAVE_MODE_REPEAT_SYNC
        /// </remarks>
        /// <param name="wave_id">&gt;=0, as returned by [*gpioWaveCreate*]</param>
        /// <param name="wave_mode">PI_WAVE_MODE_ONE_SHOT, PI_WAVE_MODE_REPEAT,</param>
        /// <returns>Returns the number of DMA control blocks in the waveform if OK, otherwise PI_BAD_WAVE_ID, or PI_BAD_WAVE_MODE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveTxSend")]
        public static extern int GpioWaveTxSend(uint wave_id, uint wave_mode);

        /// <summary>
        /// This function transmits a chain of waveforms.
        ///
        /// NOTE: Any hardware PWM started by [*gpioHardwarePWM*] will be cancelled.
        ///
        /// The waves to be transmitted are specified by the contents of buf
        /// which contains an ordered list of [*wave_id*]s and optional command
        /// codes and related data.
        ///
        /// Each wave is transmitted in the order specified.  A wave may
        /// occur multiple times per chain.
        ///
        /// A blocks of waves may be transmitted multiple times by using
        /// the loop commands. The block is bracketed by loop start and
        /// end commands.  Loops may be nested.
        ///
        /// Delays between waves may be added with the delay command.
        ///
        /// The following command codes are supported:
        ///
        /// Name         @ Cmd & Data @ Meaning
        /// Loop Start   @ 255 0      @ Identify start of a wave block
        /// Loop Repeat  @ 255 1 x y  @ loop x + y*256 times
        /// Delay        @ 255 2 x y  @ delay x + y*256 microseconds
        /// Loop Forever @ 255 3      @ loop forever
        ///
        /// If present Loop Forever must be the last entry in the chain.
        ///
        /// The code is currently dimensioned to support a chain with roughly
        /// 600 entries and 20 loop counters.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// #include &lt;stdio.h&gt;
        /// #include &lt;pigpio.h&gt;
        ///
        /// #define WAVES 5
        /// #define GPIO 4
        ///
        /// int main(int argc, char *argv[])
        /// {
        ///    int i, wid[WAVES];
        ///
        ///    if (gpioInitialise()&lt;0) return -1;
        ///
        ///    gpioSetMode(GPIO, PI_OUTPUT);
        ///
        ///    printf("start piscope, press return"); getchar();
        ///
        ///    for (i=0; i&lt;WAVES; i++)
        ///    {
        ///       gpioWaveAddGeneric(2, (gpioPulse_t[])
        ///          {{1&lt;&lt;GPIO, 0,        20},
        ///           {0, 1&lt;&lt;GPIO, (i+1)*200}});
        ///
        ///       wid[i] = gpioWaveCreate();
        ///    }
        ///
        ///    gpioWaveChain((char []) {
        ///       wid[4], wid[3], wid[2],       // transmit waves 4+3+2
        ///       255, 0,                       // loop start
        ///          wid[0], wid[0], wid[0],    // transmit waves 0+0+0
        ///          255, 0,                    // loop start
        ///             wid[0], wid[1],         // transmit waves 0+1
        ///             255, 2, 0x88, 0x13,     // delay 5000us
        ///          255, 1, 30, 0,             // loop end (repeat 30 times)
        ///          255, 0,                    // loop start
        ///             wid[2], wid[3], wid[0], // transmit waves 2+3+0
        ///             wid[3], wid[1], wid[2], // transmit waves 3+1+2
        ///          255, 1, 10, 0,             // loop end (repeat 10 times)
        ///       255, 1, 5, 0,                 // loop end (repeat 5 times)
        ///       wid[4], wid[4], wid[4],       // transmit waves 4+4+4
        ///       255, 2, 0x20, 0x4E,           // delay 20000us
        ///       wid[0], wid[0], wid[0],       // transmit waves 0+0+0
        ///
        ///       }, 46);
        ///
        ///    while (gpioWaveTxBusy()) time_sleep(0.1);
        ///
        ///    for (i=0; i&lt;WAVES; i++) gpioWaveDelete(wid[i]);
        ///
        ///    printf("stop piscope, press return"); getchar();
        ///
        ///    gpioTerminate();
        /// }
        /// </code>
        /// </example>
        /// <param name="buf">pointer to the wave_ids and optional command codes</param>
        /// <param name="bufSize">the number of bytes in buf</param>
        /// <returns>Returns 0 if OK, otherwise PI_CHAIN_NESTING, PI_CHAIN_LOOP_CNT, PI_BAD_CHAIN_LOOP, PI_BAD_CHAIN_CMD, PI_CHAIN_COUNTER, PI_BAD_CHAIN_DELAY, PI_CHAIN_TOO_BIG, or PI_BAD_WAVE_ID.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveChain")]
        public static extern int GpioWaveChain(byte[] buf, uint bufSize);

        /// <summary>
        /// This function returns the id of the waveform currently being
        /// transmitted.
        ///
        /// PI_WAVE_NOT_FOUND (9998) - transmitted wave not found.
        /// PI_NO_TX_WAVE (9999) - no wave being transmitted.
        /// </summary>
        /// <returns>Returns the waveform id or one of the following special values:</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveTxAt")]
        public static extern int GpioWaveTxAt();

        /// <summary>
        /// This function checks to see if a waveform is currently being
        /// transmitted.
        ///
        /// </summary>
        /// <returns>Returns 1 if a waveform is currently being transmitted, otherwise 0.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveTxBusy")]
        public static extern int GpioWaveTxBusy();

        /// <summary>
        /// This function aborts the transmission of the current waveform.
        ///
        /// This function is intended to stop a waveform started in repeat mode.
        /// </summary>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveTxStop")]
        public static extern int GpioWaveTxStop();

        /// <summary>
        /// This function returns the length in microseconds of the current
        /// waveform.
        /// </summary>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveGetMicros")]
        public static extern int GpioWaveGetMicros();

        /// <summary>
        /// This function returns the length in microseconds of the longest waveform
        /// created since [*gpioInitialise*] was called.
        /// </summary>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveGetHighMicros")]
        public static extern int GpioWaveGetHighMicros();

        /// <summary>
        /// This function returns the maximum possible size of a waveform in
        /// microseconds.
        /// </summary>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveGetMaxMicros")]
        public static extern int GpioWaveGetMaxMicros();

        /// <summary>
        /// This function returns the length in pulses of the current waveform.
        /// </summary>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveGetPulses")]
        public static extern int GpioWaveGetPulses();

        /// <summary>
        /// This function returns the length in pulses of the longest waveform
        /// created since [*gpioInitialise*] was called.
        /// </summary>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveGetHighPulses")]
        public static extern int GpioWaveGetHighPulses();

        /// <summary>
        /// This function returns the maximum possible size of a waveform in pulses.
        /// </summary>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveGetMaxPulses")]
        public static extern int GpioWaveGetMaxPulses();

        /// <summary>
        /// This function returns the length in DMA control blocks of the current
        /// waveform.
        /// </summary>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveGetCbs")]
        public static extern int GpioWaveGetCbs();

        /// <summary>
        /// This function returns the length in DMA control blocks of the longest
        /// waveform created since [*gpioInitialise*] was called.
        /// </summary>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveGetHighCbs")]
        public static extern int GpioWaveGetHighCbs();

        /// <summary>
        /// This function returns the maximum possible size of a waveform in DMA
        /// control blocks.
        /// </summary>
        /// <returns>The result code. 0 for success. See the ErroeCodes enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWaveGetMaxCbs")]
        public static extern int GpioWaveGetMaxCbs();
    }
}
