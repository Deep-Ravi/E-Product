using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Assignment.Core.Handlers.Commands
{
    public class UpdateManagerApprovalCommand : IRequest<Guid>
    {
        public SkillSetDTO Model { get; }
        public UpdateManagerApprovalCommand(SkillSetDTO model)
        {
            this.Model = model;
        }
    }

    public class UpdateManagerApprovalCommandHandler : IRequestHandler<UpdateManagerApprovalCommand, Guid>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<SkillSetDTO> _validator;
        private readonly IMapper _mapper;

        public UpdateManagerApprovalCommandHandler(IUnitOfWork repository, IValidator<SkillSetDTO> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateManagerApprovalCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<SkillSet>(request.Model);
            _repository.SkillSet.Update(entity);
            await _repository.CommitAsync();
            return entity.Id;
        }
    }
}
