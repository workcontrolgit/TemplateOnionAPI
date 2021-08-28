using $safeprojectname$.Exceptions;
using $safeprojectname$.Interfaces.Repositories;
using $safeprojectname$.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Positions.Commands.UpdatePosition
{
    public class UpdatePositionCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }

        public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Response<Guid>>
        {
            private readonly IPositionRepositoryAsync _positionRepository;

            public UpdatePositionCommandHandler(IPositionRepositoryAsync positionRepository)
            {
                _positionRepository = positionRepository;
            }

            public async Task<Response<Guid>> Handle(UpdatePositionCommand command, CancellationToken cancellationToken)
            {
                var position = await _positionRepository.GetByIdAsync(command.Id);

                if (position == null)
                {
                    throw new ApiException($"Position Not Found.");
                }
                else
                {
                    position.PositionTitle = command.Title;
                    position.PositionSalary = command.Salary;
                    position.PositionDescription = command.Description;
                    await _positionRepository.UpdateAsync(position);
                    return new Response<Guid>(position.Id);
                }
            }
        }
    }
}