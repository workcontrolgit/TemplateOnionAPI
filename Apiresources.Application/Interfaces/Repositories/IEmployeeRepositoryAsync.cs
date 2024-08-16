namespace $safeprojectname$.Interfaces.Repositories
{
    /// <summary>
    /// Interface for retrieving paged employee response asynchronously.
    /// </summary>
    /// <param name="requestParameters">The request parameters.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public interface IEmployeeRepositoryAsync : IGenericRepositoryAsync<Employee>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetEmployeeResponseAsync(GetEmployeesQuery requestParameters);

        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedEmployeeResponseAsync(PagedEmployeesQuery requestParameters);
    }
}