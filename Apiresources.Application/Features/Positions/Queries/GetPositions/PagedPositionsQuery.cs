namespace $safeprojectname$.Features.Positions.Queries.GetPositions
{
    public partial class PagedPositionsQuery : QueryParameter, IRequest<PagedDataTableResponse<IEnumerable<Entity>>>
    {

        //strong type input parameters
        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        public int Draw { get; set; } //page number
        /// <summary>
        /// Gets or sets the starting record for paging.
        /// </summary>
        public int Start { get; set; } //Paging first record indicator. This is the start point in the current data set (0 index based - i.e. 0 is the first record).

        /// <summary>
        /// Gets or sets the number of records per page for paging.
        /// </summary>
        public int Length { get; set; } //page size

        /// <summary>
        /// Gets or sets the sorting order and direction for each column.
        /// </summary>
        public IList<SortOrder> Order { get; set; } //Order by

        /// <summary>
        /// Gets or sets the search criteria to filter the data.
        /// </summary>
        public Search Search { get; set; } //search criteria

        /// <summary>
        /// Gets or sets the list of columns to select in the query.
        /// </summary>
        public IList<Column> Columns { get; set; } //select fields
    }

    public class PagePositionQueryHandler : IRequestHandler<PagedPositionsQuery, PagedDataTableResponse<IEnumerable<Entity>>>
    {
        private readonly IPositionRepositoryAsync _repository;
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
                    objRequest.OrderBy = colOrder.Dir == "asc" ? nameof(Entity.PositionNumber) : $"{nameof(Entity.PositionNumber)} DESC";
                    break;

                case 1:
                    objRequest.OrderBy = colOrder.Dir == "asc" ? nameof(Entity.PositionTitle) : $"{nameof(Entity.PositionTitle)} DESC";
                    break;

                case 2:
                    objRequest.OrderBy = colOrder.Dir == "asc" ? $"{nameof(Entity.Department.Name)}" : $"{nameof(Entity.Department.Name)} DESC";
                    break;
            }

            if (string.IsNullOrEmpty(objRequest.Fields))
            {
                //default fields from view model
                objRequest.Fields = _modelHelper.GetModelFields<GetPositionsViewModel>();
            }
            // query based on filter
            var qryResult = await _repository.PagedPositionReponseAsync(objRequest);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;

            // response wrapper
            return new PagedDataTableResponse<IEnumerable<Entity>>(data, request.Draw, recordCount);
        }
    }
}