using $safeprojectname$.Features.Employees.Queries.GetEmployees;
using $ext_projectname$.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $safeprojectname$.Interfaces.Repositories
{
    public interface IEmployeeRepositoryAsync : IGenericRepositoryAsync<Employee>
    {
        Task<IEnumerable<Entity>> GetPagedEmployeeReponseAsync(GetEmployeesQuery requestParameter);
    }
}
