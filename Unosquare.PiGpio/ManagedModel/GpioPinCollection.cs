namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Represents a dictionary of all GPIO Pins
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
        /// <returns>The pin object</returns>
        public GpioPin this[SystemGpio gpio]
        {
            get => this[(int)gpio];
        }

        /// <summary>
        /// Gets the <see cref="GpioPin"/> with the specified gpio.
        /// </summary>
        /// <value>
        /// The <see cref="GpioPin"/>.
        /// </value>
        /// <param name="gpio">The gpio.</param>
        /// <returns>The pin object</returns>
        public GpioPin this[UserGpio gpio]
        {
            get => this[(int)gpio];
        }

        /// <summary>
        /// Creates the internal collection.
        /// </summary>
        /// <returns>The items in the collection</returns>
        private static Dictionary<int, GpioPin> CreateInternalCollection()
        {
            var enumValues = Enum.GetValues(typeof(SystemGpio));
            var result = new Dictionary<int, GpioPin>(enumValues.Length);

            foreach (SystemGpio value in enumValues)
            {
                result[(int)value] = new GpioPin(value);
            }

            return result;
        }
    }
}
