namespace Unosquare.PiGpio.Enums
{
    /// <summary>
    /// Pins can operate in different modes.
    /// This enumeration defines the fdifferent operation modes from 0 to 7.
    /// </summary>
    public enum PortMode : int
    {
        /// <summary>
        /// The input operating mode
        /// </summary>
        Input = 0,

        /// <summary>
        /// The output operating mode
        /// </summary>
        Output = 1,

        /// <summary>
        /// The alt5 operating mode
        /// </summary>
        Alt5 = 2,

        /// <summary>
        /// The alt4 operating mode
        /// </summary>
        Alt4 = 3,

        /// <summary>
        /// The alt0 operating mode
        /// </summary>
        Alt0 = 4,

        /// <summary>
        /// The alt1 operating mode
        /// </summary>
        Alt1 = 5,

        /// <summary>
        /// The alt2 operating mode
        /// </summary>
        Alt2 = 6,

        /// <summary>
        /// The alt3 operating mode
        /// </summary>
        Alt3 = 7,
    }
}
