using $safeprojectname$.Interfaces.Repositories;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Positions.Commands.CreatePosition
{
    public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
    {
        private readonly IPositionRepositoryAsync positionRepository;

        public CreatePositionCommandValidator(IPositionRepositoryAsync positionRepository)
        {
            this.positionRepository = positionRepository;

            RuleFor(p => p.PositionNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(IsUniquePositionNumber).WithMessage("{PropertyName} already exists.");

            RuleFor(p => p.PositionTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }

        private async Task<bool> IsUniquePositionNumber(string positionNumber, CancellationToken cancellationToken)
        {
            return await positionRepository.IsUniquePositionNumberAsync(positionNumber);
        }
    }
}