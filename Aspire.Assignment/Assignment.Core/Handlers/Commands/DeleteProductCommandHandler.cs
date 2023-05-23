using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using MediatR;

namespace Assignment.Core.Handlers.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid productId { get; }
        public DeleteProductCommand(Guid id)
        {
            productId = id;
        }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand,bool>
    {
        private readonly IUnitOfWork _repository;

        public DeleteProductCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _repository.Product.Delete(request.productId);
            await _repository.CommitAsync();
            return true;
        }
    }
}
