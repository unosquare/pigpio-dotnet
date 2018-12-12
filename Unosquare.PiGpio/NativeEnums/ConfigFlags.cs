namespace Unosquare.PiGpio.NativeEnums
{
    using System;

    /// <summary>
    /// Enumerates the different configuration flags.
    /// </summary>
    [Flags]
    public enum ConfigFlags : uint
    {
        /// <summary>
        /// The debug level0
        /// </summary>
        DebugLevel0 = 1 << 0,

        /// <summary>
        /// The debug level1
        /// </summary>
        DebugLevel1 = 1 << 1,

        /// <summary>
        /// The debug level2
        /// </summary>
        DebugLevel2 = 1 << 2,

        /// <summary>
        /// The debug level3
        /// </summary>
        DebugLevel3 = 1 << 3,

        /// <summary>
        /// The alert frequency0
        /// </summary>
        AlertFrequency0 = 1 << 4,

        /// <summary>
        /// The alert frequency1
        /// </summary>
        AlertFrequency1 = 1 << 5,

        /// <summary>
        /// The alert frequency2
        /// </summary>
        AlertFrequency2 = 1 << 6,

        /// <summary>
        /// The alert frequency3
        /// </summary>
        AlertFrequency3 = 1 << 7,

        /// <summary>
        /// The real time priority
        /// </summary>
        RealTimePriority = 1 << 8,

        /// <summary>
        /// The stats
        /// </summary>
        Stats = 1 << 9,

        /// <summary>
        /// The no signal handler
        /// </summary>
        NoSignalHandler = 1 << 10,
    }
}
