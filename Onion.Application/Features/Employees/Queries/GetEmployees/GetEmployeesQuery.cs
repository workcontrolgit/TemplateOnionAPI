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
    public class GetEmployeesQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<GetEmployeesViewModel>>>
    {
        //examples:
        public string EmployeeNumber { get; set; }
        public string EmployeeTitle { get; set; }
    }
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, PagedResponse<IEnumerable<GetEmployeesViewModel>>>
    {
        private readonly IEmployeeRepositoryAsync _employeeRepository;
        private readonly IMapper _mapper;
        public GetAllEmployeesQueryHandler(IEmployeeRepositoryAsync employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetEmployeesViewModel>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetPagedEmployeeReponseAsync(request);
            var employeeViewModel = _mapper.Map<IEnumerable<GetEmployeesViewModel>>(employees);
            return new PagedResponse<IEnumerable<GetEmployeesViewModel>>(employeeViewModel, request.PageNumber, request.PageSize);
        }
    }
}
