using MediatR;
using $safeprojectname$.Interfaces.Repositories;
using $safeprojectname$.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace $safeprojectname$.Features.Positions.Commands.CreatePosition
{
    public partial class InsertMockPositionCommand : IRequest<Response<int>>
    {
        public int RowCount { get; set; }
    }

    public class SeedPositionCommandHandler : IRequestHandler<InsertMockPositionCommand, Response<int>>
    {
        private readonly IPositionRepositoryAsync _repository;
        private readonly IDepartmentRepositoryAsync _repositoryDepartment;
        private readonly ISalaryRangeRepositoryAsync _repositorySalaryRange;

        public SeedPositionCommandHandler(IPositionRepositoryAsync repository, IDepartmentRepositoryAsync departmentRepository, ISalaryRangeRepositoryAsync repositorySalaryRange)
        {
            _repository = repository;
            _repositoryDepartment = departmentRepository;
            _repositorySalaryRange = repositorySalaryRange;
            _repositorySalaryRange = repositorySalaryRange;
        }

        public async Task<Response<int>> Handle(InsertMockPositionCommand request, CancellationToken cancellationToken)
        {
            var departments = await _repositoryDepartment.GetAllAsync();
            var salaryRanges = await _repositorySalaryRange.GetAllAsync();
            await _repository.SeedDataAsync(request.RowCount, departments, salaryRanges);

            return new Response<int>(request.RowCount);
        }
    }
}