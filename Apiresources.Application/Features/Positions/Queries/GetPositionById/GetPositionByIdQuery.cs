using MediatR;
using $safeprojectname$.Exceptions;
using $safeprojectname$.Interfaces.Repositories;
using $safeprojectname$.Wrappers;
using $ext_projectname$.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Positions.Queries.GetPositionById
{
    public class GetPositionByIdQuery : IRequest<Response<Position>>
    {
        public Guid Id { get; set; }

        public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, Response<Position>>
        {
            private readonly IPositionRepositoryAsync _repository;

            public GetPositionByIdQueryHandler(IPositionRepositoryAsync repository)
            {
                _repository = repository;
            }

            public async Task<Response<Position>> Handle(GetPositionByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(query.Id);
                if (entity == null) throw new ApiException($"Position Not Found.");
                return new Response<Position>(entity);
            }
        }
    }
}