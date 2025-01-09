namespace $safeprojectname$.Features.Positions.Commands.CreatePosition
{
    // Validator class for the UpdatePositionCommand
    public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
    {
        // Constructor to inject the IPositionRepositoryAsync dependency
        public UpdatePositionCommandValidator(IPositionRepositoryAsync repository)
        {
            // Rule to validate that PositionNumber is not empty, null and does not exceed 50 characters.
            RuleFor(p => p.PositionNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            // Rule to validate that PositionTitle is not empty, null and does not exceed 50 characters.
            RuleFor(p => p.PositionTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}