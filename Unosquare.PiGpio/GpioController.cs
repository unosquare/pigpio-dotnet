namespace Unosquare.PiGpio
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using RaspberryIO.Abstractions;

    /// <summary>
    /// Represents the Raspberry Pi GPIO controller
    /// as an IReadOnlyCollection of GpioPins
    /// Low level operations are accomplished by using the PiGPIO library.
    /// </summary>
    public class GpioController : IGpioController
    {
        /// <inheritdoc />
        public IEnumerator<IGpioPin> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public int Count { get; }

        /// <inheritdoc />
        public IGpioPin this[int bcmPinNumber] => throw new NotImplementedException();

        /// <inheritdoc />
        public IGpioPin this[BcmPin bcmPin] => throw new NotImplementedException();

        /// <inheritdoc />
        public IGpioPin this[P1 pinNumber] => throw new NotImplementedException();

        /// <inheritdoc />
        public IGpioPin this[P5 pinNumber] => throw new NotImplementedException();
    }
}
