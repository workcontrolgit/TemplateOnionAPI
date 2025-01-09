namespace $safeprojectname$.Features.Positions.Commands.CreatePosition
{
    // Represents a command to insert mock position data into the database
    public partial class InsertMockPositionCommand : IRequest<Response<int>>
    {
        // The number of rows to insert into the database
        public int RowCount { get; set; }
    }

    // Handles requests for inserting mock position data
    public class SeedPositionCommandHandler : IRequestHandler<InsertMockPositionCommand, Response<int>>
    {
        // Repository used for interacting with position data in the database
        private readonly IPositionRepositoryAsync _repository;

        // Repository used for interacting with department data in the database
        private readonly IDepartmentRepositoryAsync _repositoryDepartment;

        // Repository used for interacting with salary range data in the database
        private readonly ISalaryRangeRepositoryAsync _repositorySalaryRange;

        // Constructor for SeedPositionCommandHandler, which injects the necessary repositories
        public SeedPositionCommandHandler(IPositionRepositoryAsync repository, IDepartmentRepositoryAsync departmentRepository, ISalaryRangeRepositoryAsync repositorySalaryRange)
        {
            _repository = repository;
            _repositoryDepartment = departmentRepository;
            _repositorySalaryRange = repositorySalaryRange;
            // Duplicate assignment to _repositorySalaryRange, likely a bug
            _repositorySalaryRange = repositorySalaryRange;
        }

        // Handle method for processing the InsertMockPositionCommand request
        public async Task<Response<int>> Handle(InsertMockPositionCommand request, CancellationToken cancellationToken)
        {
            // Get all departments from the database
            var departments = await _repositoryDepartment.GetAllAsync();

            // Get all salary ranges from the database
            var salaryRanges = await _repositorySalaryRange.GetAllAsync();

            // Seed the position data into the database with the specified number of rows
            await _repository.SeedDataAsync(request.RowCount, departments, salaryRanges);

            // Return a response containing the number of rows inserted
            return new Response<int>(request.RowCount);
        }
    }
}