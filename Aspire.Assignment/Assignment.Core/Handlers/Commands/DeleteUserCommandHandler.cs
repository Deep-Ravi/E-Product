using Assignment.Contracts.Data;
using MediatR;

namespace Assignment.Core.Handlers.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int userId { get; }
        public DeleteUserCommand(int id)
        {
            userId = id;
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUnitOfWork _repository;

        public DeleteUserCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _repository.User.Delete(request.userId);
            await _repository.CommitAsync();
            return true;
        }
    }
}
