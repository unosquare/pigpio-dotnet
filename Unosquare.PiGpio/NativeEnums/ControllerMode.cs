namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Defines GPIO controller initialization modes.
    /// </summary>
    internal enum ControllerMode
    {
        /// <summary>
        /// The not initialized
        /// </summary>
        NotInitialized,

        /// <summary>
        /// Connected directly
        /// </summary>
        Direct,

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