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