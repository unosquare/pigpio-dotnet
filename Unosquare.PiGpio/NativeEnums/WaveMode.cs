namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Enumerates the different wave modes
    /// </summary>
    public enum WaveMode
    {
        /// <summary>
        /// The one shot
        /// </summary>
        OneShot = 0,

        /// <summary>
        /// The repeat
        /// </summary>
        Repeat = 1,

        /// <summary>
        /// The one shot synchronize
        /// </summary>
        OneShotSync = 2,

        /// <summary>
        /// The repeat synchronize
        /// </summary>
        RepeatSync = 3,
    }
}
