namespace Unosquare.PiGpio.NativeMethods.InProcess.DllImports
{
    using NativeEnums;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Provides methods for software and hardware based PWM services for the GPIO pins.
    /// All User GPIO pins support PWM.
    /// </summary>
    internal static class Pwm
    {
        /// <summary>
        /// Starts PWM on the GPIO, dutycycle between 0 (off) and range (fully on).
        /// Range defaults to 255.
        ///
        /// Arduino style: analogWrite
        ///
        /// This and the servo functionality use the DMA and PWM or PCM peripherals
        /// to control and schedule the pulse lengths and dutycycles.
        ///
        /// The <see cref="GpioSetPwmRange"/> function may be used to change the default
        /// range of 255.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioPWM(17, 255); // Sets GPIO17 full on.
        ///
        /// gpioPWM(18, 128); // Sets GPIO18 half on.
        ///
        /// gpioPWM(23, 0);   // Sets GPIO23 full off.
        /// </code>
        /// </example>
        /// <param name="userGpio">0-31.</param>
        /// <param name="dutyCycle">0-range.</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO or PI_BAD_DUTYCYCLE.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioPWM")]
        internal static extern ResultCode GpioPwm(UserGpio userGpio, uint dutyCycle);

        /// <summary>
        /// Gets the PWM duty cycle.
        /// </summary>
        /// <param name="userGpio">The user gpio.</param>
        /// <returns>The PWM duty cycle.</returns>
        internal static uint GpioGetPwmDutyCycle(UserGpio userGpio)
        {
            return BoardException.ValidateResult(GpioGetPwmDutyCycleUnmanaged(userGpio));
        }

        /// <summary>
        /// Starts servo pulses on the GPIO, 0 (off), 500 (most anti-clockwise) to
        /// 2500 (most clockwise).
        ///
        /// The range supported by servos varies and should probably be determined
        /// by experiment.  A value of 1500 should always be safe and represents
        /// the mid-point of rotation.  You can DAMAGE a servo if you command it
        /// to move beyond its limits.
        ///
        /// The following causes an on pulse of 1500 microseconds duration to be
        /// transmitted on GPIO 17 at a rate of 50 times per second. This will
        /// command a servo connected to GPIO 17 to rotate to its mid-point.
        ///
        /// OTHER UPDATE RATES:
        ///
        /// This function updates servos at 50Hz.  If you wish to use a different
        /// update frequency you will have to use the PWM functions.
        ///
        /// Firstly set the desired PWM frequency using <see cref="GpioSetPwmFrequency"/>.
        ///
        /// Then set the PWM range using <see cref="GpioSetPwmRange"/> to 1E6/frequency.
        /// Doing this allows you to use units of microseconds when setting
        /// the servo pulsewidth.
        ///
        /// E.g. If you want to update a servo connected to GPIO25 at 400Hz
        ///
        /// Thereafter use the PWM command to move the servo,
        /// e.g. gpioPWM(25, 1500) will set a 1500 us pulse.
        /// </summary>
        /// <example>
        /// <code>
        /// gpioServo(17, 1000); // Move servo to safe position anti-clockwise.
        ///
        /// gpioServo(23, 1500); // Move servo to centre position.
        ///
        /// gpioServo(25, 2000); // Move servo to safe position clockwise.
        /// </code>
        /// </example>
        /// <remarks>
        /// PWM Hz    50   100  200  400  500
        /// 1E6/Hz 20000 10000 5000 2500 2000
        /// gpioSetPWMfrequency(25, 400);
        ///
        /// gpioSetPWMrange(25, 2500);.
        /// </remarks>
        /// <param name="userGpio">0-31.</param>
        /// <param name="pulseWidth">0, 500-2500.</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO or PI_BAD_PULSEWIDTH.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioServo")]
        internal static extern ResultCode GpioServo(UserGpio userGpio, uint pulseWidth);

        /// <summary>
        /// Returns 0 (off), 500 (most anti-clockwise) to 2500 (most clockwise).
        /// </summary>
        /// <param name="userGpio">The user gpio.</param>
        /// <returns>The Servo pulse width.</returns>
        internal static uint GpioGetServoPulseWidth(UserGpio userGpio)
        {
            return BoardException.ValidateResult(GpioGetServoPulseWidthUnmanaged(userGpio));
        }

