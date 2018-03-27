namespace Unosquare.PiGpio
{
    using Enums;
    using System.Runtime.InteropServices;

    public static partial class NativeMethods
    {
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
        public static extern ResultCode GpioSetMode(SystemGpio gpio, GpioMode mode);

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
        public static extern int GpioGetMode(SystemGpio gpio);

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
        /// <param name="pud">0-2</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO or PI_BAD_PUD.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetPullUpDown")]
        public static extern ResultCode GpioSetPullUpDown(SystemGpio gpio, GpioPullMode pud);

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
        public static extern int GpioRead(SystemGpio gpio);

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
        /// <param name="level">0-1</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_GPIO or PI_BAD_LEVEL.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWrite")]
        public static extern ResultCode GpioWrite(SystemGpio gpio, Level level);

        /// <summary>
        /// Starts PWM on the GPIO, dutycycle between 0 (off) and range (fully on).
        /// Range defaults to 255.
        ///
        /// Arduino style: analogWrite
        ///
        /// This and the servo functionality use the DMA and PWM or PCM peripherals
        /// to control and schedule the pulse lengths and dutycycles.
        ///
        /// The [*gpioSetPWMrange*] function may be used to change the default
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
        /// <param name="user_gpio">0-31</param>
        /// <param name="dutycycle">0-range</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO or PI_BAD_DUTYCYCLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioPWM")]
        public static extern ResultCode GpioPWM(Gpio user_gpio, uint dutycycle);

        /// <summary>
        ///
        /// For normal PWM the dutycycle will be out of the defined range
        /// for the GPIO (see [*gpioGetPWMrange*]).
        ///
        /// If a hardware clock is active on the GPIO the reported dutycycle
        /// will be 500000 (500k) out of 1000000 (1M).
        ///
        /// If hardware PWM is active on the GPIO the reported dutycycle
        /// will be out of a 1000000 (1M).
        ///
        /// Normal PWM range defaults to 255.
        /// </summary>
        /// <param name="user_gpio">0-31</param>
        /// <returns>Returns between 0 (off) and range (fully on) if OK, otherwise PI_BAD_USER_GPIO or PI_NOT_PWM_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioGetPWMdutycycle")]
        public static extern int GpioGetPWMdutycycle(Gpio user_gpio);

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
        /// Firstly set the desired PWM frequency using [*gpioSetPWMfrequency*].
        ///
        /// Then set the PWM range using [*gpioSetPWMrange*] to 1E6/frequency.
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
        /// gpioSetPWMrange(25, 2500);
        /// </remarks>
        /// <param name="user_gpio">0-31</param>
        /// <param name="pulsewidth">0, 500-2500</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO or PI_BAD_PULSEWIDTH.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioServo")]
        public static extern int GpioServo(Gpio user_gpio, uint pulsewidth);

        /// <summary>
        /// if OK, otherwise PI_BAD_USER_GPIO or PI_NOT_SERVO_GPIO.
        /// </summary>
        /// <param name="user_gpio">0-31</param>
        /// <returns>Returns 0 (off), 500 (most anti-clockwise) to 2500 (most clockwise) if OK, otherwise PI_BAD_USER_GPIO or PI_NOT_SERVO_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioGetServoPulsewidth")]
        public static extern int GpioGetServoPulsewidth(Gpio user_gpio);

        /// <summary>
        /// Delays for at least the number of microseconds specified by micros.
        /// Delays of 100 microseconds or less use busy waits.
        /// </summary>
        /// <param name="micros">the number of microseconds to sleep</param>
        /// <returns>Returns the actual length of the delay in microseconds.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioDelay")]
        public static extern uint GpioDelay(uint micros);

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
        /// [*gpioTick*] for more details.
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
        /// <param name="user_gpio">0-31</param>
        /// <param name="callback">the callback function</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetAlertFunc")]
        public static extern int GpioSetAlertFunc(Gpio user_gpio, GpioAlertDelegate callback);

        /// <summary>
        /// Registers a function to be called (a callback) every millis milliseconds.
        ///
        /// 10 timers are supported numbered 0 to 9.
        ///
        /// One function may be registered per timer.
        ///
        /// The timer may be cancelled by passing NULL as the function.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// void bFunction(void)
        /// {
        ///    printf("two seconds have elapsed");
        /// }
        ///
        /// // call bFunction every 2000 milliseconds
        /// gpioSetTimerFunc(0, 2000, bFunction);
        /// </code>
        /// </example>
        /// <param name="timer">0-9</param>
        /// <param name="millis">10-60000</param>
        /// <param name="callback">the function to call</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_TIMER, PI_BAD_MS, or PI_TIMER_FAILED.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetTimerFunc")]
        public static extern ResultCode GpioSetTimerFunc(HardwareTimer timer, uint millis, GpioTimerDelegate callback);
    }
}
