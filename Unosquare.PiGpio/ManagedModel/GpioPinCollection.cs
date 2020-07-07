namespace Unosquare.PiGpio.ManagedModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using NativeEnums;
    using Swan.DependencyInjection;
    using NativeMethods.Interfaces;

    /// <summary>
    /// Represents a dictionary of all GPIO Pins.
    /// </summary>
    public sealed class GpioPinCollection
        : ReadOnlyDictionary<int, GpioPin>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPinCollection"/> class.
        /// </summary>
        internal GpioPinCollection()
            : base(CreateInternalCollection())
        {
            // placeholder
        }

        /// <summary>
        /// Gets the <see cref="GpioPin"/> with the specified gpio.
        /// </summary>
        /// <value>
        /// The <see cref="GpioPin"/>.
        /// </value>
        /// <param name="gpio">The gpio.</param>
        /// <returns>The pin object.</returns>
        public GpioPin this[SystemGpio gpio] => this[(int)gpio];

        /// <summary>
        /// Gets the <see cref="GpioPin"/> with the specified gpio.
        /// </summary>
        /// <value>
        /// The <see cref="GpioPin"/>.
        /// </value>
        /// <param name="gpio">The gpio.</param>
        /// <returns>The pin object.</returns>
        public GpioPin this[UserGpio gpio] => this[(int)gpio];

        /// <summary>
        /// Creates the internal collection.
        /// </summary>
        /// <returns>The items in the collection.</returns>
        private static Dictionary<int, GpioPin> CreateInternalCollection()
        {
            var ioService = DependencyContainer.Current.Resolve<IIOService>();
            var enumValues = Enum.GetValues(typeof(SystemGpio));
            var result = new Dictionary<int, GpioPin>(enumValues.Length);

            foreach (SystemGpio value in enumValues)
            {
                result[(int) value] = new GpioPin(ioService, value);
            }

            return result;
        }
    }
}
