using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using AutoMapper;
using MediatR;

namespace Assignment.Core.Handlers.Queries
{
    public class GetNotifyNewSkillSetCountQuery : IRequest<int>

    {
    }

    public class GetNotifyNewSkillSetCountQueryHandler : IRequestHandler<GetNotifyNewSkillSetCountQuery, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetNotifyNewSkillSetCountQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetNotifyNewSkillSetCountQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<SkillSet> entities = await Task.FromResult(_repository.SkillSet.GetNotifyNewSkillSet());
            var count=entities.Count();
            entities.ToList().ForEach(entity => entity.IsNotified = true);
            _repository.SkillSet.UpdateRange(entities);
            await _repository.CommitAsync();
            return count;
        }
    }
}
