namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using NativeTypes;
    using System;

    /// <summary>
    /// Provides GPIO pin functionality to report on alerts based
    /// on sampling of 5 microseconds approximately.
    /// </summary>
    public sealed class GpioPinAlertService : GpioPinServiceBase
    {
        private readonly object SyncLock = new object();
        private PiGpioAlertDelegate Callback = null;
        private int m_GlitchFilterSteadyMicros = 0;
        private int m_NoiseFilterSteadyMicros = 0;
        private int m_NoiseFilterActiveMicros = 0;
        private int m_TimeoutMilliseconds = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPinAlertService"/> class.
        /// </summary>
        /// <param name="pin">The pin.</param>
        internal GpioPinAlertService(GpioPin pin)
            : base(pin)
        {
            // placeholder
        }

        /// <summary>
        /// Gets or sets the watchdog timeout milliseconds.
        /// When no changes are detected for this amount of time,
        /// the callback returns with a no level change.
        /// </summary>
        /// <value>
        /// The timeout in milliseconds.
        /// </value>
        public int TimeoutMilliseconds
        {
            get
            {
                lock (SyncLock) return m_TimeoutMilliseconds;
            }
            set
            {
                ValidateAvailable();

                lock (SyncLock)
                {
                    BoardException.ValidateResult(
                        IO.GpioSetWatchdog((UserGpio)Pin.PinNumber, Convert.ToUInt32(value)));
                    m_TimeoutMilliseconds = value;
                }
            }
        }

        /// <summary>
        /// Gets the glitch filter steady microseconds.
        /// Use <see cref="ApplyGlitchFilter"/> to set.
        /// </summary>
        public int GlitchFilterSteadyMicros { get { lock (SyncLock) return m_GlitchFilterSteadyMicros; } }

        /// <summary>
        /// Gets the noise filter steady microseconds.
        /// Use <see cref="ApplyNoiseFilter"/> to set.
        /// </summary>
        public int NoiseFilterSteadyMicros { get { lock (SyncLock) return m_NoiseFilterSteadyMicros; } }

        /// <summary>
        /// Gets the noise filter active microseconds.
        /// Use <see cref="ApplyNoiseFilter"/> to set.
        /// </summary>
        public int NoiseFilterActiveMicros { get { lock (SyncLock) return m_NoiseFilterActiveMicros; } }

        /// <summary>
        /// Applies a glitch filter to alert triggering.
        /// Prevents reporting signals that are not steady for at least the given number of microseconds.
        /// </summary>
        /// <param name="steadyMicroseconds">The steady microseconds.</param>
        public void ApplyGlitchFilter(int steadyMicroseconds)
        {
            ValidateAvailable();

            lock (SyncLock)
            {
                BoardException.ValidateResult(
                    IO.GpioGlitchFilter((UserGpio)Pin.PinNumber, Convert.ToUInt32(steadyMicroseconds)));

                m_GlitchFilterSteadyMicros = steadyMicroseconds;
            }
        }

        /// <summary>
        /// Resets the glitch filter.
        /// </summary>
        public void ResetGlitchFilter() => ApplyGlitchFilter(0);

        /// <summary>
        /// Applies a noise filter to alert triggering.
        /// Level changes on the GPIO are ignored until a level which has
        /// been stable for <paramref name="steadyMicroseconds"/> microseconds is detected.  Level changes
        /// on the GPIO are then reported for <paramref name="activeMicroseconds"/> microseconds after
        /// which the process repeats.
        /// </summary>
        /// <param name="steadyMicroseconds">The steady microseconds.</param>
        /// <param name="activeMicroseconds">The active microseconds.</param>
        public void ApplyNoiseFilter(int steadyMicroseconds, int activeMicroseconds)
        {
            ValidateAvailable();

            lock (SyncLock)
            {
                BoardException.ValidateResult(
                    IO.GpioNoiseFilter((UserGpio)Pin.PinNumber, Convert.ToUInt32(steadyMicroseconds), Convert.ToUInt32(activeMicroseconds)));

                m_NoiseFilterSteadyMicros = steadyMicroseconds;
                m_NoiseFilterActiveMicros = activeMicroseconds;
            }
        }

        /// <summary>
        /// Resets the noise filter.
        /// </summary>
        public void ResetNoiseFilter() => ApplyNoiseFilter(0, 0);

        /// <summary>
        /// Start the alert callbacks.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <exception cref="NotSupportedException">IsUserGpio is false.</exception>
        /// <exception cref="ArgumentNullException">callback - ClearAlertCallback.</exception>
        /// <exception cref="ArgumentException">A callback is already registered. Clear the current callback before registering a new one. - callback.</exception>
        public void Start(PiGpioAlertDelegate callback)
        {
            ValidateAvailable();

            if (callback == null)
                throw new ArgumentNullException(nameof(callback), $"The callback cannot be null. Use the '{nameof(Stop)}' method instead.");

            lock (SyncLock)
            {
                if (Callback != null)
                    throw new ArgumentException("A callback is already registered. Clear the current callback before registering a new one.", nameof(callback));

                BoardException.ValidateResult(
                    IO.GpioSetAlertFunc((UserGpio)Pin.PinNumber, callback));
                Callback = callback;
            }
        }

        /// <summary>
        /// Clears the alert callback and stops reporting changes.
        /// </summary>
        /// <exception cref="NotSupportedException">IsUserGpio is false.</exception>
        public void Stop()
        {
            ValidateAvailable();

            lock (SyncLock)
            {
                // TODO: For some reason passing NULL to cancel the alert makes everything hang!
                // This looks like a bug in the pigpio library.
                BoardException.ValidateResult(
                    IO.GpioSetAlertFunc((UserGpio)Pin.PinNumber, (g, l, t) => { }));
                Callback = null;
            }
        }

        /// <summary>
        /// Resolves the availability of this service for the associated pin.
        /// </summary>
        /// <returns>
        /// True when the service is deemed as available.
        /// </returns>
        protected override bool ResolveAvailable()
        {
            return Pin.IsUserGpio;
        }
    }
}
