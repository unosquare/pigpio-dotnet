namespace Unosquare.PiGpio.NativeMethods
{
    using NativeEnums;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Provides access to scripting methods of the pigpio library. Scripts are used by the
    /// pigpio daemon to execute a set of instructions to speedup GPIO instructions.
    /// Unused in the managed model of this library but provided for reference purposes.
    /// </summary>
    public static class Scripts
    {
        /// <summary>
        /// This function stores a null terminated script for later execution.
        ///
        /// See [[http://abyz.me.uk/rpi/pigpio/pigs.html#Scripts]] for details.
        ///
        /// The function returns a script id if the script is valid,
        /// otherwise PI_BAD_SCRIPT.
        /// </summary>
        /// <param name="script">the text of the script.</param>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioStoreScript")]
        public static extern int GpioStoreScript(string script);

        /// <summary>
        /// This function runs a stored script.
        ///
        /// The function returns 0 if OK, otherwise PI_BAD_SCRIPT_ID, or
        /// PI_TOO_MANY_PARAM.
        ///
        /// param is an array of up to 10 parameters which may be referenced in
        /// the script as p0 to p9.
        /// </summary>
        /// <param name="script_id">&gt;=0, as returned by <see cref="GpioStoreScript"/>.</param>
        /// <param name="numPar">0-10, the number of parameters.</param>
        /// <param name="param">an array of parameters.</param>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioRunScript")]
        public static extern int GpioRunScript(uint script_id, uint numPar, [In, MarshalAs(UnmanagedType.LPArray)] uint[] param);

        /// <summary>
        /// This function sets the parameters of a script.  The script may or
        /// may not be running.  The first numPar parameters of the script are
        /// overwritten with the new values.
        ///
        /// The function returns 0 if OK, otherwise PI_BAD_SCRIPT_ID, or
        /// PI_TOO_MANY_PARAM.
        ///
        /// param is an array of up to 10 parameters which may be referenced in
        /// the script as p0 to p9.
        /// </summary>
        /// <param name="script_id">&gt;=0, as returned by <see cref="GpioStoreScript"/>.</param>
        /// <param name="numPar">0-10, the number of parameters.</param>
        /// <param name="param">an array of parameters.</param>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioUpdateScript")]
        public static extern int GpioUpdateScript(uint script_id, uint numPar, [In, MarshalAs(UnmanagedType.LPArray)] uint[] param);

        /// <summary>
        /// This function returns the run status of a stored script as well as
        /// the current values of parameters 0 to 9.
        ///
        /// The function returns greater than or equal to 0 if OK,
        /// otherwise PI_BAD_SCRIPT_ID.
        ///
        /// The run status may be
        ///
        /// The current value of script parameters 0 to 9 are returned in param.
        /// </summary>
        /// <remarks>
        /// PI_SCRIPT_INITING
        /// PI_SCRIPT_HALTED
        /// PI_SCRIPT_RUNNING
        /// PI_SCRIPT_WAITING
        /// PI_SCRIPT_FAILED.
        /// </remarks>
        /// <param name="script_id">&gt;=0, as returned by <see cref="GpioStoreScript"/>.</param>
        /// <param name="param">an array to hold the returned 10 parameters.</param>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioScriptStatus")]
        public static extern int GpioScriptStatus(uint script_id, [In, MarshalAs(UnmanagedType.LPArray)] uint[] param);

        /// <summary>
        /// This function stops a running script.
        ///
        /// The function returns 0 if OK, otherwise PI_BAD_SCRIPT_ID.
        /// </summary>
        /// <param name="script_id">&gt;=0, as returned by <see cref="GpioStoreScript"/>.</param>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioStopScript")]
        public static extern int GpioStopScript(uint script_id);

        /// <summary>
        /// This function deletes a stored script.
        ///
        /// The function returns 0 if OK, otherwise PI_BAD_SCRIPT_ID.
        /// </summary>
        /// <param name="script_id">&gt;=0, as returned by <see cref="GpioStoreScript"/>.</param>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "gpioDeleteScript")]
        public static extern int GpioDeleteScript(uint script_id);

        /// <summary>
        /// Used to print a readable version of a script to stderr.
        ///
        /// Not intended for general use.
        /// </summary>
        /// <param name="scriptId">&gt;=0, a script_id returned by <see cref="GpioStoreScript"/>.</param>
        [DllImport(Constants.PiGpioLibrary, CallingConvention = CallingConvention.Cdecl, EntryPoint = "rawDumpScript")]
        public static extern void RawDumpScript(uint scriptId);
    }
}
