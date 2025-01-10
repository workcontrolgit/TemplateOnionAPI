namespace $safeprojectname$.Interfaces
{
    /// <summary>
    /// Interface for a generic repository that can handle CRUD operations and more on any entity of type T.
    /// </summary>
    public interface IGenericRepositoryAsync<T> where T : class
    {
        /// <summary>
        /// Retrieves an entity with the given ID from the database asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation and returns the retrieved entity.</returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all entities from the database asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation and returns a collection of all entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Adds a new entity to the database asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation and returns the added entity.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity in the database asynchronously.
        /// </summary>
        /// <param name="entity">The updated entity.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity from the database asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Inserts a collection of entities into the database asynchronously.
        /// </summary>
        /// <param name="entities">The collection of entities to insert.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task BulkInsertAsync(IEnumerable<T> entities);

        /// <summary>
        /// Retrieves a paged response of all entities from the database asynchronously.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <returns>A task that represents the asynchronous operation and returns a collection of entities on the specified page.</returns>
        Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Retrieves a paged response of all entities from the database asynchronously with advanced filtering, sorting, and projection options.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The size of each page.</param>
        /// <param name="orderBy">The field or fields by which to sort the results.</param>
        /// <param name="fields">The fields to include in the projection of the results.</param>
        /// <param name="predicate">An expression representing a filter condition for the results.</param>
        /// <returns>A task that represents the asynchronous operation and returns a collection of entities on the specified page that meet the filter criteria.</returns>
        Task<IEnumerable<T>> GetPagedAdvancedReponseAsync(int pageNumber, int pageSize, string orderBy, string fields, ExpressionStarter<T> predicate);

        /// <summary>
        /// Retrieves all entities from the database asynchronously with advanced sorting and projection options.
        /// </summary>
        /// <param name="orderBy">The field or fields by which to sort the results.</param>
        /// <param name="fields">The fields to include in the projection of the results.</param>
        /// <returns>A task that represents the asynchronous operation and returns a collection of all entities with the specified sorting and projection options.</returns>
        Task<IEnumerable<T>> GetAllShapeAsync(string orderBy, string fields);
    }
}