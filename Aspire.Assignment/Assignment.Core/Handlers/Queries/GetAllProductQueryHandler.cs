using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using AutoMapper;
using MediatR;

namespace Assignment.Core.Handlers.Queries
{
    public class GetAllProductQuery : IRequest<IEnumerable<ProductDTO>>
    {
    }

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProductDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Product.GetAll());
            return _mapper.Map<IEnumerable<ProductDTO>>(entities.Where(p=>p.ExpiryDate>=DateTime.Now));
        }
    }
}
