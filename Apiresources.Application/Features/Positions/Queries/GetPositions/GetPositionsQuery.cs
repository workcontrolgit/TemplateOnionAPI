namespace $safeprojectname$.Features.Positions.Queries.GetPositions
{
    /// <summary>
    /// Definition of GetPositionsQuery class that inherits from QueryParameter and implements IRequest<PagedResponse<IEnumerable<Entity>>>
    /// </summary>
    public class GetPositionsQuery : QueryParameter, IRequest<PagedResponse<IEnumerable<Entity>>>
    {
        /// <summary>
        /// Property to hold the position number as a string.
        /// </summary>
        public string PositionNumber { get; set; }
        
        /// <summary>
        /// Property to hold the position title as a string.
        /// </summary>
        public string PositionTitle { get; set; }
        
        /// <summary>
        /// Property to hold the department as a string.
        /// </summary>
        public string Department { get; set; }
    }

    /// <summary>
    /// Definition of GetAllPositionsQueryHandler class that implements IRequestHandler<GetPositionsQuery, PagedResponse<IEnumerable<Entity>>>
    /// </summary>
    public class GetAllPositionsQueryHandler : IRequestHandler<GetPositionsQuery, PagedResponse<IEnumerable<Entity>>>
    {
        private readonly IPositionRepositoryAsync _repository;
        private readonly IModelHelper _modelHelper;

        /// <summary>
        /// Constructor for GetAllPositionsQueryHandler that takes in an IPositionRepositoryAsync and IModelHelper.
        /// </summary>
        /// <param name="repository">IPositionRepositoryAsync object.</param>
        /// <param name="modelHelper">IModelHelper object.</param>
        public GetAllPositionsQueryHandler(IPositionRepositoryAsync repository, IModelHelper modelHelper)
        {
            _repository = repository;
            _modelHelper = modelHelper;
        }

        /// <summary>
        /// Handle method that takes in a GetPositionsQuery and CancellationToken and returns a PagedResponse<IEnumerable<Entity>>.
        /// </summary>
        /// <param name="request">GetPositionsQuery object.</param>
        /// <param name="cancellationToken">CancellationToken object.</param>
        /// <returns>PagedResponse<IEnumerable<Entity>> object.</returns>
        public async Task<PagedResponse<IEnumerable<Entity>>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            var objRequest = request;
            // verify request fields are valid field and exist in the DTO
            if (!string.IsNullOrEmpty(objRequest.Fields))
            {
                //limit to fields in view model
                objRequest.Fields = _modelHelper.ValidateModelFields<GetPositionsViewModel>(objRequest.Fields);
            }
            if (string.IsNullOrEmpty(objRequest.Fields))
            {
                //default fields from view model
                objRequest.Fields = _modelHelper.GetModelFields<GetPositionsViewModel>();
            }
            // verify orderby a valid field and exist in the DTO GetPositionsViewModel
            if (!string.IsNullOrEmpty(objRequest.OrderBy))
            {
                //limit to fields in view model
                objRequest.OrderBy = _modelHelper.ValidateModelFields<GetPositionsViewModel>(objRequest.OrderBy);
            }

            // query based on filter
            var qryResult = await _repository.GetPositionReponseAsync(objRequest);
            var data = qryResult.data;
            RecordsCount recordCount = qryResult.recordsCount;
            // response wrapper
            return new PagedResponse<IEnumerable<Entity>>(data, objRequest.PageNumber, objRequest.PageSize, recordCount);
        }
    }
}