        /// <summary>
        /// Selects the dutycycle range to be used for the GPIO.  Subsequent calls
        /// to gpioPWM will use a dutycycle between 0 (off) and range (fully on).
        ///
        /// If PWM is currently active on the GPIO its dutycycle will be scaled
        /// to reflect the new range.
        ///
        /// The real range, the number of steps between fully off and fully
        /// on for each frequency, is given in the following table.
        ///
        /// The real value set by <see cref="GpioPwm"/> is (dutycycle * real range) / range.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioSetPWMrange(24, 2000); // Now 2000 is fully on
        ///                            //     1000 is half on
        ///                            //      500 is quarter on, etc.
        /// </code>
        /// </example>
        /// <remarks>
        ///   25,   50,  100,  125,  200,  250,  400,   500,   625,
        ///  800, 1000, 1250, 2000, 2500, 4000, 5000, 10000, 20000.
        /// </remarks>
        /// <param name="userGpio">0-31.</param>
        /// <param name="range">25-40000.</param>
        /// <returns>Returns the real range for the given GPIO's frequency if OK, otherwise PI_BAD_USER_GPIO or PI_BAD_DUTYRANGE.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioSetPWMrange")]
        internal static extern ResultCode GpioSetPwmRange(UserGpio userGpio, uint range);

        /// <summary>
        ///
        /// If a hardware clock or hardware PWM is active on the GPIO
        /// the reported range will be 1000000 (1M).
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// r = gpioGetPWMrange(23);
        /// </code>
        /// </example>
        /// <param name="userGpio">0-31.</param>
        /// <returns>Returns the dutycycle range used for the GPIO if OK, otherwise PI_BAD_USER_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioGetPWMrange")]
        internal static extern int GpioGetPwmRange(UserGpio userGpio);

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
        /// <param name="userGpio">0-31.</param>
        /// <returns>Returns the real range used for the GPIO if OK, otherwise PI_BAD_USER_GPIO.</returns>
        internal static uint GpioGetPwmRealRange(UserGpio userGpio)
        {
            return BoardException.ValidateResult(GpioGetPwmRealRangeUnmanaged(userGpio));
        }

        /// <summary>
        /// Sets the frequency in hertz to be used for the GPIO.
        ///
        /// If PWM is currently active on the GPIO it will be
        /// switched off and then back on at the new frequency.
        ///
        /// Each GPIO can be independently set to one of 18 different PWM
        /// frequencies.
        ///
        /// The selectable frequencies depend upon the sample rate which
        /// may be 1, 2, 4, 5, 8, or 10 microseconds (default 5).
        ///
        /// The frequencies for each sample rate are:.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioSetPWMfrequency(23, 0); // Set GPIO23 to lowest frequency.
        ///
        /// gpioSetPWMfrequency(24, 500); // Set GPIO24 to 500Hz.
        ///
        /// gpioSetPWMfrequency(25, 100000); // Set GPIO25 to highest frequency.
        /// </code>
        /// </example>
        /// <remarks>
        ///                        Hertz
        ///
        ///        1: 40000 20000 10000 8000 5000 4000 2500 2000 1600
        ///            1250  1000   800  500  400  250  200  100   50
        ///
        ///        2: 20000 10000  5000 4000 2500 2000 1250 1000  800
        ///             625   500   400  250  200  125  100   50   25
        ///
        ///        4: 10000  5000  2500 2000 1250 1000  625  500  400
        ///             313   250   200  125  100   63   50   25   13
        /// sample
        ///  rate
        ///  (us)  5:  8000  4000  2000 1600 1000  800  500  400  320
        ///             250   200   160  100   80   50   40   20   10
        ///
        ///        8:  5000  2500  1250 1000  625  500  313  250  200
        ///             156   125   100   63   50   31   25   13    6
        ///
        ///       10:  4000  2000  1000  800  500  400  250  200  160
        ///             125   100    80   50   40   25   20   10    5.
        /// </remarks>
        /// <param name="userGpio">0-31.</param>
        /// <param name="frequency">&gt;=0.</param>
        /// <returns>Returns the numerically closest frequency if OK, otherwise PI_BAD_USER_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioSetPWMfrequency")]
        internal static extern ResultCode GpioSetPwmFrequency(UserGpio userGpio, uint frequency);

