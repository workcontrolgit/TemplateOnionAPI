namespace $safeprojectname$.Features.Positions.Commands.UpdatePosition
{
    public class UpdatePositionCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
        public string PositionTitle { get; set; }
        public string PositionNumber { get; set; }
        public string PositionDescription { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid SalaryRangeId { get; set; }

        public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, Response<Guid>>
        {
            private readonly IPositionRepositoryAsync _repository;

            public UpdatePositionCommandHandler(IPositionRepositoryAsync positionRepository)
            {
                _repository = positionRepository;
            }

            public async Task<Response<Guid>> Handle(UpdatePositionCommand command, CancellationToken cancellationToken)
            {
                var position = await _repository.GetByIdAsync(command.Id);

                if (position == null)
                {
                    throw new ApiException($"Position Not Found.");
                }
                else
                {
                    position.PositionTitle = command.PositionTitle;
                    position.PositionDescription = command.PositionDescription;

                    await _repository.UpdateAsync(position);

                    return new Response<Guid>(position.Id);
                }
            }
        }
    }
}