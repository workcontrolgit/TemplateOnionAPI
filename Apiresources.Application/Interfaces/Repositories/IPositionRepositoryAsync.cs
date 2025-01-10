namespace $safeprojectname$.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for Position entity with asynchronous methods.
    /// </summary>
    public interface IPositionRepositoryAsync : IGenericRepositoryAsync<Position>
    {
        /// <summary>
        /// Checks if the given position number is unique in the database.
        /// </summary>
        /// <param name="positionNumber">Position number to check for uniqueness.</param>
        /// <returns>
        /// Task indicating whether the position number is unique.
        /// </returns>
        Task<bool> IsUniquePositionNumberAsync(string positionNumber);

        /// <summary>
        /// Seeds initial data into the Position repository. The seed operation will create a specified
        /// number of records in the database, each with a randomly generated name and birthdate.
        /// </summary>
        /// <param name="rowCount">Number of rows to seed.</param>
        /// <returns>
        /// Task indicating the completion of seeding.
        /// </returns>
        Task<bool> SeedDataAsync(int rowCount, IEnumerable<Department> departments, IEnumerable<SalaryRange> salaryRanges);

        /// <summary>
        /// Retrieves a list of Position records based on the provided query parameters.
        /// </summary>
        /// <param name="requestParameters">Parameters for the query.</param>
        /// <returns>
        /// Task containing the list of Position records and the total number of records found.
        /// </returns>    
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPositionReponseAsync(GetPositionsQuery requestParameters);

        /// <summary>
        /// Retrieves a paged list of Position records based on the provided query parameters.
        /// </summary>
        /// <param name="requestParameters">Parameters for the query.</param>
        /// <returns>
        /// Task containing the paged list of Position records and the total number of records found.
        /// </returns>    
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> PagedPositionReponseAsync(PagedPositionsQuery requestParameters);
    }
}