namespace $safeprojectname$.Features.Employees.Queries.GetEmployees
{
    public partial class PagedEmployeesQuery : QueryParameter, IRequest<PagedDataTableResponse<IEnumerable<Entity>>>
    {
        //strong type input parameters
        public int Draw { get; set; } //page number

        public int Start { get; set; } //Paging first record indicator. This is the start point in the current data set (0 index based - i.e. 0 is the first record).
        public int Length { get; set; } //page size
        public IList<SortOrder> Order { get; set; } //Order by
        public Search Search { get; set; } //search criteria
        public IList<Column> Columns { get; set; } //select fields
    }

    public class PageEmployeeQueryHandler : IRequestHandler<PagedEmployeesQuery, PagedDataTableResponse<IEnumerable<Entity>>>
    {
        private readonly IEmployeeRepositoryAsync _repository;
        private readonly IModelHelper _modelHelper;

        /// <summary>
        /// Constructor for PageEmployeeQueryHandler class.
        /// </summary>
        /// <param name="repository">IEmployeeRepositoryAsync object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        /// <returns>
        /// PageEmployeeQueryHandler object.
        /// </returns>
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

            // Draw map to PageNumber
            objRequest.PageNumber = (request.Start / request.Length) + 1;
            // Length map to PageSize
            objRequest.PageSize = request.Length;

            // Map order > OrderBy
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

            if (string.IsNullOrEmpty(objRequest.Fields))
            {
                //default fields from view model
                objRequest.Fields = _modelHelper.GetModelFields<GetEmployeesViewModel>();
            }
            // query based on filter
            var qryResult = await _repository.GetPagedEmployeeResponseAsync(objRequest);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;

            // response wrapper
            return new PagedDataTableResponse<IEnumerable<Entity>>(data, request.Draw, recordCount);
        }
    }
}