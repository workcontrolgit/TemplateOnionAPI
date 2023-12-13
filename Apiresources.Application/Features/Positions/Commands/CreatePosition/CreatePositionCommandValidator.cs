using FluentValidation;
using $safeprojectname$.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Positions.Commands.CreatePosition
{
    public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
    {
        private readonly IPositionRepositoryAsync _repository;

        public CreatePositionCommandValidator(IPositionRepositoryAsync repository)
        {
            _repository = repository;

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
            return await _repository.IsUniquePositionNumberAsync(positionNumber);
        }
    }
}