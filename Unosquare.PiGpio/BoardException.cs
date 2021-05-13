﻿namespace Unosquare.PiGpio
{
    using NativeEnums;
    using System;

    /// <summary>
    /// Represents a PiGpio Library call exception.
    /// </summary>
    /// <seealso cref="Exception" />
    public class BoardException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoardException"/> class.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        private BoardException(int resultCode)
            : base(GetStarndardMessage(resultCode))
        {
            ResultCode = (ResultCode)resultCode;
        }

        /// <summary>
        /// Gets the result code.
        /// </summary>
        public ResultCode ResultCode { get; }

        /// <summary>
        /// Validates the result. This call is typically used for Setter methods.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <returns>The Result Code.</returns>
        internal static ResultCode ValidateResult(ResultCode resultCode)
        {
            return (ResultCode)ValidateResult((int)resultCode);
        }

        /// <summary>
        /// Validates the result. This call is typically used for Getter methods.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <returns>The integer result.</returns>
        internal static uint ValidateResult(int resultCode)
        {
            if (resultCode < 0)
                throw new BoardException(resultCode);

            return (uint)resultCode;
        }

        /// <summary>
        /// Validates the result. This call is typically used for execute methods.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns>The pointer or handle.</returns>
        internal static UIntPtr ValidateResult(UIntPtr handle)
        {
            if (handle == UIntPtr.Zero)
                throw new BoardException((int)ResultCode.BadHandle);

            return handle;
        }

        /// <summary>
        /// Gets the starndard message.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <returns>The standard corresponding error message based on the result code.</returns>
        private static string GetStarndardMessage(int resultCode)
        {
            return $"Hardware Exception Encountered. Error Code {resultCode}: {(ResultCode)resultCode}: " +
                $"{Constants.GetResultCodeMessage(resultCode)}";
        }
    }
}
