namespace $safeprojectname$.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        // Next delegate/middleware in the pipeline
        private readonly RequestDelegate _next; 
        // Logger for ErrorHandlerMiddleware
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        // Constructor to initialize ErrorHandlerMiddleware with the next delegate and logger
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Method to handle incoming HTTP requests and catch exceptions
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json"; // Set the response content type to JSON

                // Create a response model indicating failure and capturing the error message
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };

                // Determine the status code based on the error type
                switch (error)
                {
                    case ApiException:
                        // Custom application error, set status code to 400
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case ValidationException e:
                        // Custom application error with validation errors, set status code to 400
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors; // Capture validation errors
                        break;

                    case KeyNotFoundException:
                        // Not found error, set status code to 404
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        // Unhandled error, set status code to 500
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                
                // Log the exception message using ILogger
                _logger.LogError(error.Message);

                // Serialize the response model to a JSON string
                var result = JsonSerializer.Serialize(responseModel);

                // Write the serialized response to the HTTP response
                await response.WriteAsync(result);
            }
        }
    }

}