namespace Unosquare.PiGpio.ManagedModel
{
    using System;

    /// <summary>
    /// Provides a base implementation of a GPIO Pin Service.
    /// </summary>
    public abstract class GpioPinServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPinServiceBase"/> class.
        /// </summary>
        /// <param name="pin">The pin.</param>
        protected GpioPinServiceBase(GpioPin pin)
        {
            Pin = pin;
            IsAvailable = ResolveAvailable();
        }

        /// <summary>
        /// Gets a value indicating whether this service is available on the associated pin.
        /// </summary>
        public bool IsAvailable { get; }

        /// <summary>
        /// Gets the associated pin.
        /// </summary>
        protected GpioPin Pin { get; }

        /// <summary>
        /// Resolves the availability of this service for the associated pin.
        /// </summary>
        /// <returns>True when the service is deemed as available.</returns>
        protected abstract bool ResolveAvailable();

        /// <summary>
        /// Validates that this service is available. Otherwise, a <see cref="NotSupportedException"/> is thrown.
        /// </summary>
        /// <exception cref="NotSupportedException">Only pins marked as IsUserGpio support this service.</exception>
        protected void ValidateAvailable()
        {
            if (IsAvailable == false)
                throw new NotSupportedException($"Only pins that are marked as '{nameof(Pin.IsUserGpio)}' can use the {nameof(GpioPinInterruptService)}.");
        }
    }
}
