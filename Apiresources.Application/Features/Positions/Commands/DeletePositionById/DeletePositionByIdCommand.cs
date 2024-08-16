namespace $safeprojectname$.Features.Positions.Commands.DeletePositionById
{
    public class DeletePositionByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class DeletePositionByIdCommandHandler : IRequestHandler<DeletePositionByIdCommand, Response<Guid>>
        {
            private readonly IPositionRepositoryAsync _repository;

            public DeletePositionByIdCommandHandler(IPositionRepositoryAsync repository)
            {
                _repository = repository;
            }

            public async Task<Response<Guid>> Handle(DeletePositionByIdCommand command, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetByIdAsync(command.Id);
                if (entity == null) throw new ApiException($"Position Not Found.");
                await _repository.DeleteAsync(entity);
                return new Response<Guid>(entity.Id);
            }
        }
    }
}