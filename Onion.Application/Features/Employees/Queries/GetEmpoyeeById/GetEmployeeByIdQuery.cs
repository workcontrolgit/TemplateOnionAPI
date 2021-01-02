using $safeprojectname$.Exceptions;
using $safeprojectname$.Interfaces.Repositories;
using $safeprojectname$.Wrappers;
using $ext_projectname$.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<Response<Employee>>
    {
        public int Id { get; set; }
        public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Response<Employee>>
        {
            private readonly IEmployeeRepositoryAsync _employeeRepository;
            public GetEmployeeByIdQueryHandler(IEmployeeRepositoryAsync employeeRepository)
            {
                _employeeRepository = employeeRepository;
            }
            public async Task<Response<Employee>> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
            {
                var employee = await _employeeRepository.GetByIdAsync(query.Id);
                if (employee == null) throw new ApiException($"Employee Not Found.");
                return new Response<Employee>(employee);
            }
        }
    }
}
