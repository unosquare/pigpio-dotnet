namespace Unosquare.PiGpio
{
    using System;
    using NativeEnums;

    /// <summary>
    /// Represents a PiGpio Library call exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class PiGpioException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PiGpioException"/> class.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <param name="message">The message.</param>
        public PiGpioException(ResultCode resultCode, string message)
            : base(message)
        {
            ResultCode = resultCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PiGpioException"/> class.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <param name="message">The message.</param>
        public PiGpioException(int resultCode, string message)
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
        /// <returns></returns>
        internal static ResultCode ValidateResult(ResultCode resultCode)
        {
            return (ResultCode)ValidateResult((int)resultCode);
        }

        /// <summary>
        /// Validates the result.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <returns></returns>
        internal static int ValidateResult(int resultCode)
        {
            return ValidateResult(resultCode, $"GPIO Exception Encountered. Error Code {resultCode}: {(ResultCode)resultCode}.");
        }

        /// <summary>
        /// Validates the result.
        /// </summary>
        /// <param name="resultCode">The result code.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <exception cref="Unosquare.PiGpio.PiGpioException"></exception>
        internal static int ValidateResult(int resultCode, string message)
        {
            if (resultCode < 0)
                throw new PiGpioException(resultCode, message);

            return resultCode;
        }
    }
}
