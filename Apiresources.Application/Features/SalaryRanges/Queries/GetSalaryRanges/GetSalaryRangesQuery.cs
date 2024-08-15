using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using $safeprojectname$.Interfaces;
using $safeprojectname$.Interfaces.Repositories;
using $safeprojectname$.Parameters;

namespace $safeprojectname$.Features.SalaryRanges.Queries.GetSalaryRanges
{
    /// <summary>
    /// GetAllSalaryRangesQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetSalaryRangesQuery : ListParameter, IRequest<IEnumerable<GetSalaryRangesViewModel>>
    {
    }

    public class GetAllSalaryRangesQueryHandler : IRequestHandler<GetSalaryRangesQuery, IEnumerable<GetSalaryRangesViewModel>>
    {
        private readonly ISalaryRangeRepositoryAsync _repository;
        private readonly IModelHelper _modelHelper;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for GetAllSalaryRangesQueryHandler class.
        /// </summary>
        /// <param name="repository">ISalaryRangeRepositoryAsync object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// GetAllSalaryRangesQueryHandler object.
        /// </returns>
        public GetAllSalaryRangesQueryHandler(ISalaryRangeRepositoryAsync repository, IModelHelper modelHelper, IMapper mapper)
        {
            _repository = repository;
            _modelHelper = modelHelper;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the GetSalaryRangesQuery request and returns a PagedResponse containing the requested data.
        /// </summary>
        /// <param name="request">The GetSalaryRangesQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedResponse containing the requested data.</returns>
        public async Task<IEnumerable<GetSalaryRangesViewModel>> Handle(GetSalaryRangesQuery request, CancellationToken cancellationToken)
        {
            string fields = _modelHelper.GetModelFields<GetSalaryRangesViewModel>();
            string defaultOrderByColumn = "Name";

            string orderBy = string.Empty;

            // if the request orderby is not null
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                // check to make sure order by field is valid and in the view model
                orderBy = _modelHelper.ValidateModelFields<GetSalaryRangesViewModel>(request.OrderBy);
            }

            // if the order by is invalid
            if (string.IsNullOrEmpty(orderBy))
            {
                //default fields from view model
                orderBy = defaultOrderByColumn;
            }

            var data = await _repository.GetAllShapeAsync(orderBy, fields);

            // automap to ViewModel
            var viewModel = _mapper.Map<IEnumerable<GetSalaryRangesViewModel>>(data);

            return viewModel;
        }
    }
}