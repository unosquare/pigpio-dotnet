namespace Unosquare.PiGpio
{
    using Enums;
    using System;
    using System.Runtime.InteropServices;

    public static partial class NativeMethods
    {

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
        /// <param name="bits">a bit mask indicating the events of interest</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_HANDLE.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "eventMonitor")]
        public static extern ResultCode EventMonitor(UIntPtr handle, uint bits);

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
        /// <param name="f">the callback function</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_EVENT_ID.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "eventSetFunc")]
        public static extern ResultCode EventSetFunc(uint eventId, EventDelegate f);

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
        /// <param name="f">the callback function</param>
        /// <param name="userData">pointer to arbitrary user data</param>
        /// <returns>Returns 0 if OK, otherwise PI_BAD_EVENT_ID.</returns>
        [DllImport(Constants.PiGpioLibrary, EntryPoint = "eventSetFuncEx")]
        public static extern ResultCode EventSetFuncEx(uint eventId, EventExDelegate f, IntPtr userData);

        /// <summary>
        /// This function signals the occurrence of an event.
        ///
        /// An event is a signal used to inform one or more consumers
        /// to start an action.  Each consumer which has registered an interest
        /// in the event (e.g. by calling <see cref="eventSetFunc"/>) will be informed by
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
        public static extern ResultCode EventTrigger(uint eventId);
    }
}
