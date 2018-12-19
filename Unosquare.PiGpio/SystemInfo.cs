namespace Unosquare.PiGpio
{
    using RaspberryIO.Abstractions;
    using System;

    /// <summary>
    /// Represents the system info.
    /// </summary>
    /// <seealso cref="ISystemInfo" />
    public class SystemInfo : ISystemInfo
    {
        /// <inheritdoc />
        public BoardRevision BoardRevision { get; }
        
        /// <inheritdoc />
        public Version LibraryVersion { get; }
    }
}
