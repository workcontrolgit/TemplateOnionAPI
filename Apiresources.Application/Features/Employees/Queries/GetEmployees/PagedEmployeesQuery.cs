namespace $safeprojectname$.Features.Employees.Queries.GetEmployees
{
    // This class represents a query for paginated employee data.
    public partial class PagedEmployeesQuery : QueryParameter, IRequest<PagedDataTableResponse<IEnumerable<Entity>>>
    {
        // The page number to retrieve. 0-indexed.
        public int Draw { get; set; }

        // The index of the first record to retrieve in the current data set.
        public int Start { get; set; }

        // The number of records to retrieve per page.
        public int Length { get; set; }

        // The order in which to sort the retrieved records.
        public IList<SortOrder> Order { get; set; }

        // The search criteria for filtering the retrieved records.
        public Search Search { get; set; }

        // The fields to include in the retrieved records.
        public IList<Column> Columns { get; set; }
    }

    // This class handles requests for paginated employee data.
    public class PageEmployeeQueryHandler : IRequestHandler<PagedEmployeesQuery, PagedDataTableResponse<IEnumerable<Entity>>>
    {
        // The repository used to retrieve employee data.
        private readonly IEmployeeRepositoryAsync _repository;

        // Helper methods for working with models.
        private readonly IModelHelper _modelHelper;

        /// <summary>
        /// Constructor for PageEmployeeQueryHandler class.
        /// </summary>
        /// <param name="repository">IEmployeeRepositoryAsync object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        public PageEmployeeQueryHandler(IEmployeeRepositoryAsync repository, IModelHelper modelHelper)
        {
            _repository = repository;
            _modelHelper = modelHelper;
        }

        /// <summary>
        /// Handles the PagedEmployeesQuery request and returns a PagedDataTableResponse.
        /// </summary>
        /// <param name="request">The PagedEmployeesQuery request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A PagedDataTableResponse.</returns>
        public async Task<PagedDataTableResponse<IEnumerable<Entity>>> Handle(PagedEmployeesQuery request, CancellationToken cancellationToken)
        {
            var objRequest = request;

            // Convert the page number and page size from the query parameters.
            objRequest.PageNumber = (request.Start / request.Length) + 1;
            objRequest.PageSize = request.Length;

            // Convert the order parameter to an ORDER BY clause.
            var colOrder = request.Order[0];
            switch (colOrder.Column)
            {
                case 0:
                    objRequest.OrderBy = colOrder.Dir == "asc" ? "LastName" : "LastName DESC";
                    break;

                case 1:
                    objRequest.OrderBy = colOrder.Dir == "asc" ? "FirstName" : "FirstName DESC";
                    break;

                case 2:
                    objRequest.OrderBy = colOrder.Dir == "asc" ? "Email" : "Email DESC";
                    break;

                case 3:
                    objRequest.OrderBy = colOrder.Dir == "asc" ? "EmployeeNumber" : "EmployeeNumber DESC";
                    break;

                case 4:
                    objRequest.OrderBy = colOrder.Dir == "asc" ? "Position.PositionTitle" : "Position.PositionTitle DESC";
                    break;
            }

            // If no fields were specified, use the default fields from the view model.
            if (string.IsNullOrEmpty(objRequest.Fields))
            {
                objRequest.Fields = _modelHelper.GetModelFields<GetEmployeesViewModel>();
            }

            // Retrieve the paginated employee data based on the query parameters.
            var qryResult = await _repository.GetPagedEmployeeResponseAsync(objRequest);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;

            // Return a PagedDataTableResponse containing the retrieved data.
            return new PagedDataTableResponse<IEnumerable<Entity>>(data, request.Draw, recordCount);
        }
    }
}