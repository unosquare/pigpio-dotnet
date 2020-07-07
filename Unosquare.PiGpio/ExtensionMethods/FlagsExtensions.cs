namespace Unosquare.PiGpio.ExtensionMethods
{
    using System;
    using System.Collections;

    /// <summary>
    /// Extension methods for bit flags
    /// </summary>
    public static class FlagsExtensions {
        /// <summary>
        /// Applies bit values according to the indexes.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="value">True to set the 1 bits. False to clear the 1 bits.</param>
        /// <param name="indexes">The indexes.</param>
        /// <returns>The applied bitmask.</returns>
        public static int ApplyBits(this int flags, bool value, params int[] indexes)
        {
            var array = new BitArray(32);
            foreach (var index in indexes)
                array[index] = true;

            var bytes = new byte[4];
            // TODO: array.CopyTo(bytes);
            var mask = BitConverter.ToInt32(bytes, 0);

            return value ? flags | mask : flags & ~mask;
        }

        /// <summary>
        /// Sets a bit at the given position index from right to left.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>The flags with the bit set at the given position.</returns>
        public static int SetBit(this int flags, int index, bool value) => value ? flags | (1 << index) : flags & ~(1 << index);

        /// <summary>
        /// Sets a bit at the given position index from right to left.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>The flags with the bit set at the given position.</returns>
        public static uint SetBit(this uint flags, int index, bool value) => unchecked((uint)SetBit(unchecked((int)flags), index, value));

        /// <summary>
        /// Sets a bit at the given position index from right to left.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>The flags with the bit set at the given position.</returns>
        public static short SetBit(this short flags, int index, bool value) => unchecked((short)SetBit((int)flags, index, value));

        /// <summary>
        /// Sets a bit at the given position index from right to left.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>The flags with the bit set at the given position.</returns>
        public static ushort SetBit(this ushort flags, int index, bool value) => unchecked((ushort)SetBit((int)flags, index, value));

        /// <summary>
        /// Sets a bit at the given position index from right to left.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns>The flags with the bit set at the given position.</returns>
        public static byte SetBit(this byte flags, int index, bool value) => unchecked((byte)SetBit((int)flags, index, value));

        /// <summary>
        /// Gets a bit at the given position index from right to left.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value of the bit at the given position index.</returns>
        public static bool GetBit(this int flags, int index) => (flags & (1 << index)) != 0;

        /// <summary>
        /// Gets a bit at the given position index from right to left.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value of the bit at the given position index.</returns>
        public static bool GetBit(this uint flags, int index) => GetBit(unchecked((int)flags), index);

        /// <summary>
        /// Gets a bit at the given position index from right to left.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value of the bit at the given position index.</returns>
        public static bool GetBit(this short flags, int index) => GetBit((int)flags, index);

        /// <summary>
        /// Gets a bit at the given position index from right to left.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value of the bit at the given position index.</returns>
        public static bool GetBit(this ushort flags, int index) => GetBit((int)flags, index);

        /// <summary>
        /// Gets a bit at the given position index from right to left.
        /// </summary>
        /// <param name="flags">The flags.</param>
        /// <param name="index">The index.</param>
        /// <returns>The value of the bit at the given position index.</returns>
        public static bool GetBit(this byte flags, int index) => GetBit((int)flags, index);
    }
}