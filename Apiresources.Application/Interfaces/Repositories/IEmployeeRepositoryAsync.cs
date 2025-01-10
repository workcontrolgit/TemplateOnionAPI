namespace $safeprojectname$.Interfaces.Repositories
{
    /// <summary>
    /// Interface for retrieving paged employee response asynchronously.
    /// </summary>
    public interface IEmployeeRepositoryAsync : IGenericRepositoryAsync<Employee>
    {
        /// <summary>
        /// Retrieves a list of employees based on the provided query parameters asynchronously.
        /// </summary>
        /// <param name="requestParameters">The request parameters.</param>
        /// <returns>A task that represents the asynchronous operation and returns a tuple containing the list of employees and the total number of records.</returns>
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetEmployeeResponseAsync(GetEmployeesQuery requestParameters);

        /// <summary>
        /// Retrieves a paged list of employees based on the provided query parameters asynchronously.
        /// </summary>
        /// <param name="requestParameters">The request parameters.</param>
        /// <returns>A task that represents the asynchronous operation and returns a tuple containing the paged list of employees and the total number of records.</returns>
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedEmployeeResponseAsync(PagedEmployeesQuery requestParameters);
    }
}