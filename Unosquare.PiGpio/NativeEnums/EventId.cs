namespace Unosquare.PiGpio.NativeEnums
{
    using System;

    /// <summary>
    /// Enumerates the different Event Identifiers
    /// </summary>
    [Flags]
    public enum EventId : uint
    {
        /// <summary>
        /// No event
        /// </summary>
        None = 0,

        /// <summary>The Event Identifier for GPIO 00</summary>
        Event00 = 0x00000001,

        /// <summary>The Event Identifier for GPIO 01</summary>
        Event01 = 0x00000002,

        /// <summary>The Event Identifier for GPIO 02</summary>
        Event02 = 0x00000004,

        /// <summary>The Event Identifier for GPIO 03</summary>
        Event03 = 0x00000008,

        /// <summary>The Event Identifier for GPIO 04</summary>
        Event04 = 0x00000010,

        /// <summary>The Event Identifier for GPIO 05</summary>
        Event05 = 0x00000020,

        /// <summary>The Event Identifier for GPIO 06</summary>
        Event06 = 0x00000040,

        /// <summary>The Event Identifier for GPIO 07</summary>
        Event07 = 0x00000080,

        /// <summary>The Event Identifier for GPIO 08</summary>
        Event08 = 0x00000100,

        /// <summary>The Event Identifier for GPIO 09</summary>
        Event09 = 0x00000200,

        /// <summary>The Event Identifier for GPIO 10</summary>
        Event10 = 0x00000400,

        /// <summary>The Event Identifier for GPIO 11</summary>
        Event11 = 0x00000800,

        /// <summary>The Event Identifier for GPIO 12</summary>
        Event12 = 0x00001000,

        /// <summary>The Event Identifier for GPIO 13</summary>
        Event13 = 0x00002000,

        /// <summary>The Event Identifier for GPIO 14</summary>
        Event14 = 0x00004000,

        /// <summary>The Event Identifier for GPIO 15</summary>
        Event15 = 0x00008000,

        /// <summary>The Event Identifier for GPIO 16</summary>
        Event16 = 0x00010000,

        /// <summary>The Event Identifier for GPIO 17</summary>
        Event17 = 0x00020000,

        /// <summary>The Event Identifier for GPIO 18</summary>
        Event18 = 0x00040000,

        /// <summary>The Event Identifier for GPIO 19</summary>
        Event19 = 0x00080000,

        /// <summary>The Event Identifier for GPIO 20</summary>
        Event20 = 0x00100000,

        /// <summary>The Event Identifier for GPIO 21</summary>
        Event21 = 0x00200000,

        /// <summary>The Event Identifier for GPIO 22</summary>
        Event22 = 0x00400000,

        /// <summary>The Event Identifier for GPIO 23</summary>
        Event23 = 0x00800000,

        /// <summary>The Event Identifier for GPIO 24</summary>
        Event24 = 0x01000000,

        /// <summary>The Event Identifier for GPIO 25</summary>
        Event25 = 0x02000000,

        /// <summary>The Event Identifier for GPIO 26</summary>
        Event26 = 0x04000000,

        /// <summary>The Event Identifier for GPIO 27</summary>
        Event27 = 0x08000000,

        /// <summary>The Event Identifier for GPIO 28</summary>
        Event28 = 0x10000000,

        /// <summary>The Event Identifier for GPIO 29</summary>
        Event29 = 0x20000000,

        /// <summary>The Event Identifier for GPIO 30</summary>
        Event30 = 0x40000000,

        /// <summary>The Event Identifier for GPIO 31</summary>
        Event31 = 0x80000000,
    }
}
