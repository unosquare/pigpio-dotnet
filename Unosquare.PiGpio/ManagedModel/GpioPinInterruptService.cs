namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using NativeTypes;
    using System;

    /// <summary>
    /// Provides Interrupt Service Routine callback services on the GPIO pin.
    /// </summary>
    /// <seealso cref="GpioPinServiceBase" />
    public sealed class GpioPinInterruptService : GpioPinServiceBase
    {
        private readonly object SyncLock = new object();
        private PiGpioIsrDelegate Callback = null;
        private EdgeDetection m_EdgeDetection = EdgeDetection.EitherEdge;
        private int m_TimeoutMilliseconds = 0;

        internal GpioPinInterruptService(GpioPin pin)
            : base(pin)
        {
            // placeholder;
        }

        /// <summary>
        /// Gets or sets the edge detection strategy.
        /// </summary>
        /// <exception cref="InvalidOperationException">EdgeDetection cannot be set when callbacks have started.</exception>
        public EdgeDetection EdgeDetection
        {
            get
            {
                lock (SyncLock) return m_EdgeDetection;
            }
            set
            {
                ValidateAvailable();

                lock (SyncLock)
                {
                    if (Callback != null)
                        throw new InvalidOperationException($"Unable to change {nameof(EdgeDetection)} when callback is set.");
                    m_EdgeDetection = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the ISR timeout milliseconds.
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
                    if (Callback != null)
                        throw new InvalidOperationException($"Unable to change {nameof(TimeoutMilliseconds)} when callback is set.");

                    m_TimeoutMilliseconds = value;
                }
            }
        }

        /// <summary>
        /// Starts the hardware ISR callbacks.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="edgeDetection">The edge detection.</param>
        /// <param name="timeoutMilliseconds">The timeout milliseconds.</param>
        /// <exception cref="ArgumentNullException">callback - Use Stop first.</exception>
        /// <exception cref="ArgumentException">A callback is already registered. Clear the current callback before registering a new one. - callback</exception>
        public void Start(PiGpioIsrDelegate callback, EdgeDetection edgeDetection, int timeoutMilliseconds)
        {
            ValidateAvailable();

            if (callback == null)
                throw new ArgumentNullException(nameof(callback), $"The callback cannot be null. Use the '{nameof(Stop)}' method instead.");

            lock (SyncLock)
            {
                if (Callback != null)
                    throw new ArgumentException("A callback is already registered. Clear the current callback before registering a new one.", nameof(callback));

                BoardException.ValidateResult(
                    IO.GpioSetIsrFunc(Pin.PinGpio, edgeDetection, timeoutMilliseconds, callback));

                m_EdgeDetection = edgeDetection;
                m_TimeoutMilliseconds = timeoutMilliseconds;
                Callback = callback;
            }
        }

        /// <summary>
        /// Starts the hardware ISR callbacks.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <exception cref="NotSupportedException">IsUserGpio is false</exception>
        /// <exception cref="ArgumentNullException">callback - ClearAlertCallback</exception>
        /// <exception cref="ArgumentException">A callback is already registered. Clear the current callback before registering a new one. - callback</exception>
        public void Start(PiGpioIsrDelegate callback)
        {
            Start(callback, EdgeDetection, TimeoutMilliseconds);
        }

        /// <summary>
        /// Stops the hardware ISR callbacks.
        /// </summary>
        public void Stop()
        {
            ValidateAvailable();

            lock (SyncLock)
            {
                BoardException.ValidateResult(
                    IO.GpioSetIsrFunc(Pin.PinGpio, EdgeDetection, 0, null));
                Callback = null;
            }
        }

        /// <summary>
        /// Resolves the availability of this service for the associated pin.
        /// </summary>
        /// <returns>
        /// True when the service is deemed as available.
        /// </returns>
        protected override bool ResolveAvailable() => true;
    }
}
