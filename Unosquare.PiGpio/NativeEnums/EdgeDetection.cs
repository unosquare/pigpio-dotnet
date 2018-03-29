namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Defines the edge detection strategies
    /// </summary>
    public enum EdgeDetection : int
    {
        /// <summary>
        /// The falling edge (from high to low voltage)
        /// </summary>
        FallingEdge = 0,

        /// <summary>
        /// The rising edge (from low to high voltage)
        /// </summary>
        RisingEdge = 1,

        /// <summary>
        /// Rising and falling edge detection strategy
        /// </summary>
        EitherEdge = 2,
    }
}
