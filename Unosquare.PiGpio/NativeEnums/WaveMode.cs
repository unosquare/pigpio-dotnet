namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Enumerates the different wave modes
    /// </summary>
    public enum WaveMode
    {
        /// <summary>
        /// The one shot wave mode
        /// </summary>
        OneShot = 0,

        /// <summary>
        /// The repeat wave mode
        /// </summary>
        Repeat = 1,

        /// <summary>
        /// The one shot synchronize wave mode
        /// </summary>
        OneShotSync = 2,

        /// <summary>
        /// The repeat synchronize wave mode
        /// </summary>
        RepeatSync = 3,
    }
}
