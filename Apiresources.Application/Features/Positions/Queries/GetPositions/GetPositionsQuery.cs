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
        public string Department { get; set; }
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
            var objRequest = request;
            // verify request fields are valid field and exist in the DTO
            if (!string.IsNullOrEmpty(objRequest.Fields))
            {
                //limit to fields in view model
                objRequest.Fields = _modelHelper.ValidateModelFields<GetPositionsViewModel>(objRequest.Fields);
            }
            if (string.IsNullOrEmpty(objRequest.Fields))
            {
                //default fields from view model
                objRequest.Fields = _modelHelper.GetModelFields<GetPositionsViewModel>();
            }
            // verify orderby a valid field and exist in the DTO GetPositionsViewModel
            if (!string.IsNullOrEmpty(objRequest.OrderBy))
            {
                //limit to fields in view model
                objRequest.OrderBy = _modelHelper.ValidateModelFields<GetPositionsViewModel>(objRequest.OrderBy);
            }

            // query based on filter
            var qryResult = await _repository.GetPositionReponseAsync(objRequest);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;
            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, objRequest.PageNumber, objRequest.PageSize, recordCount);
        }
    }
}