namespace Unosquare.PiGpio
{
    using Enums;
    using System.Runtime.InteropServices;

    public static partial class NativeMethods
    {
        /// <summary>
        /// This function returns a handle to a file opened in a specified mode.
        ///
        /// File
        ///
        /// A file may only be opened if permission is granted by an entry in
        /// /opt/pigpio/access.  This is intended to allow remote access to files
        /// in a more or less controlled manner.
        ///
        /// Each entry in /opt/pigpio/access takes the form of a file path
        /// which may contain wildcards followed by a single letter permission.
        /// The permission may be R for read, W for write, U for read/write,
        /// and N for no access.
        ///
        /// Where more than one entry matches a file the most specific rule
        /// applies.  If no entry matches a file then access is denied.
        ///
        /// Suppose /opt/pigpio/access contains the following entries
        ///
        /// Files may be written in directory dir_1 with the exception
        /// of file.txt.
        ///
        /// Files may be read in directory dir_2.
        ///
        /// Files may be read and written in directory dir_3.
        ///
        /// If a directory allows read, write, or read/write access then files may
        /// be created in that directory.
        ///
        /// In an attempt to prevent risky permissions the following paths are
        /// ignored in /opt/pigpio/access.
        ///
        /// Mode
        ///
        /// The mode may have the following values.
        ///
        /// Macro         @ Value @ Meaning
        /// PI_FILE_READ  @   1   @ open file for reading
        /// PI_FILE_WRITE @   2   @ open file for writing
        /// PI_FILE_RW    @   3   @ open file for reading and writing
        ///
        /// The following values may be or'd into the mode.
        ///
        /// Macro          @ Value @ Meaning
        /// PI_FILE_APPEND @ 4     @ Writes append data to the end of the file
        /// PI_FILE_CREATE @ 8     @ The file is created if it doesn't exist
        /// PI_FILE_TRUNC  @ 16    @ The file is truncated
        ///
        /// Newly created files are owned by root with permissions owner read and write.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// #include &lt;stdio.h&gt;
        /// #include &lt;pigpio.h&gt;
        ///
        /// int main(int argc, char *argv[])
        /// {
        ///    int handle, c;
        ///    char buf[60000];
        ///
        ///    if (gpioInitialise() &lt; 0) return 1;
        ///
        ///    // assumes /opt/pigpio/access contains the following line
        ///    // /ram/*.c r
        ///
        ///    handle = fileOpen("/ram/pigpio.c", PI_FILE_READ);
        ///
        ///    if (handle &gt;= 0)
        ///    {
        ///       while ((c=fileRead(handle, buf, sizeof(buf)-1)))
        ///       {
        ///          buf[c] = 0;
        ///          printf("%s", buf);
        ///       }
        ///
        ///       fileClose(handle);
        ///    }
        ///
        ///    gpioTerminate();
        /// }
        /// </code>
        /// </example>
        /// <remarks>
        /// /home/* n
        /// /home/pi/shared/dir_1/* w
        /// /home/pi/shared/dir_2/* r
        /// /home/pi/shared/dir_3/* u
        /// /home/pi/shared/dir_1/file.txt n
        /// a path containing ..
        /// a path containing only wildcards (*?)
        /// a path containing less than two non-wildcard parts
        /// </remarks>
        /// <param name="file">the file to open</param>
        /// <param name="mode">the file open mode</param>
        /// <returns>Returns a handle (&gt;=0) if OK, otherwise PI_NO_HANDLE, PI_NO_FILE_ACCESS, PI_BAD_FILE_MODE, PI_FILE_OPEN_FAILED, or PI_FILE_IS_A_DIR.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "fileOpen")]
        public static extern int FileOpen(string file, uint mode);

        /// <summary>
        /// This function closes the file associated with handle.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// fileClose(h);
        /// </code>
        /// </example>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="fileOpen"/></param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "fileClose")]
        public static extern ResultCode FileClose(uint handle);

