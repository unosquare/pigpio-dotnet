namespace Unosquare.PiGpio.Enums
{
    /// <summary>
    /// Input GPIOS have pull-up, pull-down or no resistors.
    /// This enumeration defines the different resistor pull modes.
    /// </summary>
    public enum GpioPullMode
    {
        /// <summary>
        /// No pull-up or pull-down mode.
        /// </summary>
        Off = 0,

        /// <summary>
        /// Pull-down resistor mode configuration
        /// </summary>
        Down = 1,

        /// <summary>
        /// Pull-up resistor mode configuration
        /// </summary>
        Up = 2,
    }
}
