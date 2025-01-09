namespace $safeprojectname$.Repositories
{
    /// <summary>
    /// Represents a repository for asynchronous operations on the Department entity.
    /// </summary>
    public class DepartmentRepositoryAsync : GenericRepositoryAsync<Department>, IDepartmentRepositoryAsync
    {
        /// <summary>
        /// Initializes a new instance of the DepartmentRepositoryAsync class with the provided database context.
        /// </summary>
        /// <param name="dbContext">The application's DbContext.</param>
        public DepartmentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}