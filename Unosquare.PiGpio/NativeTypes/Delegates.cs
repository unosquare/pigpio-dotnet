namespace Unosquare.PiGpio.NativeTypes
{
    using NativeEnums;
    using System;
    using System.Runtime.InteropServices;

    public delegate void PiGpioAlertDelegate(UserGpio userGpio, LevelChange levelChange, uint tick);

    public delegate void PiGpioAlertExDelegate(UserGpio userGpio, LevelChange levelChange, uint tick, UIntPtr userData);

    public delegate void PiGpioEventDelegate(int eventId, uint tick);

    public delegate void PiGpioEventExDelegate(int eventId, uint tick, UIntPtr userData);

    public delegate void PiGpioISRDelegate(SystemGpio gpio, LevelChange level, uint tick);

    public delegate void PiGpioISRExDelegate(SystemGpio gpio, LevelChange level, uint tick, UIntPtr userData);

    public delegate void PiGpioTimerDelegate();

    public delegate void PiGpioTimerExDelegate(UIntPtr userData);

    public delegate void PiGpioSignalDelegate(int signalNumber);

    public delegate void PiGpioSignalExDelegate(int signalNumber, UIntPtr userData);

    public delegate void PiGpioGetSamplesDelegate([In, MarshalAs(UnmanagedType.LPArray)] GpioSample[] samples, int numSamples);

    public delegate void PiGpioGetSamplesExDelegate([In, MarshalAs(UnmanagedType.LPArray)] GpioSample[] samples, int numSamples, UIntPtr userData);

    public delegate void PiGpioThreadDelegate(UIntPtr userData);
}
