using Microsoft.EntityFrameworkCore;
using $ext_projectname$.Application.Interfaces.Repositories;
using $ext_projectname$.Domain.Entities;
using $safeprojectname$.Contexts;
using $safeprojectname$.Repository;

namespace $safeprojectname$.Repositories
{
    public class SalaryRangeRepositoryAsync : GenericRepositoryAsync<SalaryRange>, ISalaryRangeRepositoryAsync
    {
        private readonly DbSet<SalaryRange> _repository;

        public SalaryRangeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _repository = dbContext.Set<SalaryRange>();
        }
    }
}