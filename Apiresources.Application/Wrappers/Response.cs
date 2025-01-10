namespace $safeprojectname$.Wrappers
{
    public class Response<T>
    {
        /// <summary>
        /// Initializes a new instance of the Response class.
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Response class with the specified data and message.
        /// </summary>
        /// <param name="data">The data to return in the response.</param>
        /// <param name="message">A message to accompany the data, if any.</param>
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the Response class with the specified error message.
        /// </summary>
        /// <param name="message">The error message to return in the response.</param>
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        /// <summary>
        /// Indicates whether the operation was successful or not.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// A message to accompany the response, if any.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// A list of error messages, if any.
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// The data returned in the response.
        /// </summary>
        public T Data { get; set; }
    }
}