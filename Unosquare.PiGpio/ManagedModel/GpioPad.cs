namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;

    /// <summary>
    /// Represents an electrical pad which groups
    /// GPIO pins and has configurable electrical drive strength.
    /// </summary>
    public sealed class GpioPad
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GpioPad"/> class.
        /// </summary>
        /// <param name="padId">The pad identifier.</param>
        internal GpioPad(GpioPadId padId)
        {
            PadId = padId;
        }

        /// <summary>
        /// Gets the electrical pad identifier.
        /// </summary>
        public GpioPadId PadId { get; }

        /// <summary>
        /// Gets or sets the electrical pad strength.
        /// </summary>
        public GpioPadStrength PadStrength
        {
            get => IO.GpioGetPad(PadId);
            set => PiGpioException.ValidateResult(IO.GpioSetPad(PadId, value));
        }
    }
}
