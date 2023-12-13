using AutoMapper;
using MediatR;
using $safeprojectname$.Interfaces;
using $safeprojectname$.Interfaces.Repositories;
using $safeprojectname$.Parameters;
using $safeprojectname$.Wrappers;
using $ext_projectname$.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Positions.Queries.GetPositions
{
    public class GetPositionsQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        public string PositionNumber { get; set; }
        public string PositionTitle { get; set; }
    }

    public class GetAllPositionsQueryHandler : IRequestHandler<GetPositionsQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IPositionRepositoryAsync _repository;
        private readonly IModelHelper _modelHelper;

        public GetAllPositionsQueryHandler(IPositionRepositoryAsync repository, IModelHelper modelHelper)
        {
            _repository = repository;
            _modelHelper = modelHelper;
        }

        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {

            var validFilter = request;
            //filtered fields security
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetPositionsViewModel>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetPositionsViewModel>();
            }
            // query based on filter
            var qryResult = await _repository.GetPagedPositionReponseAsync(validFilter);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;
            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }
    }
}