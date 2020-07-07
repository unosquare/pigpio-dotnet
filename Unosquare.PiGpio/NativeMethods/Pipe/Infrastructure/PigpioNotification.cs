using System;
using System.Runtime.InteropServices;

namespace Unosquare.PiGpio.NativeMethods.Pipe.Infrastructure
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct PigpioNotification
    {
        internal UInt16 SequenceNumber;
        internal UInt16 Flags;
        internal UInt32 Tick;
        internal UInt32 Level;
    }
}