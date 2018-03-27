namespace Unosquare.PiGpio
{
    using Enums;
    using System;
    using System.Runtime.InteropServices;

    public static partial class NativeMethods
    {
        /// <summary>
        /// This function sends a trigger pulse to a GPIO.  The GPIO is set to
        /// level for pulseLen microseconds and then reset to not level.
        ///
        /// or PI_BAD_PULSELEN.
        /// </summary>
        /// <param name="user_gpio">0-31</param>
        /// <param name="pulseLen">1-100</param>
        /// <param name="level">0,1</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO, PI_BAD_LEVEL, or PI_BAD_PULSELEN.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioTrigger")]
        public static extern ResultCode GpioTrigger(Gpio user_gpio, uint pulseLen, Level level);

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
        /// <param name="user_gpio">0-31</param>
        /// <param name="timeout">0-60000</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_USER_GPIO or PI_BAD_WDOG_TIMEOUT.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetWatchdog")]
        public static extern ResultCode GpioSetWatchdog(Gpio user_gpio, uint timeout);

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
        /// The real value set by [*gpioPWM*] is (dutycycle * real range) / range.
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
        ///  800, 1000, 1250, 2000, 2500, 4000, 5000, 10000, 20000
        /// </remarks>
        /// <param name="user_gpio">0-31</param>
        /// <param name="range">25-40000</param>
        /// <returns>Returns the real range for the given GPIO's frequency if OK, otherwise PI_BAD_USER_GPIO or PI_BAD_DUTYRANGE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetPWMrange")]
        public static extern ResultCode GpioSetPWMrange(Gpio user_gpio, uint range);

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
        /// <param name="user_gpio">0-31</param>
        /// <returns>Returns the dutycycle range used for the GPIO if OK, otherwise PI_BAD_USER_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioGetPWMrange")]
        public static extern int GpioGetPWMrange(Gpio user_gpio);

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
        /// The frequencies for each sample rate are:
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
        ///             125   100    80   50   40   25   20   10    5
        /// </remarks>
        /// <param name="user_gpio">0-31</param>
        /// <param name="frequency">&gt;=0</param>
        /// <returns>Returns the numerically closest frequency if OK, otherwise PI_BAD_USER_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioSetPWMfrequency")]
        public static extern ResultCode GpioSetPWMfrequency(Gpio user_gpio, uint frequency);

        /// <summary>
        ///
        /// For normal PWM the frequency will be that defined for the GPIO by
        /// [*gpioSetPWMfrequency*].
        ///
        /// If a hardware clock is active on the GPIO the reported frequency
        /// will be that set by [*gpioHardwareClock*].
        ///
        /// If hardware PWM is active on the GPIO the reported frequency
        /// will be that set by [*gpioHardwarePWM*].
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// f = gpioGetPWMfrequency(23); // Get frequency used for GPIO23.
        /// </code>
        /// </example>
        /// <param name="user_gpio">0-31</param>
        /// <returns>Returns the frequency (in hertz) used for the GPIO if OK, otherwise PI_BAD_USER_GPIO.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioGetPWMfrequency")]
        public static extern int GpioGetPWMfrequency(Gpio user_gpio);

        /// <summary>
        /// Returns the current level of GPIO 0-31.
        /// </summary>
        /// <returns>Returns the current level of GPIO 0-31.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioRead_Bits_0_31")]
        public static extern uint GpioReadBits0To31();

        /// <summary>
        /// Returns the current level of GPIO 32-53.
        /// </summary>
        /// <returns>Returns the current level of GPIO 32-53.</returns>
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
        public static extern int GpioWriteBits0To31Clear(uint bits);

        /// <summary>
        /// Clears GPIO 32-53 if the corresponding bit (0-21) in bits is set.
        ///
        /// </summary>
        /// <param name="bits">a bit mask of GPIO to clear</param>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWrite_Bits_32_53_Clear")]
        public static extern int GpioWriteBits32To53Clear(uint bits);

        /// <summary>
        /// Sets GPIO 0-31 if the corresponding bit in bits is set.
        ///
        /// </summary>
        /// <param name="bits">a bit mask of GPIO to set</param>
        /// <returns>Returns 0 if OK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioWrite_Bits_0_31_Set")]
        public static extern int GpioWriteBits0To31Set(uint bits);

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
        public static extern int GpioWriteBits32To53Set(uint bits);

        /// <summary>
        /// Starts a new thread of execution with f as the main routine.
        ///
        /// The function is passed the single argument arg.
        ///
        /// The thread can be cancelled by passing the pointer to pthread_t to
        /// [*gpioStopThread*].
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// #include &lt;stdio.h&gt;
        /// #include &lt;pigpio.h&gt;
        ///
        /// void *myfunc(void *arg)
        /// {
        ///    while (1)
        ///    {
        ///       printf("%s", arg);
        ///       sleep(1);
        ///    }
        /// }
        ///
        /// int main(int argc, char *argv[])
        /// {
        ///    pthread_t *p1, *p2, *p3;
        ///
        ///    if (gpioInitialise() &lt; 0) return 1;
        ///
        ///    p1 = gpioStartThread(myfunc, "thread 1"); sleep(3);
        ///
        ///    p2 = gpioStartThread(myfunc, "thread 2"); sleep(3);
        ///
        ///    p3 = gpioStartThread(myfunc, "thread 3"); sleep(3);
        ///
        ///    gpioStopThread(p3); sleep(3);
        ///
        ///    gpioStopThread(p2); sleep(3);
        ///
        ///    gpioStopThread(p1); sleep(3);
        ///
        ///    gpioTerminate();
        /// }
        /// </code>
        /// </example>
        /// <param name="callback">the main function for the new thread</param>
        /// <param name="userData">a pointer to arbitrary user data</param>
        /// <returns>Returns a pointer to pthread_t if OK, otherwise NULL.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "*gpioStartThread")]
        public static extern UIntPtr GpioStartThread(GpioThreadDelegate callback, IntPtr userData);

        /// <summary>
        /// Cancels the thread pointed at by pth.
        ///
        /// No value is returned.
        ///
        /// The thread to be stopped should have been started with [*gpioStartThread*].
        /// </summary>
        /// <param name="threadHandle">a thread pointer returned by [*gpioStartThread*]</param>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioStopThread")]
        public static extern void GpioStopThread(UIntPtr threadHandle);
    }
}
