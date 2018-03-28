using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unosquare.PiGpio.Enums
{
    [Flags]
    public enum FileModeFlags
    {
        Read = 1,
        Write = 2,
        Append = 4,
        Create = 8,
        Truncate = 16
    }
}
