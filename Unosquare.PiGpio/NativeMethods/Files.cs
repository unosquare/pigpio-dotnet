namespace Unosquare.PiGpio.NativeMethods
{
    using NativeEnums;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
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
    /// </summary>
    public static class Files
    {
        #region Unmanaged Methods

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
        private static extern int FileOpenUnmanaged(string file, uint mode);

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
        private static extern int FileSeekUnmanaged(UIntPtr handle, int seekOffset, SeekMode seekFrom);

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
        /// <param name="pathPattern">file pattern to match</param>
        /// <param name="buffer">an array to receive the matching file names</param>
        /// <param name="count">the maximum number of bytes to read</param>
        /// <returns>Returns the number of returned bytes if OK, otherwise PI_NO_FILE_ACCESS, or PI_NO_FILE_MATCH.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "fileList")]
        private static extern int FileListUnmanaged(string pathPattern, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

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
        public static extern ResultCode FileWriteUnmanaged(UIntPtr handle, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

        #endregion

        /// <summary>
        /// A wrapper function for <see cref="FileOpenUnmanaged"/>
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="mode">The mode.</param>
        /// <returns>A file handle</returns>
        /// <exception cref="System.IO.IOException">When the file fails to open</exception>
        public static UIntPtr FileOpen(string filePath, FileModeFlags mode)
        {
            var result = PiGpioException.ValidateResult(FileOpenUnmanaged(filePath, (uint)mode));
            return new UIntPtr((uint)result);
        }

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
        public static extern ResultCode FileClose(UIntPtr handle);

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
        /// <param name="buffer">an array to receive the read data</param>
        /// <param name="count">the maximum number of bytes to read</param>
        /// <returns>Returns the number of bytes read (&gt;=0) if OK, otherwise PI_BAD_HANDLE, PI_BAD_PARAM, PI_FILE_NOT_ROPEN, or PI_BAD_FILE_WRITE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "fileRead")]
        public static extern int FileRead(UIntPtr handle, [In, MarshalAs(UnmanagedType.LPArray)] byte[] buffer, uint count);

        /// <summary>
        /// Reads from a file handle up to count bytes.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static byte[] FileRead(UIntPtr handle, int count)
        {
            var buffer = new byte[count];
            var result = PiGpioException.ValidateResult(FileRead(handle, buffer, (uint)count));
            var outputBuffer = new byte[result];
            Buffer.BlockCopy(buffer, 0, outputBuffer, 0, result);
            return outputBuffer;
        }

        /// <summary>
        /// Writes the given buffer to a file handle
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="buffer">The buffer.</param>
        /// <returns></returns>
        public static ResultCode FileWrite(UIntPtr handle, byte[] buffer)
        {
            return PiGpioException.ValidateResult(FileWriteUnmanaged(handle, buffer, (uint)buffer.Length));
        }

        /// <summary>
        /// Writes the given buffer to a file handle
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static ResultCode FileWrite(UIntPtr handle, byte[] buffer, int length)
        {
            return PiGpioException.ValidateResult(FileWriteUnmanaged(handle, buffer, (uint)length));
        }

        /// <summary>
        /// Seeks within a file
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="seekOffset">The seek offset.</param>
        /// <param name="seekFrom">The seek from.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.IOException"></exception>
        public static int FileSeek(UIntPtr handle, int seekOffset, SeekMode seekFrom)
        {
            return PiGpioException.ValidateResult(FileSeekUnmanaged(handle, seekOffset, seekFrom));
        }

        /// <summary>
        /// Retrieves a list of files matching the given pattern. See <see cref="FileListUnmanaged"/>
        /// </summary>
        /// <param name="pathPattern">The path pattern.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.IOException"></exception>
        public static string[] FileList(string pathPattern)
        {
            var buffer = new byte[1024 * 128]; // 128kb of file paths
            var result = PiGpioException.ValidateResult(FileListUnmanaged(pathPattern, buffer, (uint)buffer.Length));
            var encoding = Encoding.GetEncoding(0);
            var fileList = new List<string>();
            var lastZeroIndex = -1;
            var runLength = 0;
            var byteValue = default(byte);

            // Split the bytes into 0-terminated strings
            for (var byteIndex = 0; byteIndex < result; byteIndex++)
            {
                byteValue = buffer[byteIndex];
                if (byteValue == 0 || byteIndex >= result - 1)
                {
                    if (runLength > 0)
                        fileList.Add(encoding.GetString(buffer, lastZeroIndex + 1, runLength));

                    runLength = 0;
                    lastZeroIndex = byteIndex;
                    continue;
                }

                runLength++;
            }

            return fileList.ToArray();
        }
    }
}
