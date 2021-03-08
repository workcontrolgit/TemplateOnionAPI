using AutoMapper;
using $safeprojectname$.Interfaces.Repositories;
using $safeprojectname$.Wrappers;
using $ext_projectname$.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Positions.Commands.CreatePosition
{
    public partial class CreatePositionCommand : IRequest<Response<Position>>
    {
        public string PositionTitle { get; set; }
        public string PositionNumber { get; set; }
        public string PositionDescription { get; set; }
        public decimal PositionSalary { get; set; }
    }

    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Response<Position>>
    {
        private readonly IPositionRepositoryAsync _positionRepository;
        private readonly IMapper _mapper;

        public CreatePositionCommandHandler(IPositionRepositoryAsync positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<Response<Position>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            var position = _mapper.Map<Position>(request);
            var entityPosition = await _positionRepository.AddAsync(position);
            return new Response<Position>(entityPosition);
        }
    }
}