        /// <summary>
        ///
        /// For normal PWM the frequency will be that defined for the GPIO by
        /// <see cref="GpioSetPwmFrequency"/>.
        ///
        /// If a hardware clock is active on the GPIO the reported frequency
        /// will be that set by <see cref="GpioHardwareClock"/>.
        ///
        /// If hardware PWM is active on the GPIO the reported frequency
        /// will be that set by <see cref="GpioHardwarePwm"/>.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// f = gpioGetPWMfrequency(23); // Get frequency used for GPIO23.
        /// </code>
        /// </example>
        /// <param name="userGpio">0-31.</param>
        /// <returns>Returns the frequency (in hertz) used for the GPIO if OK, otherwise PI_BAD_USER_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioGetPWMfrequency")]
        internal static extern int GpioGetPwmFrequency(UserGpio userGpio);

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
        /// 44  clock 1  Compute module only (reserved for system use).
        /// </remarks>
        /// <param name="gpio">see description.</param>
        /// <param name="clockFrequency">0 (off) or 4689-250000000 (250M).</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO, PI_NOT_HCLK_GPIO, PI_BAD_HCLK_FREQ,or PI_BAD_HCLK_PASS.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioHardwareClock")]
        internal static extern ResultCode GpioHardwareClock(SystemGpio gpio, uint clockFrequency);

        /// <summary>
        /// Starts hardware PWM on a GPIO at the specified frequency and dutycycle.
        /// Frequencies above 30MHz are unlikely to work.
        ///
        /// NOTE: Any waveform started by <see cref="Waves.GpioWaveTxSend"/>, or
        /// <see cref="Waves.GpioWaveChain"/> will be cancelled.
        ///
        /// This function is only valid if the pigpio main clock is PCM.  The
        /// main clock defaults to PCM but may be overridden by a call to
        /// <see cref="Setup.GpioCfgClock"/>.
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
        /// 53  PWM channel 1  Compute module only.
        /// </remarks>
        /// <param name="gpio">see description.</param>
        /// <param name="pwmFrequency">0 (off) or 1-125000000 (125M).</param>
        /// <param name="pwmDytuCycle">0 (off) to 1000000 (1M)(fully on).</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO, PI_NOT_HPWM_GPIO, PI_BAD_HPWM_DUTY, PI_BAD_HPWM_FREQ, or PI_HPWM_ILLEGAL.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioHardwarePWM")]
        internal static extern ResultCode GpioHardwarePwm(SystemGpio gpio, uint pwmFrequency, uint pwmDutyCycle);

        #region Unmanaged Methods

        /// <summary>
        /// if OK, otherwise PI_BAD_USER_GPIO or PI_NOT_SERVO_GPIO.
        /// </summary>
        /// <param name="userGpio">0-31.</param>
        /// <returns>Returns 0 (off), 500 (most anti-clockwise) to 2500 (most clockwise) if OK, otherwise PI_BAD_USER_GPIO or PI_NOT_SERVO_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioGetServoPulsewidth")]
        private static extern int GpioGetServoPulseWidthUnmanaged(UserGpio userGpio);

        /// <summary>
        ///
        /// For normal PWM the dutycycle will be out of the defined range
        /// for the GPIO (see <see cref="GpioGetPwmRange"/>).
        ///
        /// If a hardware clock is active on the GPIO the reported dutycycle
        /// will be 500000 (500k) out of 1000000 (1M).
        ///
        /// If hardware PWM is active on the GPIO the reported dutycycle
        /// will be out of a 1000000 (1M).
        ///
        /// Normal PWM range defaults to 255.
        /// </summary>
        /// <param name="userGpio">0-31.</param>
        /// <returns>Returns between 0 (off) and range (fully on) if OK, otherwise PI_BAD_USER_GPIO or PI_NOT_PWM_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioGetPWMdutycycle")]
        private static extern int GpioGetPwmDutyCycleUnmanaged(UserGpio userGpio);

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
        /// <param name="userGpio">0-31.</param>
        /// <returns>Returns the real range used for the GPIO if OK, otherwise PI_BAD_USER_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioGetPWMrealRange")]
        internal static extern int GpioGetPwmRealRangeUnmanaged(UserGpio userGpio);

        #endregion
    }
}
