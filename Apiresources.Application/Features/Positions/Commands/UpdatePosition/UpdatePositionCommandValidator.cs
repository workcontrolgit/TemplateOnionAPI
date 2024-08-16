namespace $safeprojectname$.Features.Positions.Commands.CreatePosition
{
    public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
    {
        public UpdatePositionCommandValidator(IPositionRepositoryAsync repository)
        {
            RuleFor(p => p.PositionNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.PositionTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}