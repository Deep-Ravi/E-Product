using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using AutoMapper;
using MediatR;

namespace Assignment.Core.Handlers.Queries
{
    public class GetAllDeveloperSkillSetQuery : IRequest<IEnumerable<SkillSetDTO>>
    {
        public string UserName { get; }
        public GetAllDeveloperSkillSetQuery(string userName)
        {
            UserName = userName;
        }
    }

    public class GetAllDeveloperSkillSetQueryHandler : IRequestHandler<GetAllDeveloperSkillSetQuery, IEnumerable<SkillSetDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllDeveloperSkillSetQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SkillSetDTO>> Handle(GetAllDeveloperSkillSetQuery request, CancellationToken cancellationToken)
        {
            var user = await Task.FromResult(_repository.User.GetAllUsers().FirstOrDefault(x => x.Username ==request.UserName));
            if (user == null) {
                throw new EntityNotFoundException($"User Not found");
            }
            var entities= await Task.FromResult(_repository.SkillSet.GetAllDeveloperSkillSet(user.Id));
            return _mapper.Map<IEnumerable<SkillSetDTO>>(entities);
        }
    }
}
