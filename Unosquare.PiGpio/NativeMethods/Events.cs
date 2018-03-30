namespace Unosquare.PiGpio.NativeMethods
{
    using NativeEnums;
    using NativeTypes;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Provides event notification warapper APIs for the pigpio library.
    /// </summary>
    public static class Events
    {
        /// <summary>
        /// This function requests a free notification handle.
        ///
        /// A notification is a method for being notified of GPIO state changes
        /// via a pipe or socket.
        ///
        /// Pipe notifications for handle x will be available at the pipe
        /// named /dev/pigpiox (where x is the handle number).  E.g. if the
        /// function returns 15 then the notifications must be read
        /// from /dev/pigpio15.
        ///
        /// Socket notifications are returned to the socket which requested the
        /// handle.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// h = gpioNotifyOpen();
        ///
        /// if (h &gt;= 0)
        /// {
        ///    sprintf(str, "/dev/pigpio%d", h);
        ///
        ///    fd = open(str, O_RDONLY);
        ///
        ///    if (fd &gt;= 0)
        ///    {
        ///       // Okay.
        ///    }
        ///    else
        ///    {
        ///       // Error.
        ///    }
        /// }
        /// else
        /// {
        ///    // Error.
        /// }
        /// </code>
        /// </example>
        /// <returns>Returns a handle greater than or equal to zero if OK, otherwise PI_NO_HANDLE.</returns>
        public static UIntPtr GpioNotifyOpen()
        {
            var result = PiGpioException.ValidateResult(GpioNotifyOpenUnmanaged());
            return new UIntPtr((uint)result);
        }

        /// <summary>
        /// This function requests a free notification handle.
        ///
        /// It differs from <see cref="GpioNotifyOpen"/> in that the pipe size may be
        /// specified, whereas <see cref="GpioNotifyOpen"/> uses the default pipe size.
        ///
        /// See <see cref="GpioNotifyOpen"/> for further details.
        /// </summary>
        /// <returns>The result code. 0 for success. See the <see cref="ResultCode"/> enumeration.</returns>
        public static UIntPtr GpioNotifyOpenWithSize(int bufferSize)
        {
            var result = PiGpioException.ValidateResult(GpioNotifyOpenWithSizeUnmanaged(bufferSize));
            return new UIntPtr((uint)result);
        }

        /// <summary>
        /// This function starts notifications on a previously opened handle.
        ///
        /// The notification sends state changes for each GPIO whose corresponding
        /// bit in bits is set.
        ///
        /// Each notification occupies 12 bytes in the fifo and has the
        /// following structure.
        ///
        /// seqno: starts at 0 each time the handle is opened and then increments
        /// by one for each report.
        ///
        /// flags: three flags are defined, PI_NTFY_FLAGS_WDOG,
        /// PI_NTFY_FLAGS_ALIVE, and PI_NTFY_FLAGS_EVENT.
        ///
        /// If bit 5 is set (PI_NTFY_FLAGS_WDOG) then bits 0-4 of the flags
        /// indicate a GPIO which has had a watchdog timeout.
        ///
        /// If bit 6 is set (PI_NTFY_FLAGS_ALIVE) this indicates a keep alive
        /// signal on the pipe/socket and is sent once a minute in the absence
        /// of other notification activity.
        ///
        /// If bit 7 is set (PI_NTFY_FLAGS_EVENT) then bits 0-4 of the flags
        /// indicate an event which has been triggered.
        ///
        /// tick: the number of microseconds since system boot.  It wraps around
        /// after 1h12m.
        ///
        /// level: indicates the level of each GPIO.  If bit 1&lt;&lt;x is set then
        /// GPIO x is high.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// // Start notifications for GPIO 1, 4, 6, 7, 10.
        ///
        /// //                         1
        /// //                         0  76 4  1
        /// // (1234 = 0x04D2 = 0b0000010011010010)
        ///
        /// gpioNotifyBegin(h, 1234);
        /// </code>
        /// </example>
        /// <remarks>
        /// typedef struct
        /// {
        ///    uint16_t seqno;
        ///    uint16_t flags;
        ///    uint tick;
        ///    uint level;
        /// } gpioReport_t;
        /// </remarks>
        /// <param name="handle">&gt;=0, as returned by <see cref="GpioNotifyOpen"/></param>
        /// <param name="bitMask">a bit mask indicating the GPIO of interest</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioNotifyBegin")]
        public static extern ResultCode GpioNotifyBegin(UIntPtr handle, EventId bitMask);

        /// <summary>
        /// This function pauses notifications on a previously opened handle.
        ///
        /// Notifications for the handle are suspended until <see cref="GpioNotifyBegin"/>
        /// is called again.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioNotifyPause(h);
        /// </code>
        /// </example>
        /// <param name="handle">&gt;=0, as returned by <see cref="GpioNotifyOpen"/></param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioNotifyPause")]
        public static extern ResultCode GpioNotifyPause(UIntPtr handle);

        /// <summary>
        /// This function stops notifications on a previously opened handle
        /// and releases the handle for reuse.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// gpioNotifyClose(h);
        /// </code>
        /// </example>
        /// <param name="handle">&gt;=0, as returned by <see cref="GpioNotifyOpen"/></param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioNotifyClose")]
        public static extern ResultCode GpioNotifyClose(UIntPtr handle);

        /// <summary>
        /// This function selects the events to be reported on a previously
        /// opened handle.
        ///
        /// A report is sent each time an event is triggered providing the
        /// corresponding bit in bits is set.
        ///
        /// See <see cref="GpioNotifyBegin"/> for the notification format.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// // Start reporting events 3, 6, and 7.
        ///
        /// //  bit      76543210
        /// // (0xC8 = 0b11001000)
        ///
        /// eventMonitor(h, 0xC8);
        /// </code>
        /// </example>
        /// <param name="handle">&gt;=0, as returned by <see cref="GpioNotifyOpen"/></param>
        /// <param name="bitMask">a bit mask indicating the events of interest</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "eventMonitor")]
        public static extern ResultCode EventMonitor(UIntPtr handle, EventId bitMask);

        /// <summary>
        /// Registers a function to be called (a callback) when the specified
        /// event occurs.
        ///
        /// One function may be registered per event.
        ///
        /// The function is passed the event, and the tick.
        ///
        /// The callback may be cancelled by passing NULL as the function.
        /// </summary>
        /// <param name="eventId">0-31</param>
        /// <param name="callback">the callback function</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_EVENT_ID.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "eventSetFunc")]
        public static extern ResultCode EventSetFunc(uint eventId, PiGpioEventDelegate callback);

        /// <summary>
        /// Registers a function to be called (a callback) when the specified
        /// event occurs.
        ///
        /// One function may be registered per event.
        ///
        /// The function is passed the event, the tick, and the ueserdata pointer.
        ///
        /// The callback may be cancelled by passing NULL as the function.
        ///
        /// Only one of <see cref="EventSetFunc"/> <see cref="EventSetFuncEx"/> can be
        /// registered per event.
        /// </summary>
        /// <param name="eventId">0-31</param>
        /// <param name="callback">the callback function</param>
        /// <param name="userData">pointer to arbitrary user data</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_EVENT_ID.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "eventSetFuncEx")]
        public static extern ResultCode EventSetFuncEx(UserGpio eventId, PiGpioEventExDelegate callback, UIntPtr userData);

        /// <summary>
        /// This function signals the occurrence of an event.
        ///
        /// An event is a signal used to inform one or more consumers
        /// to start an action.  Each consumer which has registered an interest
        /// in the event (e.g. by calling <see cref="EventSetFunc"/>) will be informed by
        /// a callback.
        ///
        /// One event, PI_EVENT_BSC (31) is predefined.  This event is
        /// auto generated on BSC slave activity.
        ///
        /// The meaning of other events is arbitrary.
        ///
        /// Note that other than its id and its tick there is no data associated
        /// with an event.
        /// </summary>
        /// <param name="eventId">0-31, the event</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_EVENT_ID.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "eventTrigger")]
        public static extern ResultCode EventTrigger(UserGpio eventId);

        #region Unmanaged Methods

        /// <summary>
        /// This function requests a free notification handle.
        ///
        /// A notification is a method for being notified of GPIO state changes
        /// via a pipe or socket.
        ///
        /// Pipe notifications for handle x will be available at the pipe
        /// named /dev/pigpiox (where x is the handle number).  E.g. if the
        /// function returns 15 then the notifications must be read
        /// from /dev/pigpio15.
        ///
        /// Socket notifications are returned to the socket which requested the
        /// handle.
        ///
        /// </summary>
        /// <example>
        /// <code>
        /// h = gpioNotifyOpen();
        ///
        /// if (h &gt;= 0)
        /// {
        ///    sprintf(str, "/dev/pigpio%d", h);
        ///
        ///    fd = open(str, O_RDONLY);
        ///
        ///    if (fd &gt;= 0)
        ///    {
        ///       // Okay.
        ///    }
        ///    else
        ///    {
        ///       // Error.
        ///    }
        /// }
        /// else
        /// {
        ///    // Error.
        /// }
        /// </code>
        /// </example>
        /// <returns>Returns a handle greater than or equal to zero if OK, otherwise PI_NO_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioNotifyOpen")]
        private static extern int GpioNotifyOpenUnmanaged();

        /// <summary>
        /// This function requests a free notification handle.
        /// It differs from <see cref="GpioNotifyOpen" /> in that the pipe size may be
        /// specified, whereas <see cref="GpioNotifyOpen" /> uses the default pipe size.
        /// See <see cref="GpioNotifyOpen" /> for further details.
        /// </summary>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns>
        /// The result code. 0 for success. See the <see cref="ResultCode" /> enumeration.
        /// </returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "gpioNotifyOpenWithSize")]
        private static extern int GpioNotifyOpenWithSizeUnmanaged(int bufferSize);

        #endregion
    }
}
