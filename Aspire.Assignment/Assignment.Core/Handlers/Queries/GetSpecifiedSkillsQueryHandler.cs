using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using AutoMapper;
using MediatR;

namespace Assignment.Core.Handlers.Queries
{
    public class GetSpecifiedSkillsQuery : IRequest<IEnumerable<SkillDTO>>
    {
        public Guid CategoryId { get; }
        public GetSpecifiedSkillsQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }

    public class GetSpecifiedSkillsQueryHandler : IRequestHandler<GetSpecifiedSkillsQuery, IEnumerable<SkillDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetSpecifiedSkillsQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SkillDTO>> Handle(GetSpecifiedSkillsQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Skill.GetAll().Where(s=>s.CategoryId==request.CategoryId));
            return _mapper.Map<IEnumerable<SkillDTO>>(entities);
        }
    }
}
