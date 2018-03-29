namespace Unosquare.PiGpio.NativeMethods
{
    using System.Runtime.InteropServices;

    public static class Custom
    {
        /// <summary>
        /// This function is available for user customisation.
        ///
        /// It returns a single integer value.
        ///
        /// </summary>
        /// <param name="arg1">&gt;=0</param>
        /// <param name="arg2">&gt;=0</param>
        /// <param name="argx">extra (byte) arguments</param>
        /// <param name="argc">number of extra arguments</param>
        /// <returns>Returns &gt;= 0 if OK, less than 0 indicates a user defined error.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCustom1")]
        public static extern int GpioCustom1(uint arg1, uint arg2, [In, MarshalAs(UnmanagedType.LPArray)] byte[] argx, uint argc);

        /// <summary>
        /// This function is available for user customisation.
        ///
        /// It differs from gpioCustom1 in that it returns an array of bytes
        /// rather than just an integer.
        ///
        /// The returned value is an integer indicating the number of returned bytes.
        ///
        /// The number of returned bytes must be retMax or less.
        /// </summary>
        /// <param name="arg1">&gt;=0</param>
        /// <param name="argx">extra (byte) arguments</param>
        /// <param name="argc">number of extra arguments</param>
        /// <param name="retBuf">buffer for returned bytes</param>
        /// <param name="retMax">maximum number of bytes to return</param>
        /// <returns>Returns &gt;= 0 if OK, less than 0 indicates a user defined error.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioCustom2")]
        public static extern int GpioCustom2(uint arg1, [In, MarshalAs(UnmanagedType.LPArray)] byte[] argx, uint argc, [In, MarshalAs(UnmanagedType.LPArray)] byte[] retBuf, uint retMax);
    }
}
