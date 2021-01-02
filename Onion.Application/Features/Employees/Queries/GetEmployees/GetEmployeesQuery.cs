using AutoMapper;
using $safeprojectname$.Filters;
using $safeprojectname$.Interfaces.Repositories;
using $safeprojectname$.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Employees.Queries.GetEmployees
{
    /// <summary>
    /// GetAllEmployeesQuery - handles media IRequest
    /// BaseRequestParameter - contains paging parameters
    /// To add filter/search parameters, add search properties to the body of this class
    /// </summary>
    public class GetEmployeesQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        //examples:
        public string EmployeeNumber { get; set; }
        public string EmployeeTitle { get; set; }
    }
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IModelHelper _modelHelper;
        public GetAllEmployeesQueryHandler(IEmployeeRepositoryAsync employeeRepository, IMapper mapper, IModelHelper modelHelper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _modelHelper = modelHelper;
        }

        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {

            //filtered fields security
            if (!string.IsNullOrEmpty(request.Fields))
            {
                //limit to fields in view model
                validFilter.Fields = _modelHelper.ValidateModelFields<GetEmployeesViewModel>(request.Fields);

            }
            if (string.IsNullOrEmpty(request.Fields))
            {
                //default fields from view model
                validFilter.Fields = _modelHelper.GetModelFields<GetEmployeesViewModel>();
            }
            // query based on filter
            var entityEmployees = await _employeeRepository.GetPagedEmployeeReponseAsync(request);
            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(entityEmployees, request.PageNumber, request.PageSize);
        }
    }
}