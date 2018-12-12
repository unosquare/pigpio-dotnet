namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Enumerates the different change states for edge detection.
    /// </summary>
    public enum LevelChange
    {
        /// <summary>
        /// Change to low (a falling edge)
        /// </summary>
        HighToLow = 0,

        /// <summary>
        /// Change to high (a rising edge)
        /// </summary>
        LowToHigh = 1,

        /// <summary>
        /// No level change (a watchdog timeout)
        /// </summary>
        NoChange = 2,
    }
}
