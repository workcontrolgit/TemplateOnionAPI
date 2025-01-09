namespace $safeprojectname$.Features.Positions.Commands.CreatePosition
{
    // This class represents a command to create a new position.
    public partial class CreatePositionCommand : IRequest<Response<Guid>>
    {
        // The title of the position being created.
        public string PositionTitle { get; set; }
        
        // A unique number assigned to the position being created.
        public string PositionNumber { get; set; }
        
        // A description of the position being created.
        public string PositionDescription { get; set; }
        
        // The ID of the department that the position belongs to.
        public Guid DepartmentId { get; set; }
        
        // The ID of the salary range associated with the position being created.
        public Guid SalaryRangeId { get; set; }

    }

    // This class handles the logic for creating a new position.
    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, Response<Guid>>
    {
        // A repository to interact with the database for positions.
        private readonly IPositionRepositoryAsync _repository;
        
        // An object mapper to convert between different data types.
        private readonly IMapper _mapper;

        // Constructor that injects the position repository and mapper into the handler.
        public CreatePositionCommandHandler(IPositionRepositoryAsync repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // This method is called when a new position creation command is issued.
        public async Task<Response<Guid>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            // Maps the incoming command to a Position object using the mapper.
            var position = _mapper.Map<Position>(request);
            
            // Adds the new position to the database asynchronously.
            await _repository.AddAsync(position);
            
            // Returns a response containing the ID of the newly created position.
            return new Response<Guid>(position.Id);
        }
    }
}