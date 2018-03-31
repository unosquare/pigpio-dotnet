namespace Unosquare.PiGpio.NativeMethods
{
    using NativeEnums;
    using NativeTypes;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Defines basic IO methods for the GPIO
    /// </summary>
    public static partial class IO
    {
        #region Single Bit Reads / Writes

        /// <summary>
        /// Sets the GPIO mode, typically input or output.
        ///
        /// Arduino style: pinMode.
        ///
        /// See [[http://www.raspberrypi.org/documentation/hardware/raspberrypi/bcm2835/BCM2835-ARM-Peripherals.pdf]] page 102 for an overview of the modes.
        /// </summary>
        /// <example>
        /// <code>
        /// gpioSetMode(17, PI_INPUT);  // Set GPIO17 as input.
        ///
        /// gpioSetMode(18, PI_OUTPUT); // Set GPIO18 as output.
        ///
        /// gpioSetMode(22,PI_ALT0);    // Set GPIO22 to alternative mode 0.
        /// </code>
        /// </example>
        /// <param name="gpio">0-53</param>
        /// <param name="mode">0-7</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO or PI_BAD_MODE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetMode")]
        public static extern ResultCode GpioSetMode(SystemGpio gpio, PortMode mode);

        /// <summary>
        /// Gest the current mode for the given GPIO
        /// </summary>
        /// <param name="gpio">The gpio.</param>
        /// <returns>The port mode</returns>
        public static PortMode GpioGetMode(SystemGpio gpio)
        {
            var result = PiGpioException.ValidateResult(GpioGetModeUnmanaged(gpio));
            return (PortMode)result;
        }

        /// <summary>
        /// Sets or clears resistor pull ups or downs on the GPIO.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioSetPullUpDown(17, PI_PUD_UP);   // Sets a pull-up.
        ///
        /// gpioSetPullUpDown(18, PI_PUD_DOWN); // Sets a pull-down.
        ///
        /// gpioSetPullUpDown(23, PI_PUD_OFF);  // Clear any pull-ups/downs.
        /// </code>
        /// </example>
        /// <param name="gpio">0-53</param>
        /// <param name="pullMode">0-2</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO or PI_BAD_PUD.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetPullUpDown")]
        public static extern ResultCode GpioSetPullUpDown(SystemGpio gpio, GpioPullMode pullMode);

        /// <summary>
        /// Reads the value of the GPIO
        /// </summary>
        /// <param name="gpio">The gpio.</param>
        /// <returns>The digital value</returns>
        public static bool GpioRead(SystemGpio gpio)
        {
            var result = PiGpioException.ValidateResult(GpioReadUnmanaged(gpio));
            return (DigitalValue)result == DigitalValue.True;
        }

        /// <summary>
        /// Sets the GPIO level, on or off.
        ///
        /// If PWM or servo pulses are active on the GPIO they are switched off.
        ///
        /// Arduino style: digitalWrite
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioWrite(24, 1); // Set GPIO24 high.
        /// </code>
        /// </example>
        /// <param name="gpio">0-53</param>
        /// <param name="value">0-1</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO or PI_BAD_LEVEL.</returns>
        public static ResultCode GpioWrite(SystemGpio gpio, bool value)
        {
            return GpioWriteUnmanaged(gpio, value ? DigitalValue.True : DigitalValue.False);
        }

        /// <summary>
        /// This function sends a trigger pulse to a GPIO.  The GPIO is set to
        /// level for pulseLen microseconds and then reset to not level.
        ///
        /// or PI_BAD_PULSELEN.
        /// </summary>
        /// <param name="userGpio">0-31</param>
        /// <param name="pulseLength">1-100</param>
        /// <param name="value">0,1</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, PI_BAD_LEVEL, or PI_BAD_PULSELEN.</returns>
        public static ResultCode GpioTrigger(UserGpio userGpio, uint pulseLength, bool value)
        {
            return GpioTriggerUnmanaged(userGpio, pulseLength, value ? DigitalValue.True : DigitalValue.False);
        }

        #endregion

        #region Bank Reads / Writes

        /// <summary>
        /// Returns the current level of GPIO 0-31.
        /// </summary>
        /// <returns>The current level of GPIO 0-31.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioRead_Bits_0_31")]
        public static extern uint GpioReadBits00To31();

        /// <summary>
        /// Returns the current level of GPIO 32-53.
        /// </summary>
        /// <returns>The current level of GPIO 32-53.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioRead_Bits_32_53")]
        public static extern uint GpioReadBits32To53();

        /// <summary>
        /// Clears GPIO 0-31 if the corresponding bit in bits is set.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// // To clear (set to 0) GPIO 4, 7, and 15
        /// gpioWrite_Bits_0_31_Clear( (1&lt;&lt;4) | (1&lt;&lt;7) | (1&lt;&lt;15) );
        /// </code>
        /// </example>
        /// <param name="bits">a bit mask of GPIO to clear</param>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWrite_Bits_0_31_Clear")]
        public static extern ResultCode GpioWriteBits00To31Clear(BitMask bits);

        /// <summary>
        /// Clears GPIO 32-53 if the corresponding bit (0-31) in bits is set.
        ///
        /// </summary>
        /// <param name="bits">a bit mask of GPIO to clear</param>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWrite_Bits_32_53_Clear")]
        public static extern ResultCode GpioWriteBits32To53Clear(BitMask bits);

        /// <summary>
        /// Sets GPIO 0-31 if the corresponding bit in bits is set.
        ///
        /// </summary>
        /// <param name="bits">a bit mask of GPIO to set</param>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWrite_Bits_0_31_Set")]
        public static extern ResultCode GpioWriteBits00To31Set(BitMask bits);

        /// <summary>
        /// Sets GPIO 32-53 if the corresponding bit (0-21) in bits is set.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// // To set (set to 1) GPIO 32, 40, and 53
        /// gpioWrite_Bits_32_53_Set((1&lt;&lt;(32-32)) | (1&lt;&lt;(40-32)) | (1&lt;&lt;(53-32)));
        /// </code>
        /// </example>
        /// <param name="bits">a bit mask of GPIO to set</param>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWrite_Bits_32_53_Set")]
        public static extern ResultCode GpioWriteBits32To53Set(BitMask bits);

        #endregion

        #region Digital Monitoring and Callbacks

        /// <summary>
        /// Registers a function to be called (a callback) when the specified
        /// GPIO changes state.
        ///
        /// One callback may be registered per GPIO.
        ///
        /// The callback is passed the GPIO, the new level, and the tick.
        ///
        /// The alert may be cancelled by passing NULL as the function.
        ///
        /// The GPIO are sampled at a rate set when the library is started.
        ///
        /// If a value isn't specifically set the default of 5 us is used.
        ///
        /// The number of samples per second is given in the following table.
        ///
        /// Level changes shorter than the sample rate may be missed.
        ///
        /// The thread which calls the alert functions is triggered nominally
        /// 1000 times per second.  The active alert functions will be called
        /// once per level change since the last time the thread was activated.
        /// i.e. The active alert functions will get all level changes but there
        /// will be a latency.
        ///
        /// The tick value is the time stamp of the sample in microseconds, see
        /// <see cref="Utilities.GpioTick"/> for more details.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// void aFunction(int gpio, int level, uint tick)
        /// {
        ///    printf("GPIO %d became %d at %d", gpio, level, tick);
        /// }
        ///
        /// // call aFunction whenever GPIO 4 changes state
        ///
        /// gpioSetAlertFunc(4, aFunction);
        /// </code>
        /// </example>
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
        ///               samples
        ///               per sec
        ///
        ///          1  1,000,000
        ///          2    500,000
        /// sample   4    250,000
        /// rate     5    200,000
        /// (us)     8    125,000
        ///         10    100,000
        /// </remarks>
        /// <param name="userGpio">0-31</param>
        /// <param name="callback">the callback function</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetAlertFunc")]
        public static extern ResultCode GpioSetAlertFunc(UserGpio userGpio, [In, MarshalAs(UnmanagedType.FunctionPtr)] PiGpioAlertDelegate callback);

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
        public static extern ResultCode GpioSetAlertFuncEx(UserGpio userGpio, [In, MarshalAs(UnmanagedType.FunctionPtr)] PiGpioAlertExDelegate callback, UIntPtr userData);

        /// <summary>
        /// Sets a watchdog for a GPIO.
        ///
        /// The watchdog is nominally in milliseconds.
        ///
        /// One watchdog may be registered per GPIO.
        ///
        /// The watchdog may be cancelled by setting timeout to 0.
        ///
        /// Until cancelled a timeout will be reported every timeout milliseconds
        /// after the last GPIO activity.
        ///
        /// In particular:
        ///
        /// 1) any registered alert function for the GPIO will be called with
        ///    the level set to PI_TIMEOUT.
        ///
        /// 2) any notification for the GPIO will have a report written to the
        ///    fifo with the flags set to indicate a watchdog timeout.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// void aFunction(int gpio, int level, uint tick)
        /// {
        ///    printf("GPIO %d became %d at %d", gpio, level, tick);
        /// }
        ///
        /// // call aFunction whenever GPIO 4 changes state
        /// gpioSetAlertFunc(4, aFunction);
        ///
        /// //  or approximately every 5 millis
        /// gpioSetWatchdog(4, 5);
        /// </code>
        /// </example>
        /// <param name="userGpio">0-31</param>
        /// <param name="timeoutMilliseconds">0-60000</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO or PI_BAD_WDOG_TIMEOUT.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetWatchdog")]
        public static extern ResultCode GpioSetWatchdog(UserGpio userGpio, uint timeoutMilliseconds);

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
        public static extern ResultCode GpioSetIsrFunc(SystemGpio gpio, EdgeDetection edge, int timeout, [In, MarshalAs(UnmanagedType.FunctionPtr)] PiGpioIsrDelegate callback);

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
        public static extern ResultCode GpioSetIsrFuncEx(SystemGpio gpio, EdgeDetection edge, int timeout, [In, MarshalAs(UnmanagedType.FunctionPtr)] PiGpioIsrExDelegate callback, UIntPtr userData);

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
        public static extern ResultCode GpioSetSignalFunc(uint signalNumber, [In, MarshalAs(UnmanagedType.FunctionPtr)] PiGpioSignalDelegate f);

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
        public static extern ResultCode GpioSetSignalFuncEx(uint signalNumber, [In, MarshalAs(UnmanagedType.FunctionPtr)] PiGpioSignalExDelegate callback, UIntPtr userData);

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
        public static extern ResultCode GpioSetGetSamplesFunc([In, MarshalAs(UnmanagedType.FunctionPtr)] PiGpioGetSamplesDelegate callback, BitMask bits);

        /// <summary>
        /// Registers a function to be called (a callback) every millisecond
        /// with the latest GPIO samples.
        ///
        /// The function is passed a pointer to the samples (an array of
        /// <see cref="GpioSample"/>), the number of samples, and the userData pointer.
        ///
        /// Only one of <see cref="GpioSetGetSamplesFunc"/> or <see cref="GpioSetGetSamplesFuncEx"/> can be
        /// registered.
        ///
        /// See <see cref="GpioSetGetSamplesFunc"/> for further details.
        /// </summary>
        /// <param name="callback">the function to call</param>
        /// <param name="bits">the GPIO of interest</param>
        /// <param name="userData">a pointer to arbitrary user data</param>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetGetSamplesFuncEx")]
        public static extern ResultCode GpioSetGetSamplesFuncEx([In, MarshalAs(UnmanagedType.FunctionPtr)] PiGpioGetSamplesExDelegate callback, BitMask bits, UIntPtr userData);

        #endregion

        #region Digital Filtering (for Alerts)

        /// <summary>
        /// Sets a glitch filter on a GPIO.
        ///
        /// Level changes on the GPIO are not reported unless the level
        /// has been stable for at least <paramref name="steadyMicroseconds"/> microseconds.  The
        /// level is then reported.  Level changes of less than <paramref name="steadyMicroseconds"/>
        /// microseconds are ignored.
        ///
        /// This filter affects the GPIO samples returned to callbacks set up
        /// with <see cref="GpioSetAlertFunc"/>, <see cref="GpioSetAlertFuncEx"/>, <see cref="GpioSetGetSamplesFunc"/>,
        /// and <see cref="GpioSetGetSamplesFuncEx"/>.
        ///
        /// It does not affect interrupts set up with <see cref="GpioSetIsrFunc"/>,
        /// <see cref="GpioSetIsrFuncEx"/>, or levels read by <see cref="GpioRead"/>,
        /// <see cref="GpioReadBits00To31"/>, or <see cref="GpioReadBits32To53"/>.
        ///
        /// Each (stable) edge will be timestamped <paramref name="steadyMicroseconds"/> microseconds
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
        /// been stable for <paramref name="steadyMicroseconds"/> microseconds is detected.  Level changes
        /// on the GPIO are then reported for <paramref name="activeMicroseconds"/> microseconds after
        /// which the process repeats.
        ///
        /// This filter affects the GPIO samples returned to callbacks set up
        /// with <see cref="GpioSetAlertFunc"/>, <see cref="GpioSetAlertFuncEx"/>, <see cref="GpioSetGetSamplesFunc"/>,
        /// and <see cref="GpioSetGetSamplesFuncEx"/>.
        ///
        /// It does not affect interrupts set up with <see cref="GpioSetIsrFunc"/>,
        /// <see cref="GpioSetIsrFuncEx"/>, or levels read by <see cref="GpioRead"/>,
        /// <see cref="GpioReadBits00To31"/>, or <see cref="GpioReadBits32To53"/>.
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

        #endregion

        #region Electrical Pads

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
        public static GpioPadStrength GpioGetPad(GpioPadId pad)
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
        public static extern ResultCode GpioSetPad(GpioPadId pad, GpioPadStrength padStrength);

        #endregion

        #region Unmanaged Methods

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
        private static extern int GpioGetPadUnmanaged(GpioPadId pad);

        /// <summary>
        /// Gets the GPIO mode.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// if (gpioGetMode(17) != PI_ALT0)
        /// {
        ///    gpioSetMode(17, PI_ALT0);  // set GPIO17 to ALT0
        /// }
        /// </code>
        /// </example>
        /// <param name="gpio">0-53</param>
        /// <returns>Returns the GPIO mode if OK, otherwise PI_BAD_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioGetMode")]
        private static extern int GpioGetModeUnmanaged(SystemGpio gpio);

        /// <summary>
        /// Reads the GPIO level, on or off.
        ///
        /// Arduino style: digitalRead.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// printf("GPIO24 is level %d", gpioRead(24));
        /// </code>
        /// </example>
        /// <param name="gpio">0-53</param>
        /// <returns>Returns the GPIO level if OK, otherwise PI_BAD_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioRead")]
        private static extern int GpioReadUnmanaged(SystemGpio gpio);

        /// <summary>
        /// Sets the GPIO level, on or off.
        ///
        /// If PWM or servo pulses are active on the GPIO they are switched off.
        ///
        /// Arduino style: digitalWrite
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioWrite(24, 1); // Set GPIO24 high.
        /// </code>
        /// </example>
        /// <param name="gpio">0-53</param>
        /// <param name="value">0-1</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO or PI_BAD_LEVEL.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWrite")]
        private static extern ResultCode GpioWriteUnmanaged(SystemGpio gpio, DigitalValue value);

        /// <summary>
        /// This function sends a trigger pulse to a GPIO.  The GPIO is set to
        /// level for pulseLen microseconds and then reset to not level.
        ///
        /// or PI_BAD_PULSELEN.
        /// </summary>
        /// <param name="userGpio">0-31</param>
        /// <param name="pulseLength">1-100</param>
        /// <param name="value">0,1</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, PI_BAD_LEVEL, or PI_BAD_PULSELEN.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioTrigger")]
        private static extern ResultCode GpioTriggerUnmanaged(UserGpio userGpio, uint pulseLength, DigitalValue value);

        #endregion
    }
}
