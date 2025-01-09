namespace $safeprojectname$.Features.Positions.Queries.GetPositionById
{
    // Represents a query to get a position by its ID
    public class GetPositionByIdQuery : IRequest<Response<Position>>
    {
        // The ID of the position to retrieve
        public Guid Id { get; set; }

        // Handles the GetPositionByIdQuery request and retrieves the corresponding Position entity from the repository.
        public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, Response<Position>>
        {
            private readonly IPositionRepositoryAsync _repository;

            // Constructor that initializes the repository
            public GetPositionByIdQueryHandler(IPositionRepositoryAsync repository)
            {
                _repository = repository;
            }
            {
                _repository = repository;
            }
                var entity = await _repository.GetByIdAsync(query.Id);
            // Handles the request and returns a response containing the Position entity if it exists, otherwise throws an ApiException.
            public async Task<Response<Position>> Handle(GetPositionByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(query.Id);
                if (entity == null) throw new ApiException($"Position Not Found.");
                return new Response<Position>(entity);
            }
