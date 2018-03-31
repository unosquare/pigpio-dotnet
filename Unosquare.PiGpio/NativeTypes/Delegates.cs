namespace Unosquare.PiGpio.NativeTypes
{
    using NativeEnums;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Defines a signature for a pthread worker
    /// </summary>
    /// <param name="userData">The user data.</param>
    public delegate void PiGpioThreadDelegate(UIntPtr userData);

    /// <summary>
    /// Defines a signature for alert callbacks. Conatins the pin number, a level change and a microseconds
    /// timestamp. The timestamp wraps around every ~72 minutes.
    /// </summary>
    /// <param name="userGpio">The user gpio.</param>
    /// <param name="levelChange">The level change.</param>
    /// <param name="timeMicrosecs">The microseconds timestamp.</param>
    public delegate void PiGpioAlertDelegate(UserGpio userGpio, LevelChange levelChange, uint timeMicrosecs);

    /// <summary>
    /// Defines a signature for alert callbacks. Conatins the pin number, a level change and a microseconds
    /// timestamp. The timestamp wraps around every ~72 minutes.
    /// </summary>
    /// <param name="userGpio">The user gpio.</param>
    /// <param name="levelChange">The level change.</param>
    /// <param name="timeMicrosecs">The time microsecs.</param>
    /// <param name="userData">The user data.</param>
    public delegate void PiGpioAlertExDelegate(UserGpio userGpio, LevelChange levelChange, uint timeMicrosecs, UIntPtr userData);

    /// <summary>
    /// Defines a signature for ISR callbacks. Conatins the pin number, a level change and a microseconds
    /// timestamp. The timestamp wraps around every ~72 minutes.
    /// </summary>
    /// <param name="gpio">The gpio.</param>
    /// <param name="level">The level.</param>
    /// <param name="timeMicrosecs">The time microsecs.</param>
    public delegate void PiGpioIsrDelegate(SystemGpio gpio, LevelChange level, uint timeMicrosecs);

    /// <summary>
    /// Defines a signature for ISR callbacks. Conatins the pin number, a level change and a microseconds
    /// timestamp. The timestamp wraps around every ~72 minutes.
    /// </summary>
    /// <param name="gpio">The gpio.</param>
    /// <param name="level">The level.</param>
    /// <param name="timeMicrosecs">The time microsecs.</param>
    /// <param name="userData">The user data.</param>
    public delegate void PiGpioIsrExDelegate(SystemGpio gpio, LevelChange level, uint timeMicrosecs, UIntPtr userData);

    public delegate void PiGpioTimerDelegate();

    public delegate void PiGpioTimerExDelegate(UIntPtr userData);

    public delegate void PiGpioEventDelegate(int eventId, uint timeMicrosecs);

    public delegate void PiGpioEventExDelegate(int eventId, uint tick, UIntPtr userData);

    public delegate void PiGpioSignalDelegate(int signalNumber);

    public delegate void PiGpioSignalExDelegate(int signalNumber, UIntPtr userData);

    public delegate void PiGpioGetSamplesDelegate([Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] GpioSample[] samples, int numSamples);

    public delegate void PiGpioGetSamplesExDelegate([Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] GpioSample[] samples, int numSamples, UIntPtr userData);
}
