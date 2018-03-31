namespace Unosquare.PiGpio
{
    using System;
    using NativeEnums;

    /// <summary>
    /// Represents a PiGpio Library call exception
    /// </summary>
    /// <seealso cref="Exception" />
    public class BoardException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoardException"/> class.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <param name="message">The message.</param>
        public BoardException(ResultCode resultCode, string message)
            : base(message)
        {
            ResultCode = resultCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoardException"/> class.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <param name="message">The message.</param>
        public BoardException(int resultCode, string message)
            : base(message)
        {
            ResultCode = (ResultCode)resultCode;
        }

        /// <summary>
        /// Gets the result code.
        /// </summary>
        public ResultCode ResultCode { get; }

        /// <summary>
        /// Validates the result.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <returns>The Result Code</returns>
        internal static ResultCode ValidateResult(ResultCode resultCode)
        {
            return (ResultCode)ValidateResult((int)resultCode);
        }

        /// <summary>
        /// Validates the result.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <returns>The integer result</returns>
        internal static int ValidateResult(int resultCode)
        {
            return ValidateResult(resultCode, $"GPIO Exception Encountered. Error Code {resultCode}: {(ResultCode)resultCode}.");
        }

        /// <summary>
        /// Validates the result.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <param name="message">The message.</param>
        /// <returns>The integer result</returns>
        /// <exception cref="BoardException">When the result is negative</exception>
        internal static int ValidateResult(int resultCode, string message)
        {
            if (resultCode < 0)
                throw new BoardException(resultCode, message);

            return resultCode;
        }
    }
}
