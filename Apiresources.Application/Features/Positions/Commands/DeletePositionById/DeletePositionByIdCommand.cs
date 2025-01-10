namespace $safeprojectname$.Features.Positions.Commands.DeletePositionById
{
    // Represents a command to delete a position by its ID.
    public class DeletePositionByIdCommand : IRequest<Response<Guid>>
    {
        // The ID of the position to be deleted.
        public Guid Id { get; set; }

        // Represents the handler for deleting a position by its ID.
        public class DeletePositionByIdCommandHandler : IRequestHandler<DeletePositionByIdCommand, Response<Guid>>
        {
            // The repository used to access and manipulate position data.
            private readonly IPositionRepositoryAsync _repository;

            // Constructor that initializes the command handler with the given repository.
            public DeletePositionByIdCommandHandler(IPositionRepositoryAsync repository)
            {
                _repository = repository;
            }

            // Handles the command by deleting the specified position from the repository.
            public async Task<Response<Guid>> Handle(DeletePositionByIdCommand command, CancellationToken cancellationToken)
            {
                // Retrieves the position with the specified ID from the repository.
                var entity = await _repository.GetByIdAsync(command.Id);
                if (entity == null) throw new ApiException($"Position Not Found.");
                // Deletes the retrieved position from the repository.
                await _repository.DeleteAsync(entity);
                // Returns a response indicating the successful deletion of the position.
                return new Response<Guid>(entity.Id);
            }
        }
    }
}