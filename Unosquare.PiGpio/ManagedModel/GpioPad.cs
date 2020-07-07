namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using Swan.DependencyInjection;
    using NativeMethods.Interfaces;

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
            IOService = DependencyContainer.Current.Resolve<IIOService>();
            PadId = padId;
        }

        private IIOService IOService { get; }

        /// <summary>
        /// Gets the electrical pad identifier.
        /// </summary>
        public GpioPadId PadId { get; }

        /// <summary>
        /// Gets electrical pad object.
        /// </summary>
        public GpioPad Pad => Board.GpioPads[PadId];

        /// <summary>
        /// Gets or sets the electrical pad strength.
        /// </summary>
        public GpioPadStrength PadStrength
        {
            get => IOService.GpioGetPad(PadId);
            set => BoardException.ValidateResult(IOService.GpioSetPad(PadId, value));
        }
    }
}
