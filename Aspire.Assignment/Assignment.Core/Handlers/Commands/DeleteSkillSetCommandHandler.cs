using Assignment.Contracts.Data;
using MediatR;

namespace Assignment.Core.Handlers.Commands
{
    public class DeleteSkillSetCommand : IRequest<bool>
    {
        public Guid skillSetId { get; }
        public DeleteSkillSetCommand(Guid id)
        {
            skillSetId = id;
        }
    }

    public class DeleteSkillSetCommandHandler : IRequestHandler<DeleteSkillSetCommand, bool>
    {
        private readonly IUnitOfWork _repository;

        public DeleteSkillSetCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteSkillSetCommand request, CancellationToken cancellationToken)
        {
            _repository.SkillSet.Delete(request.skillSetId);
            await _repository.CommitAsync();
            return true;
        }
    }
}
