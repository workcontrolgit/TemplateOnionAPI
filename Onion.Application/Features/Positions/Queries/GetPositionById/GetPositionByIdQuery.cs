using $safeprojectname$.Exceptions;
using $safeprojectname$.Interfaces.Repositories;
using $safeprojectname$.Wrappers;
using $ext_projectname$.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Positions.Queries.GetPositionById
{
    public class GetPositionByIdQuery : IRequest<Response<Position>>
    {
        public int Id { get; set; }
        public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, Response<Position>>
        {
            private readonly IPositionRepositoryAsync _positionRepository;
            public GetPositionByIdQueryHandler(IPositionRepositoryAsync positionRepository)
            {
                _positionRepository = positionRepository;
            }
            public async Task<Response<Position>> Handle(GetPositionByIdQuery query, CancellationToken cancellationToken)
            {
                var position = await _positionRepository.GetByIdAsync(query.Id);
                if (position == null) throw new ApiException($"Position Not Found.");
                return new Response<Position>(position);
            }
        }
    }
}
