namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// A collection of GPIO electrical pads.
    /// </summary>
    public sealed class GpioPadCollection
        : ReadOnlyDictionary<GpioPadId, GpioPad>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPadCollection"/> class.
        /// </summary>
        internal GpioPadCollection()
            : base(CreateInternalCollection())
        {
            // placeholder
        }

        /// <summary>
        /// Creates the internal collection.
        /// </summary>
        /// <returns>The items in the collection.</returns>
        private static Dictionary<GpioPadId, GpioPad> CreateInternalCollection()
        {
            var enumValues = Enum.GetValues(typeof(GpioPadId));
            var result = new Dictionary<GpioPadId, GpioPad>(enumValues.Length);

            foreach (GpioPadId value in enumValues)
            {
                result[value] = new GpioPad(value);
            }

            return result;
        }
    }
}
