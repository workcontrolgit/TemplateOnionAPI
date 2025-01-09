namespace $safeprojectname$.Features.Positions.Commands.CreatePosition
{
    /// <summary>
    /// Validator for the CreatePositionCommand.
    /// </summary>
    public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
    {
        private readonly IPositionRepositoryAsync _repository;

        /// <summary>
        /// Initializes a new instance of the CreatePositionCommandValidator class.
        /// </summary>
        /// <param name="repository">The repository to use for validation.</param>
        public CreatePositionCommandValidator(IPositionRepositoryAsync repository)
        {
            _repository = repository;

            // Rule for validating the PositionNumber property.
            RuleFor(p => p.PositionNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(IsUniquePositionNumber).WithMessage("{PropertyName} already exists.");

            // Rule for validating the PositionTitle property.
            RuleFor(p => p.PositionTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }

        /// <summary>
        /// Checks if a position number is unique by querying the repository.
        /// </summary>
        /// <param name="positionNumber">The position number to check.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>True if the position number is unique, false otherwise.</returns>
        private async Task<bool> IsUniquePositionNumber(string positionNumber, CancellationToken cancellationToken)
        {
            return await _repository.IsUniquePositionNumberAsync(positionNumber);
        }
    }
}