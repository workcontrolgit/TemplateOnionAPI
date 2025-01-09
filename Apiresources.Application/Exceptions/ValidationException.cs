namespace $safeprojectname$.Exceptions
{
    /// <summary>
    /// Represents a validation exception that occurs when one or more validation failures have occurred.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with default values.
        /// </summary>
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Gets a list of error messages that describe the validation failures.
        /// </summary>
        public List<string> Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with a specified message and inner exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or null if no inner exception is specified.</param>
        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationException"/> class with a list of validation failures.
        /// </summary>
        /// <param name="failures">A collection of validation failure objects that describe the errors.</param>
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}
