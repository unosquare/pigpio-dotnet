namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// The PWM mode.
    /// </summary>
    public enum PwmMode
    {
        /// <summary>
        /// PWM pulses are sent using mark-sign patterns (old school)
        /// </summary>
        MarkSign = 0,

        /// <summary>
        /// PWM pulses are sent as a balanced signal (default, newer mode)
        /// </summary>
        Balanced = 1,
    }
}