        /// <summary>
        /// This function writes count bytes from buf to the the file
        /// associated with handle.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// status = fileWrite(h, buf, count);
        /// if (status == 0)
        /// {
        ///    // okay
        /// }
        /// else
        /// {
        ///    // error
        /// }
        /// </code>
        /// </example>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="fileOpen"/></param>
        /// <param name="buffer">the array of bytes to write</param>
        /// <param name="count">the number of bytes to write</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, PI_FILE_NOT_WOPEN, or PI_BAD_FILE_WRITE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "fileWrite")]
        public static extern ResultCode FileWrite(uint handle, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

        /// <summary>
        /// This function reads up to count bytes from the the file
        /// associated with handle and writes them to buf.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// if (fileRead(h, buf, sizeof(buf)) &gt; 0)
        /// {
        ///    // process read data
        /// }
        /// </code>
        /// </example>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="fileOpen"/></param>
        /// <param name="buf">an array to receive the read data</param>
        /// <param name="count">the maximum number of bytes to read</param>
        /// <returns>Returns the number of bytes read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, PI_FILE_NOT_ROPEN, or PI_BAD_FILE_WRITE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "fileRead")]
        public static extern int FileRead(uint handle, byte[] buf, uint count);

        /// <summary>
        /// This function seeks to a position within the file associated
        /// with handle.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// fileSeek(0, 20, PI_FROM_START); // Seek to start plus 20
        ///
        /// size = fileSeek(0, 0, PI_FROM_END); // Seek to end, return size
        ///
        /// pos = fileSeek(0, 0, PI_FROM_CURRENT); // Return current position
        /// </code>
        /// </example>
        /// <remarks>
        ///             move forward, negative offsets backwards.
        ///             or PI_FROM_END (2)
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by a call to <see cref="fileOpen"/></param>
        /// <param name="seekOffset">the number of bytes to move.  Positive offsets</param>
        /// <param name="seekFrom">one of PI_FROM_START (0), PI_FROM_CURRENT (1),</param>
        /// <returns>Returns the new byte position within the file (&gt;=0) if OK, otherwise PI_BAD_HANDLE, or PI_BAD_FILE_SEEK.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "fileSeek")]
        public static extern int FileSeek(uint handle, int seekOffset, int seekFrom);

        /// <summary>
        /// This function returns a list of files which match a pattern.  The
        /// pattern may contain wildcards.
        ///
        /// The pattern must match an entry in /opt/pigpio/access.  The pattern
        /// may contain wildcards.  See <see cref="fileOpen"/>.
        ///
        /// NOTE
        ///
        /// The returned value is not the number of files, it is the number
        /// of bytes in the buffer.  The file names are separated by newline
        /// characters.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// #include &lt;stdio.h&gt;
        /// #include &lt;pigpio.h&gt;
        ///
        /// int main(int argc, char *argv[])
        /// {
        ///    int c;
        ///    char buf[1000];
        ///
        ///    if (gpioInitialise() &lt; 0) return 1;
        ///
        ///    // assumes /opt/pigpio/access contains the following line
        ///    // /ram/*.c r
        ///
        ///    c = fileList("/ram/p*.c", buf, sizeof(buf));
        ///
        ///    if (c &gt;= 0)
        ///    {
        ///       // terminate string
        ///       buf[c] = 0;
        ///       printf("%s", buf);
        ///    }
        ///
        ///    gpioTerminate();
        /// }
        /// </code>
        /// </example>
        /// <param name="fpat">file pattern to match</param>
        /// <param name="buf">an array to receive the matching file names</param>
        /// <param name="count">the maximum number of bytes to read</param>
        /// <returns>Returns the number of returned bytes if OK, otherwise PI_NO_FILE_ACCESS, or PI_NO_FILE_MATCH.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "fileList")]
        public static extern int FileList(string fpat, byte[] buf, uint count);
    }
}
