namespace Unosquare.PiGpio
{
    using Enums;
    using NativeTypes;
    using System;
    using System.Runtime.InteropServices;

    public delegate void GpioAlertDelegate(UserGpio gpioPin, LevelChange levelChange, uint tick);

    public delegate void GpioAlertExDelegate(UserGpio gpioPin, LevelChange levelChange, uint tick, IntPtr userData);

    public delegate void EventDelegate(int eventId, uint tick);

    public delegate void EventExDelegate(int eventId, uint tick, IntPtr userData);

    public delegate void GpioISRDelegate(SystemGpio gpioPin, LevelChange level, uint tick);

    public delegate void GpioISRExDelegate(SystemGpio gpioPin, LevelChange level, uint tick, IntPtr userData);

    public delegate void GpioTimerDelegate();

    public delegate void GpioTimerExDelegate(IntPtr userData);

    public delegate void GpioSignalDelegate(int signalNumber);

    public delegate void GpioSignalExDelegate(int signalNumber, IntPtr userData);

    public delegate void GpioGetSamplesDelegate([MarshalAs(UnmanagedType.LPArray)] GpioSample[] samples, int numSamples);

    public delegate void GpioGetSamplesExDelegate([MarshalAs(UnmanagedType.LPArray)] GpioSample[] samples, int numSamples, IntPtr userData);

    public delegate void GpioThreadDelegate();
}
