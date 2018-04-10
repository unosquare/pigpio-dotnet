namespace Unosquare.PiGpio.ManagedModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NativeEnums;
    using NativeTypes;
    using NativeMethods;

    /// <summary>
    /// Provides a a pin service to generate pulses with microsecond precision
    /// </summary>
    /// <seealso cref="GpioPinServiceBase" />
    public sealed class GpioPinWaveService
        : GpioPinServiceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPinWaveService"/> class.
        /// </summary>
        /// <param name="pin">The pin.</param>
        internal GpioPinWaveService(GpioPin pin)
            : base(pin)
        {
            // placeholder
            // TODO: implementation is incomplete
            // see https://github.com/bschwind/ir-slinger/blob/master/irslinger.h
            // for usage
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
