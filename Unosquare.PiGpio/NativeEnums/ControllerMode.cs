namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Defines GPIO controller initialization modes.
    /// </summary>
    public enum ControllerMode
    {
        /// <summary>
        /// The not initialized
        /// </summary>
        NotInitialized,

        /// <summary>
        /// Connected directly
        /// </summary>
        InProcess,

        /// <summary>
        /// Connected via pipe 
        /// </summary>
        Pipe,

        /// <summary>
        /// Connected via socket
        /// </summary>
        Socket,
    }
}