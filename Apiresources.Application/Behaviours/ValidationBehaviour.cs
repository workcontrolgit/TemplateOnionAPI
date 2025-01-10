namespace $safeprojectname$.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        // A collection of validators that will be used to validate the request.
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        // Constructor that takes in a collection of validators.
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        // This method is called when a request is being handled by the pipeline. It first checks if there are any validators to use. If there are, it uses them to validate the request. If there are any validation failures, it throws a ValidationException. If all validations pass, it calls the next handler in the pipeline.
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                // Create a new ValidationContext with the request object.
                var context = new ValidationContext<TRequest>(request);

                // Use all validators to validate the request asynchronously. 
                var validationResults = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

                // Extract any validation failures from the validation results.
                var failures = validationResults.SelectMany(validationResult => validationResult.Errors)
                    .Where(validationResult => validationResult != null)
                    .ToList();

                // If there are any validation failures, throw a ValidationException with the list of failures.
                if (failures.Any())
                    throw new Exceptions.ValidationException(failures);
            }
            
            // Call the next handler in the pipeline and return its result.
            return await next();
        }
    }
}