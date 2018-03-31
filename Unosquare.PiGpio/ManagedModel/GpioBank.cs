namespace Unosquare.PiGpio.ManagedModel
{
    using NativeEnums;
    using NativeMethods;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides access to bulk GPIO read and write operations
    /// </summary>
    public sealed class GpioBank
    {
        #region Fields

        private readonly SetClearBitsDelegate SetBitsCallback;
        private readonly SetClearBitsDelegate ClearBitsCallback;
        private readonly ReadBitsDelegate ReadBitsCallback;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GpioBank"/> class.
        /// </summary>
        /// <param name="bankNumber">The bank number. Must be 1 or 2.</param>
        /// <exception cref="ArgumentException">Bank number can only be either 1 or 2 - bankNumber</exception>
        internal GpioBank(int bankNumber)
        {
            if (bankNumber != 1 && bankNumber != 2)
                throw new ArgumentException("Bank number can only be either 1 or 2", nameof(bankNumber));

            BankNumber = bankNumber;

            SetBitsCallback = bankNumber == 1 ?
                new SetClearBitsDelegate(IO.GpioWriteBits00To31Set) :
                new SetClearBitsDelegate(IO.GpioWriteBits32To53Set);

            ClearBitsCallback = bankNumber == 1 ?
                new SetClearBitsDelegate(IO.GpioWriteBits00To31Clear) :
                new SetClearBitsDelegate(IO.GpioWriteBits32To53Clear);

            ReadBitsCallback = bankNumber == 1 ?
                new ReadBitsDelegate(IO.GpioReadBits00To31) :
                new ReadBitsDelegate(IO.GpioReadBits32To53);

            if (bankNumber == 1)
            {
                MinGpioIndex = 0;
                MaxGpioIndex = 31;
            }
            else
            {
                MinGpioIndex = 32;
                MaxGpioIndex = 53;
            }

            GpioCount = MaxGpioIndex - MinGpioIndex + 1;
        }

        #endregion

        #region Delegates

        private delegate ResultCode SetClearBitsDelegate(BitMask bitMask);
        private delegate uint ReadBitsDelegate();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the bank number; 1 or 2.
        /// </summary>
        public int BankNumber { get; }

        /// <summary>
        /// Gets the minimum index of the gpio bank.
        /// </summary>
        public int MinGpioIndex { get; }

        /// <summary>
        /// Gets the maximum index of the gpio bank.
        /// </summary>
        public int MaxGpioIndex { get; }

        /// <summary>
        /// Gets the number of gpio pins for this bank.
        /// </summary>
        public int GpioCount { get; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Returns a series of 0s and 1s from MSB to LSB.
        /// Please note the output of the Bit Array is reversed.
        /// </summary>
        /// <param name="bits">The bits.</param>
        /// <returns>A string containing 0s and 1s</returns>
        public static string ToBinLiteral(BitArray bits)
        {
            var builder = new StringBuilder(32);
            for (var i = bits.Length - 1; i >= 0; i--)
            {
                builder.Append(bits[i] ? '1' : '0');
            }

            return builder.ToString();
        }

        /// <summary>
        /// Returns a series of hexadecimal chars from MSB to LSB.
        /// Please note the output of the byte array is reversed.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>A string containing the hexadecimal chars</returns>
        public static string ToHexLiteral(byte[] bytes)
        {
            var output = bytes.Reverse().ToArray();
            return BitConverter.ToString(output).Replace("-", string.Empty);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Clears the bits according to the bit mask.
        /// For bank 1, the starting index is GPIO 00.
        /// For bank 2, the starting index is GPIO 32.
        /// This means that for bank 2, bit 33 is addressed as Bit01 in the bit mask.
        /// </summary>
        /// <param name="bitMask">The bit mask.</param>
        public void ClearBits(BitMask bitMask)
        {
            PiGpioException.ValidateResult(ClearBitsCallback(bitMask));
        }

        /// <summary>
        /// Sets the bits according to the bit mask.
        /// For bank 1, the starting index is GPIO 00.
        /// For bank 2, the starting index is GPIO 32.
        /// This means that for bank 2, bit 33 is addressed as Bit01 in the bit mask.
        /// </summary>
        /// <param name="bitMask">The bit mask.</param>
        public void SetBits(BitMask bitMask)
        {
            PiGpioException.ValidateResult(SetBitsCallback(bitMask));
        }

        /// <summary>
        /// Reads the value of all the GPIO pins at once as an unsigned, 32-bit integer.
        /// </summary>
        /// <returns>The current value of all pins</returns>
        public uint ReadValue()
        {
            return ReadBitsCallback();
        }

        /// <summary>
        /// Reads the value of all the GPIO pins at once as an array of 4 bytes.
        /// The 0th index of the result is the Least Significant Byte (low index pins pins).
        /// The 3rd index of the result is the Most Significant Byte (high index pins)
        /// </summary>
        /// <returns>The bytes that were read</returns>
        public byte[] ReadBytes()
        {
            return BitConverter.GetBytes(ReadValue());
        }

        /// <summary>
        /// Reads the value of all the GPIO pins at once, where the 0th index of the array
        /// is the lowest pin index (LSB) and the 31st index of the array is the highes pin index (MSB)
        /// </summary>
        /// <returns>The bits read</returns>
        public BitArray ReadBits()
        {
            return new BitArray(ReadBytes());
        }

        /// <summary>
        /// Reads from the bank and returns a series of 0s and 1s from MSB to LSB.
        /// Please note the output of the Bit Array is reversed so that the MSB is the first character.
        /// </summary>
        /// <returns>A string containing 0s and 1s</returns>
        public string ReadBinLiteral()
        {
            return ToBinLiteral(ReadBits());
        }

        /// <summary>
        /// Reads from the bank and returns a series of hexadecimal chars from MSB to LSB.
        /// Please note the output of the byte array is reversed so that the left-most characters are the MSB.
        /// </summary>
        /// <returns>A string containing the hexadecimal chars</returns>
        public string ReadHexLiteral()
        {
            return ToHexLiteral(ReadBytes());
        }

        #endregion
    }
}
