namespace $safeprojectname$.Exceptions {
    /// <summary>
    /// Custom exception class for API-related errors.
    /// </summary>
    public class ApiException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class with default values.
        /// </summary>
        public ApiException() : base() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that describes the cause of the exception.</param>
        public ApiException(string message) : base(message) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class with a specified error message and arguments.
        /// </summary>
        /// <param name="message">The error message that describes the cause of the exception.</param>
        /// <param name="args">An object array that contains zero or more objects to format into the error message string.</param>
        public ApiException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class with a specified error message and an inner exception.
        /// </summary>
        /// <param name="message">The error message that describes the cause of the exception.</param>
        /// <param name="innerException">The exception that caused the current exception, or null if no inner exception is present.</param>
        public ApiException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}