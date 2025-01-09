namespace $safeprojectname$.Repositories
{
    // Repository class for handling operations related to the SalaryRange entity asynchronously
    public class SalaryRangeRepositoryAsync : GenericRepositoryAsync<SalaryRange>, ISalaryRangeRepositoryAsync
    {
        // Entity framework set for interacting with the SalaryRange entities in the database
        private readonly DbSet<SalaryRange> _repository;

        // Constructor for the SalaryRangeRepositoryAsync class
        // Takes in the application's database context and passes it to the base class constructor
        public SalaryRangeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            // Initialize the _repository field by associating it with the SalaryRange set in the database context
            _repository = dbContext.Set<SalaryRange>();
        }
    }
}