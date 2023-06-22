using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using AutoMapper;
using MediatR;

namespace Assignment.Core.Handlers.Queries
{
    public class GetAllCategoryQuery : IRequest<IEnumerable<CategoryDTO>>
    {
    }

    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<CategoryDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllCategoryQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Category.GetAll());
            return _mapper.Map<IEnumerable<CategoryDTO>>(entities);
        }
    }
}
