namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Provides access to the registiered library timers.
    /// </summary>
    public sealed class GpioTimerCollection
            : ReadOnlyDictionary<TimerId, GpioTimer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpioTimerCollection"/> class.
        /// </summary>
        internal GpioTimerCollection()
            : base(CreateInternalCollection())
        {
            // placeholder
        }

        /// <summary>
        /// Creates the internal collection.
        /// </summary>
        /// <returns>The items in the collection</returns>
        private static Dictionary<TimerId, GpioTimer> CreateInternalCollection()
        {
            var enumValues = Enum.GetValues(typeof(TimerId));
            var result = new Dictionary<TimerId, GpioTimer>(enumValues.Length);

            foreach (TimerId value in enumValues)
            {
                result[value] = new GpioTimer(value);
            }

            return result;
        }
    }
}
