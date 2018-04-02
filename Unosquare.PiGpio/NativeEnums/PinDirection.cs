namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Enumerates the IO directions.
    /// This enumeration is compatible with the <see cref="PinMode"/> enum.
    /// </summary>
    public enum PinDirection
    {
        /// <summary>
        /// The pin is operating in an alternative mode.
        /// </summary>
        Alternative = -1,

        /// <summary>
        /// The input operating mode
        /// </summary>
        Input = 0,

        /// <summary>
        /// The output operating mode
        /// </summary>
        Output = 1,
    }
}
