using MediatR;
using $safeprojectname$.Exceptions;
using $safeprojectname$.Interfaces.Repositories;
using $safeprojectname$.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Positions.Commands.UpdatePosition
{
    public class UpdatePositionCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string PositionTitle { get; set; }
        public string PositionDescription { get; set; }
        public decimal PositionSalary { get; set; }

        public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Response<Guid>>
        {
            private readonly IPositionRepositoryAsync _repository;

            public UpdatePositionCommandHandler(IPositionRepositoryAsync repository)
            {
                _repository = repository;
            }

            public async Task<Response<Guid>> Handle(UpdatePositionCommand command, CancellationToken cancellationToken)
            {
                var position = await _repository.GetByIdAsync(command.Id);

                if (position == null)
                {
                    throw new ApiException($"Position Not Found.");
                }
                else
                {
                    position.PositionTitle = command.PositionTitle;
                    position.PositionSalary = command.PositionSalary;
                    position.PositionDescription = command.PositionDescription;
                    await _repository.UpdateAsync(position);
                    return new Response<Guid>(position.Id);
                }
            }
        }
    }
}