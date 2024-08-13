using $ext_projectname$.Application.Interfaces.Repositories;
using $ext_projectname$.Domain.Entities;
using $safeprojectname$.Contexts;
using $safeprojectname$.Repository;

namespace $safeprojectname$.Repositories
{
    public class DepartmentRepositoryAsync : GenericRepositoryAsync<Department>, IDepartmentRepositoryAsync
    {
        public DepartmentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}