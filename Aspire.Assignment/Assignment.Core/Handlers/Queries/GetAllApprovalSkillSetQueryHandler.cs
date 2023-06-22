using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using AutoMapper;
using MediatR;

namespace Assignment.Core.Handlers.Queries
{
    public class GetAllApprovalSkillSetQuery : IRequest<IEnumerable<SkillSetDTO>>

    {
    }

    public class GetAllApprovalSkillSetQueryHandler : IRequestHandler<GetAllApprovalSkillSetQuery, IEnumerable<SkillSetDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllApprovalSkillSetQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SkillSetDTO>> Handle(GetAllApprovalSkillSetQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.SkillSet.GetAllApprovalSkillSet());
            return _mapper.Map<IEnumerable<SkillSetDTO>>(entities);
        }
    }
}
