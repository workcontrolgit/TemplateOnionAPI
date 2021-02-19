using $safeprojectname$.Exceptions;
using $safeprojectname$.Interfaces.Repositories;
using $safeprojectname$.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace $safeprojectname$.Features.Positions.Commands.UpdatePosition
{
    public class UpdatePositionCommand : IRequest<Response<int>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Response<int>>
        {
            private readonly IPositionRepositoryAsync _positionRepository;
            public UpdatePositionCommandHandler(IPositionRepositoryAsync positionRepository)
            {
                _positionRepository = positionRepository;
            }
            public async Task<Response<int>> Handle(UpdatePositionCommand command, CancellationToken cancellationToken)
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
                    return new Response<int>(position.Id);
                }
            }
        }
    }
}
