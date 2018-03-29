namespace Unosquare.PiGpio
{
    using Enums;
    using System;
    using System.Runtime.InteropServices;

    public static partial class NativeMethods
    {
        #region Unmanaged Methods
        
        /// <summary>
        /// If a hardware clock is active on the GPIO the reported real
        /// range will be 1000000 (1M).
        ///
        /// If hardware PWM is active on the GPIO the reported real range
        /// will be approximately 250M divided by the set PWM frequency.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// rr = gpioGetPWMrealRange(17);
        /// </code>
        /// </example>
        /// <param name="userGpio">0-31</param>
        /// <returns>Returns the real range used for the GPIO if OK, otherwise PI_BAD_USER_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioGetPWMrealRange")]
        private static extern int GpioGetPwmRealRangeUnmanaged(UserGpio userGpio);

        /// <summary>
        /// This function requests a free notification handle.
        ///
        /// A notification is a method for being notified of GPIO state changes
        /// via a pipe or socket.
        ///
        /// Pipe notifications for handle x will be available at the pipe
        /// named /dev/pigpiox (where x is the handle number).  E.g. if the
        /// function returns 15 then the notifications must be read
        /// from /dev/pigpio15.
        ///
        /// Socket notifications are returned to the socket which requested the
        /// handle.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// h = gpioNotifyOpen();
        ///
        /// if (h &gt;= 0)
        /// {
        ///    sprintf(str, "/dev/pigpio%d", h);
        ///
        ///    fd = open(str, O_RDONLY);
        ///
        ///    if (fd &gt;= 0)
        ///    {
        ///       // Okay.
        ///    }
        ///    else
        ///    {
        ///       // Error.
        ///    }
        /// }
        /// else
        /// {
        ///    // Error.
        /// }
        /// </code>
        /// </example>
        /// <returns>Returns a handle greater than or equal to zero if OK, otherwise PI_NO_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioNotifyOpen")]
        private static extern int GpioNotifyOpenUnmanaged();

        /// <summary>
        /// This function copies up to bufSize bytes of data read from the
        /// bit bang serial cyclic buffer to the buffer starting at buf.
        ///
        /// The bytes returned for each character depend upon the number of
        /// data bits <see cref="data_bits"/> specified in the <see cref="GpioSerialReadOpen"/> command.
        ///
        /// For <see cref="data_bits"/> 1-8 there will be one byte per character.
        /// For <see cref="data_bits"/> 9-16 there will be two bytes per character.
        /// For <see cref="data_bits"/> 17-32 there will be four bytes per character.
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

        /// <summary>
        /// This function returns the pad drive strength in mA.
        ///
        /// Pad @ GPIO
        /// 0   @ 0-27
        /// 1   @ 28-45
        /// 2   @ 46-53
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// strength = gpioGetPad(1); // get pad 1 strength
        /// </code>
        /// </example>
        /// <param name="pad">0-2, the pad to get</param>
        /// <returns>Returns the pad drive strength if OK, otherwise PI_BAD_PAD.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioGetPad")]
        private static extern int GpioGetPadUnmanaged(GpioPad pad);

        #endregion

        /// <summary>
        /// If a hardware clock is active on the GPIO the reported real
        /// range will be 1000000 (1M).
        ///
        /// If hardware PWM is active on the GPIO the reported real range
        /// will be approximately 250M divided by the set PWM frequency.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// rr = gpioGetPWMrealRange(17);
        /// </code>
        /// </example>
        /// <param name="userGpio">0-31</param>
        /// <returns>Returns the real range used for the GPIO if OK, otherwise PI_BAD_USER_GPIO.</returns>
        public static int GpioGetPwmRealRange(UserGpio userGpio)
        {
            return PiGpioException.ValidateResult(GpioGetPwmRealRangeUnmanaged(userGpio));
        }

        /// <summary>
        /// Registers a function to be called (a callback) when the specified
        /// GPIO changes state.
        ///
        /// One callback may be registered per GPIO.
        ///
        /// The callback is passed the GPIO, the new level, the tick, and
        /// the userData pointer.
        ///
        /// See <see cref="GpioSetAlertFunc"/> for further details.
        ///
        /// Only one of <see cref="GpioSetAlertFunc"/> or <see cref="GpioSetAlertFuncEx"/> can be
        /// registered per GPIO.
        /// </summary>
        /// <remarks>
        /// Parameter   Value    Meaning
        ///
        /// GPIO        0-31     The GPIO which has changed state
        ///
        /// level       0-2      0 = change to low (a falling edge)
        ///                      1 = change to high (a rising edge)
        ///                      2 = no level change (a watchdog timeout)
        ///
        /// tick        32 bit   The number of microseconds since boot
        ///                      WARNING: this wraps around from
        ///                      4294967295 to 0 roughly every 72 minutes
        ///
        /// userData    pointer  Pointer to an arbitrary object
        /// </remarks>
        /// <param name="userGpio">0-31</param>
        /// <param name="callback">the callback function</param>
        /// <param name="userData">pointer to arbitrary user data</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetAlertFuncEx")]
        public static extern ResultCode GpioSetAlertFuncEx(UserGpio userGpio, GpioAlertExDelegate callback, IntPtr userData);

        /// <summary>
        /// Registers a function to be called (a callback) whenever the specified
        /// GPIO interrupt occurs.
        ///
        /// One function may be registered per GPIO.
        ///
        /// The function is passed the GPIO, the current level, and the
        /// current tick.  The level will be PI_TIMEOUT if the optional
        /// interrupt timeout expires.
        ///
        /// The underlying Linux sysfs GPIO interface is used to provide
        /// the interrupt services.
        ///
        /// The first time the function is called, with a non-NULL f, the
        /// GPIO is exported, set to be an input, and set to interrupt
        /// on the given edge and timeout.
        ///
        /// Subsequent calls, with a non-NULL f, can vary one or more of the
        /// edge, timeout, or function.
        ///
        /// The ISR may be cancelled by passing a NULL f, in which case the
        /// GPIO is unexported.
        ///
        /// The tick is that read at the time the process was informed of
        /// the interrupt.  This will be a variable number of microseconds
        /// after the interrupt occurred.  Typically the latency will be of
        /// the order of 50 microseconds.  The latency is not guaranteed
        /// and will vary with system load.
        ///
        /// The level is that read at the time the process was informed of
        /// the interrupt, or PI_TIMEOUT if the optional interrupt timeout
        /// expired.  It may not be the same as the expected edge as
        /// interrupts happening in rapid succession may be missed by the
        /// kernel (i.e. this mechanism can not be used to capture several
        /// interrupts only a few microseconds apart).
        /// </summary>
        /// <remarks>
        /// Parameter   Value    Meaning
        ///
        /// GPIO        0-53     The GPIO which has changed state
        ///
        /// level       0-2      0 = change to low (a falling edge)
        ///                      1 = change to high (a rising edge)
        ///                      2 = no level change (interrupt timeout)
        ///
        /// tick        32 bit   The number of microseconds since boot
        ///                      WARNING: this wraps around from
        ///                      4294967295 to 0 roughly every 72 minutes
        /// </remarks>
        /// <param name="gpio">0-53</param>
        /// <param name="edge">RISING_EDGE, FALLING_EDGE, or EITHER_EDGE</param>
        /// <param name="timeout">interrupt timeout in milliseconds (&lt;=0 to cancel)</param>
        /// <param name="callback">the callback function</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO, PI_BAD_EDGE, or PI_BAD_ISR_INIT.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetISRFunc")]
        public static extern ResultCode GpioSetIsrFunc(SystemGpio gpio, EdgeDetection edge, int timeout, GpioISRDelegate callback);

        /// <summary>
        /// Registers a function to be called (a callback) whenever the specified
        /// GPIO interrupt occurs.
        ///
        /// The function is passed the GPIO, the current level, the
        /// current tick, and the userData pointer.
        ///
        /// Only one of <see cref="GpioSetIsrFunc"/> or <see cref="GpioSetIsrFuncEx"/> can be
        /// registered per GPIO.
        ///
        /// See <see cref="GpioSetIsrFunc"/> for further details.
        /// </summary>
        /// <remarks>
        /// Parameter   Value    Meaning
        ///
        /// GPIO        0-53     The GPIO which has changed state
        ///
        /// level       0-2      0 = change to low (a falling edge)
        ///                      1 = change to high (a rising edge)
        ///                      2 = no level change (interrupt timeout)
        ///
        /// tick        32 bit   The number of microseconds since boot
        ///                      WARNING: this wraps around from
        ///                      4294967295 to 0 roughly every 72 minutes
        ///
        /// userData    pointer  Pointer to an arbitrary object
        /// </remarks>
        /// <param name="gpio">0-53</param>
        /// <param name="edge">RISING_EDGE, FALLING_EDGE, or EITHER_EDGE</param>
        /// <param name="timeout">interrupt timeout in milliseconds (&lt;=0 to cancel)</param>
        /// <param name="callback">the callback function</param>
        /// <param name="userData">pointer to arbitrary user data</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO, PI_BAD_EDGE, or PI_BAD_ISR_INIT.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetISRFuncEx")]
        public static extern ResultCode GpioSetIsrFuncEx(SystemGpio gpio, EdgeDetection edge, int timeout, GpioISRExDelegate callback, IntPtr userData);

        /// <summary>
        /// Registers a function to be called (a callback) when a signal occurs.
        ///
        /// The function is passed the signal number.
        ///
        /// One function may be registered per signal.
        ///
        /// The callback may be cancelled by passing NULL.
        ///
        /// By default all signals are treated as fatal and cause the library
        /// to call gpioTerminate and then exit.
        /// </summary>
        /// <param name="signalNumber">0-63</param>
        /// <param name="f">the callback function</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_signalNumber.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetSignalFunc")]
        public static extern ResultCode GpioSetSignalFunc(uint signalNumber, GpioSignalDelegate f);

        /// <summary>
        /// Registers a function to be called (a callback) when a signal occurs.
        ///
        /// The function is passed the signal number and the userData pointer.
        ///
        /// Only one of gpioSetSignalFunc or gpioSetSignalFuncEx can be
        /// registered per signal.
        ///
        /// See gpioSetSignalFunc for further details.
        /// </summary>
        /// <param name="signalNumber">0-63</param>
        /// <param name="callback">the callback function</param>
        /// <param name="userData">a pointer to arbitrary user data</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_signalNumber.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetSignalFuncEx")]
        public static extern ResultCode GpioSetSignalFuncEx(uint signalNumber, GpioSignalExDelegate callback, IntPtr userData);

        /// <summary>
        /// Registers a function to be called (a callback) every millisecond
        /// with the latest GPIO samples.
        ///
        /// The function is passed a pointer to the samples (an array of
        /// <see cref="GpioSample"/>),  and the number of samples.
        ///
        /// Only one function can be registered.
        ///
        /// The callback may be cancelled by passing NULL as the function.
        ///
        /// The samples returned will be the union of bits, plus any active alerts,
        /// plus any active notifications.
        ///
        /// e.g.  if there are alerts for GPIO 7, 8, and 9, notifications for GPIO
        /// 8, 10, 23, 24, and bits is (1&lt;&lt;23)|(1&lt;&lt;17) then samples for GPIO
        /// 7, 8, 9, 10, 17, 23, and 24 will be reported.
        /// </summary>
        /// <param name="callback">the function to call</param>
        /// <param name="bits">the GPIO of interest</param>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetGetSamplesFunc")]
        public static extern ResultCode GpioSetGetSamplesFunc(GpioGetSamplesDelegate callback, uint bits);

        /// <summary>
        /// Registers a function to be called (a callback) every millisecond
        /// with the latest GPIO samples.
        ///
        /// The function is passed a pointer to the samples (an array of
        /// <see cref="GpioSample_t"/>), the number of samples, and the userData pointer.
        ///
        /// Only one of <see cref="GpioGetSamplesFunc"/> or <see cref="GpioGetSamplesFuncEx"/> can be
        /// registered.
        ///
        /// See <see cref="GpioSetGetSamplesFunc"/> for further details.
        /// </summary>
        /// <param name="callback">the function to call</param>
        /// <param name="bits">the GPIO of interest</param>
        /// <param name="userData">a pointer to arbitrary user data</param>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetGetSamplesFuncEx")]
        public static extern ResultCode GpioSetGetSamplesFuncEx(GpioGetSamplesExDelegate callback, uint bits, IntPtr userData);

        /// <summary>
        /// Registers a function to be called (a callback) every millis milliseconds.
        ///
        /// The function is passed the userData pointer.
        ///
        /// Only one of <see cref="GpioSetTimerFunc"/> or <see cref="GpioSetTimerFuncEx"/> can be
        /// registered per timer.
        ///
        /// See <see cref="GpioSetTimerFunc"/> for further details.
        /// </summary>
        /// <param name="timer">0-9.</param>
        /// <param name="millisecondsTimeout">10-60000</param>
        /// <param name="callback">the function to call</param>
        /// <param name="userData">a pointer to arbitrary user data</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_TIMER, PI_BAD_MS, or PI_TIMER_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetTimerFuncEx")]
        public static extern ResultCode GpioSetTimerFuncEx(TimerId timer, uint millisecondsTimeout, GpioTimerExDelegate callback, IntPtr userData);

        /// <summary>
        /// This function requests a free notification handle.
        ///
        /// A notification is a method for being notified of GPIO state changes
        /// via a pipe or socket.
        ///
        /// Pipe notifications for handle x will be available at the pipe
        /// named /dev/pigpiox (where x is the handle number).  E.g. if the
        /// function returns 15 then the notifications must be read
        /// from /dev/pigpio15.
        ///
        /// Socket notifications are returned to the socket which requested the
        /// handle.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// h = gpioNotifyOpen();
        ///
        /// if (h &gt;= 0)
        /// {
        ///    sprintf(str, "/dev/pigpio%d", h);
        ///
        ///    fd = open(str, O_RDONLY);
        ///
        ///    if (fd &gt;= 0)
        ///    {
        ///       // Okay.
        ///    }
        ///    else
        ///    {
        ///       // Error.
        ///    }
        /// }
        /// else
        /// {
        ///    // Error.
        /// }
        /// </code>
        /// </example>
        /// <returns>Returns a handle greater than or equal to zero if OK, otherwise PI_NO_HANDLE.</returns>
        public static UIntPtr GpioNotifyOpen()
        {
            var result = PiGpioException.ValidateResult(GpioNotifyOpenUnmanaged());
            return new UIntPtr((uint)result);
        }

        /// <summary>
        /// This function requests a free notification handle.
        ///
        /// It differs from <see cref="GpioNotifyOpen"/> in that the pipe size may be
        /// specified, whereas <see cref="GpioNotifyOpen"/> uses the default pipe size.
        ///
        /// See <see cref="GpioNotifyOpen"/> for further details.
        /// </summary>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioNotifyOpenWithSize")]
        public static extern ResultCode GpioNotifyOpenWithSize(int bufferSize);

        /// <summary>
        /// This function starts notifications on a previously opened handle.
        ///
        /// The notification sends state changes for each GPIO whose corresponding
        /// bit in bits is set.
        ///
        /// Each notification occupies 12 bytes in the fifo and has the
        /// following structure.
        ///
        /// seqno: starts at 0 each time the handle is opened and then increments
        /// by one for each report.
        ///
        /// flags: three flags are defined, PI_NTFY_FLAGS_WDOG,
        /// PI_NTFY_FLAGS_ALIVE, and PI_NTFY_FLAGS_EVENT.
        ///
        /// If bit 5 is set (PI_NTFY_FLAGS_WDOG) then bits 0-4 of the flags
        /// indicate a GPIO which has had a watchdog timeout.
        ///
        /// If bit 6 is set (PI_NTFY_FLAGS_ALIVE) this indicates a keep alive
        /// signal on the pipe/socket and is sent once a minute in the absence
        /// of other notification activity.
        ///
        /// If bit 7 is set (PI_NTFY_FLAGS_EVENT) then bits 0-4 of the flags
        /// indicate an event which has been triggered.
        ///
        /// tick: the number of microseconds since system boot.  It wraps around
        /// after 1h12m.
        ///
        /// level: indicates the level of each GPIO.  If bit 1&lt;&lt;x is set then
        /// GPIO x is high.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// // Start notifications for GPIO 1, 4, 6, 7, 10.
        ///
        /// //                         1
        /// //                         0  76 4  1
        /// // (1234 = 0x04D2 = 0b0000010011010010)
        ///
        /// gpioNotifyBegin(h, 1234);
        /// </code>
        /// </example>
        /// <remarks>
        /// typedef struct
        /// {
        ///    uint16_t seqno;
        ///    uint16_t flags;
        ///    uint tick;
        ///    uint level;
        /// } gpioReport_t;
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by <see cref="GpioNotifyOpen"/></param>
        /// <param name="bits">a bit mask indicating the GPIO of interest</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioNotifyBegin")]
        public static extern ResultCode GpioNotifyBegin(UIntPtr handle, uint bits);

        /// <summary>
        /// This function pauses notifications on a previously opened handle.
        ///
        /// Notifications for the handle are suspended until <see cref="GpioNotifyBegin"/>
        /// is called again.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioNotifyPause(h);
        /// </code>
        /// </example>
        /// <param name="handle">&gt;=0, as returned by <see cref="GpioNotifyOpen"/></param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioNotifyPause")]
        public static extern ResultCode GpioNotifyPause(UIntPtr handle);

        /// <summary>
        /// This function stops notifications on a previously opened handle
        /// and releases the handle for reuse.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioNotifyClose(h);
        /// </code>
        /// </example>
        /// <param name="handle">&gt;=0, as returned by <see cref="GpioNotifyOpen"/></param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioNotifyClose")]
        public static extern ResultCode GpioNotifyClose(UIntPtr handle);

        /// <summary>
        /// This function opens a GPIO for bit bang reading of serial data.
        ///
        /// The serial data is returned in a cyclic buffer and is read using
        /// <see cref="GpioSerialRead"/>.
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
        /// <returns></returns>
        public static int GpioSerialRead(UserGpio userGpio, byte[] buffer, int readLength)
        {
            var result = PiGpioException.ValidateResult(GpioSerialReadUnmanaged(userGpio, buffer, (uint)readLength));
            return result;
        }

        /// <summary>
        /// Wrapper for the native <see cref="GpioSerialReadUnmanaged"/>
        /// </summary>
        /// <param name="userGpio">The user gpio.</param>
        /// <param name="readLength">Length of the read.</param>
        /// <returns></returns>
        public static byte[] GpioSerialRead(UserGpio userGpio, int readLength)
        {
            var buffer = new byte[readLength];
            var result = PiGpioException.ValidateResult(GpioSerialReadUnmanaged(userGpio, buffer, (uint)readLength));
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

        /// <summary>
        /// Starts a hardware clock on a GPIO at the specified frequency.
        /// Frequencies above 30MHz are unlikely to work.
        ///
        /// The same clock is available on multiple GPIO.  The latest
        /// frequency setting will be used by all GPIO which share a clock.
        ///
        /// The GPIO must be one of the following.
        ///
        /// Access to clock 1 is protected by a password as its use will likely
        /// crash the Pi.  The password is given by or'ing 0x5A000000 with the
        /// GPIO number.
        /// </summary>
        /// <remarks>
        /// 4   clock 0  All models
        /// 5   clock 1  All models but A and B (reserved for system use)
        /// 6   clock 2  All models but A and B
        /// 20  clock 0  All models but A and B
        /// 21  clock 1  All models but A and Rev.2 B (reserved for system use)
        ///
        /// 32  clock 0  Compute module only
        /// 34  clock 0  Compute module only
        /// 42  clock 1  Compute module only (reserved for system use)
        /// 43  clock 2  Compute module only
        /// 44  clock 1  Compute module only (reserved for system use)
        /// </remarks>
        /// <param name="gpio">see description</param>
        /// <param name="clockFrequency">0 (off) or 4689-250000000 (250M)</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO, PI_NOT_HCLK_GPIO, PI_BAD_HCLK_FREQ,or PI_BAD_HCLK_PASS.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioHardwareClock")]
        public static extern ResultCode GpioHardwareClock(SystemGpio gpio, uint clockFrequency);

        /// <summary>
        /// Starts hardware PWM on a GPIO at the specified frequency and dutycycle.
        /// Frequencies above 30MHz are unlikely to work.
        ///
        /// NOTE: Any waveform started by <see cref="GpioWaveTxSend"/>, or
        /// <see cref="GpioWaveChain"/> will be cancelled.
        ///
        /// This function is only valid if the pigpio main clock is PCM.  The
        /// main clock defaults to PCM but may be overridden by a call to
        /// <see cref="GpioCfgClock"/>.
        ///
        /// The same PWM channel is available on multiple GPIO.  The latest
        /// frequency and dutycycle setting will be used by all GPIO which
        /// share a PWM channel.
        ///
        /// The GPIO must be one of the following.
        ///
        /// The actual number of steps beween off and fully on is the
        /// integral part of 250 million divided by PWMfreq.
        ///
        /// The actual frequency set is 250 million / steps.
        ///
        /// There will only be a million steps for a PWMfreq of 250.
        /// Lower frequencies will have more steps and higher
        /// frequencies will have fewer steps.  PWMduty is
        /// automatically scaled to take this into account.
        /// </summary>
        /// <remarks>
        /// 12  PWM channel 0  All models but A and B
        /// 13  PWM channel 1  All models but A and B
        /// 18  PWM channel 0  All models
        /// 19  PWM channel 1  All models but A and B
        ///
        /// 40  PWM channel 0  Compute module only
        /// 41  PWM channel 1  Compute module only
        /// 45  PWM channel 1  Compute module only
        /// 52  PWM channel 0  Compute module only
        /// 53  PWM channel 1  Compute module only
        /// </remarks>
        /// <param name="gpio">see description</param>
        /// <param name="pwmFrequency">0 (off) or 1-125000000 (125M)</param>
        /// <param name="pwmDytuCycle">0 (off) to 1000000 (1M)(fully on)</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO, PI_NOT_HPWM_GPIO, PI_BAD_HPWM_DUTY, PI_BAD_HPWM_FREQ, or PI_HPWM_ILLEGAL.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioHardwarePWM")]
        public static extern ResultCode GpioHardwarePwm(SystemGpio gpio, uint pwmFrequency, uint pwmDytuCycle);

        /// <summary>
        /// Sets a glitch filter on a GPIO.
        ///
        /// Level changes on the GPIO are not reported unless the level
        /// has been stable for at least <see cref="steady"/> microseconds.  The
        /// level is then reported.  Level changes of less than <see cref="steady"/>
        /// microseconds are ignored.
        ///
        /// This filter affects the GPIO samples returned to callbacks set up
        /// with <see cref="GpioSetAlertFunc"/>, <see cref="GpioSetAlertFuncEx"/>, <see cref="GpioSetGetSamplesFunc"/>,
        /// and <see cref="GpioSetGetSamplesFuncEx"/>.
        ///
        /// It does not affect interrupts set up with <see cref="GpioSetIsrFunc"/>,
        /// <see cref="GpioSetIsrFuncEx"/>, or levels read by <see cref="GpioRead"/>,
        /// <see cref="GpioRead_Bits_0_31"/>, or <see cref="GpioRead_Bits_32_53"/>.
        ///
        /// Each (stable) edge will be timestamped <see cref="steady"/> microseconds
        /// after it was first detected.
        /// </summary>
        /// <param name="userGpio">0-31</param>
        /// <param name="steadyMicroseconds">0-300000</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, or PI_BAD_FILTER.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioGlitchFilter")]
        public static extern ResultCode GpioGlitchFilter(UserGpio userGpio, uint steadyMicroseconds);

        /// <summary>
        /// Sets a noise filter on a GPIO.
        ///
        /// Level changes on the GPIO are ignored until a level which has
        /// been stable for <see cref="steady"/> microseconds is detected.  Level changes
        /// on the GPIO are then reported for <see cref="active"/> microseconds after
        /// which the process repeats.
        ///
        /// This filter affects the GPIO samples returned to callbacks set up
        /// with <see cref="GpioSetAlertFunc"/>, <see cref="GpioSetAlertFuncEx"/>, <see cref="GpioSetGetSamplesFunc"/>,
        /// and <see cref="GpioSetGetSamplesFuncEx"/>.
        ///
        /// It does not affect interrupts set up with <see cref="GpioSetIsrFunc"/>,
        /// <see cref="GpioSetIsrFuncEx"/>, or levels read by <see cref="GpioRead"/>,
        /// <see cref="GpioRead_Bits_0_31"/>, or <see cref="GpioRead_Bits_32_53"/>.
        ///
        /// Level changes before and after the active period may
        /// be reported.  Your software must be designed to cope with
        /// such reports.
        /// </summary>
        /// <param name="userGpio">0-31</param>
        /// <param name="steadyMicroseconds">0-300000</param>
        /// <param name="activeMicroseconds">0-1000000</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, or PI_BAD_FILTER.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioNoiseFilter")]
        public static extern ResultCode GpioNoiseFilter(UserGpio userGpio, uint steadyMicroseconds, uint activeMicroseconds);

        /// <summary>
        /// This function returns the pad drive strength in mA.
        ///
        /// Pad @ GPIO
        /// 0   @ 0-27
        /// 1   @ 28-45
        /// 2   @ 46-53
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// strength = gpioGetPad(1); // get pad 1 strength
        /// </code>
        /// </example>
        /// <param name="pad">0-2, the pad to get</param>
        /// <returns>Returns the pad drive strength if OK, otherwise PI_BAD_PAD.</returns>
        public static GpioPadStrength GpioGetPad(GpioPad pad)
        {
            var result = PiGpioException.ValidateResult(GpioGetPadUnmanaged(pad));
            return (GpioPadStrength)result;
        }

        /// <summary>
        /// This function sets the pad drive strength in mA.
        ///
        /// Pad @ GPIO
        /// 0   @ 0-27
        /// 1   @ 28-45
        /// 2   @ 46-53
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioSetPad(0, 16); // set pad 0 strength to 16 mA
        /// </code>
        /// </example>
        /// <param name="pad">0-2, the pad to set</param>
        /// <param name="padStrength">1-16 mA</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_PAD, or PI_BAD_STRENGTH.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetPad")]
        public static extern ResultCode GpioSetPad(GpioPad pad, GpioPadStrength padStrength);

        /// <summary>
        /// This function uses the system call to execute a shell script
        /// with the given string as its parameter.
        ///
        /// The exit status of the system call is returned if OK, otherwise
        /// PI_BAD_SHELL_STATUS.
        ///
        /// scriptName must exist in /opt/pigpio/cgi and must be executable.
        ///
        /// The returned exit status is normally 256 times that set by the
        /// shell script exit function.  If the script can't be found 32512 will
        /// be returned.
        ///
        /// The following table gives some example returned statuses.
        ///
        /// Script exit status @ Returned system call status
        /// 1                  @ 256
        /// 5                  @ 1280
        /// 10                 @ 2560
        /// 200                @ 51200
        /// script not found   @ 32512
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// // pass two parameters, hello and world
        /// status = shell("scr1", "hello world");
        ///
        /// // pass three parameters, hello, string with spaces, and world
        /// status = shell("scr1", "hello 'string with spaces' world");
        ///
        /// // pass one parameter, hello string with spaces world
        /// status = shell("scr1", "\"hello string with spaces world\"");
        /// </code>
        /// </example>
        /// <remarks>
        ///               '-' and '_' are allowed in the name
        /// </remarks>
        /// <param name="scriptName">the name of the script, only alphanumeric characters,</param>
        /// <param name="scriptString">the string to pass to the script</param>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "shell")]
        public static extern ResultCode Shell(string scriptName, string scriptString);
    }
}
