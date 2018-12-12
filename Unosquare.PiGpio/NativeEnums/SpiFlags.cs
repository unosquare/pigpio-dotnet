namespace Unosquare.PiGpio.NativeEnums
{
    using System;

    /// <summary>
    /// SPI Open Flags.
    /// </summary>
    [Flags]
    public enum SpiFlags
    {
        /// <summary>
        /// The default
        /// </summary>
        Default = 0b000000_0_0_0000_0_0_000_000_00,

        /// <summary>
        /// The mode bit0
        /// </summary>
        ModeBit0 = 0b000000_0_0_0000_0_0_000_000_01,

        /// <summary>
        /// The mode bit1
        /// </summary>
        ModeBit1 = 0b000000_0_0_0000_0_0_000_000_10,

        /// <summary>
        /// The chip enable active low bit0
        /// </summary>
        ChipEnableActiveLowBit0 = 0b000000_0_0_0000_0_0_000_001_00,

        /// <summary>
        /// The chip enable active low bit1
        /// </summary>
        ChipEnableActiveLowBit1 = 0b000000_0_0_0000_0_0_000_010_00,

        /// <summary>
        /// The chip enable active low bit2
        /// </summary>
        ChipEnableActiveLowBit2 = 0b000000_0_0_0000_0_0_000_100_00,

        /// <summary>
        /// The chip enable reserved bit0
        /// </summary>
        ChipEnableReservedBit0 = 0b000000_0_0_0000_0_0_001_000_00,

        /// <summary>
        /// The chip enable reserved bit1
        /// </summary>
        ChipEnableReservedBit1 = 0b000000_0_0_0000_0_0_010_000_00,

        /// <summary>
        /// The chip enable reserved bit2
        /// </summary>
        ChipEnableReservedBit2 = 0b000000_0_0_0000_0_0_100_000_00,

        /// <summary>
        /// The use auxiliary spi
        /// </summary>
        UseAuxiliarySpi = 0b000000_0_0_0000_0_1_000_000_00,

        /// <summary>
        /// The use3 wire device
        /// </summary>
        Use3WireDevice = 0b000000_0_0_0000_1_0_000_000_00,

        /// <summary>
        /// The byte count3 wire bit0
        /// </summary>
        ByteCount3WireBit0 = 0b000000_0_0_0001_0_0_000_000_00,

        /// <summary>
        /// The byte count3 wire bit1
        /// </summary>
        ByteCount3WireBit1 = 0b000000_0_0_0010_0_0_000_000_00,

        /// <summary>
        /// The byte count3 wire bit2
        /// </summary>
        ByteCount3WireBit2 = 0b000000_0_0_0100_0_0_000_000_00,

        /// <summary>
        /// The byte count3 wire bit3
        /// </summary>
        ByteCount3WireBit3 = 0b000000_0_0_1000_0_0_000_000_00,

        /// <summary>
        /// The invert mosi
        /// </summary>
        InvertMosi = 0b000000_0_1_0000_0_0_000_000_00,

        /// <summary>
        /// The invert miso
        /// </summary>
        InvertMiso = 0b000000_1_0_0000_0_0_000_000_00,

        /// <summary>
        /// The word size bit0
        /// </summary>
        WordSizeBit0 = 0b000001_0_0_0000_0_0_000_000_00,

        /// <summary>
        /// The word size bit1
        /// </summary>
        WordSizeBit1 = 0b000010_0_0_0000_0_0_000_000_00,

        /// <summary>
        /// The word size bit2
        /// </summary>
        WordSizeBit2 = 0b000100_0_0_0000_0_0_000_000_00,

        /// <summary>
        /// The word size bit3
        /// </summary>
        WordSizeBit3 = 0b001000_0_0_0000_0_0_000_000_00,

        /// <summary>
        /// The word size bit4
        /// </summary>
        WordSizeBit4 = 0b010000_0_0_0000_0_0_000_000_00,

        /// <summary>
        /// The word size bit5
        /// </summary>
        WordSizeBit5 = 0b100000_0_0_0000_0_0_000_000_00,
    }
}
