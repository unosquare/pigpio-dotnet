namespace Unosquare.PiGpio.NativeEnums
{
    using System;

    [Flags]
    public enum InterfaceFlags
    {
        DisableFifoInterface = 1,
        DisableSocketInterface = 2,
        LocalhostInterface = 4,
    }
}
