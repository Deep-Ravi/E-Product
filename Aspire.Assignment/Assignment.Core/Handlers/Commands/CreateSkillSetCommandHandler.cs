using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Assignment.Core.Handlers.Commands
{
    public class CreateSkillSetCommand : IRequest<Guid>
    {
        public CreateSkillSetDTO Model { get; }
        public CreateSkillSetCommand(CreateSkillSetDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateSkillSetCommandHandler : IRequestHandler<CreateSkillSetCommand, Guid>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateSkillSetDTO> _validator;
        private readonly IMapper _mapper;

        public CreateSkillSetCommandHandler(IUnitOfWork repository, IValidator<CreateSkillSetDTO> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateSkillSetCommand request, CancellationToken cancellationToken)
        {            
            CreateSkillSetDTO model = request.Model;
            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var user = _repository.User.GetAllUsers().FirstOrDefault(x=>x.Username==request.Model.UserName&&x.Role.Name=="DEVELOPER");
            if (user == null)
            {
                throw new InvalidRequestBodyException
                {
                    Errors = new string[] { $"Username does not exist" }
                };
            }
            var existingRecord = _repository.SkillSet.GetAllDeveloperSkillSet(user.Id).FirstOrDefault(s => s.SkillId == request.Model.SkillId);
            if (existingRecord != null)
            {
                throw new InvalidRequestBodyException
                {
                    Errors = new string[] { $"Skill Already Exists" }
                };
            }
            var skillRecord = _repository.Skill.GetAll().FirstOrDefault(s => s.Id == request.Model.SkillId);
            if (skillRecord==null||skillRecord.CategoryId != request.Model.CategoryId)
            {
                throw new InvalidRequestBodyException
                {
                    Errors = new string[] { $"No Such Skill Type for the Selected Category" }
                };
            }
            var entity = _mapper.Map<SkillSet>(model);
            entity.UserId=user.Id;
            entity.IsSendForApproval = false;
            entity.ApprovalStatus = "NOT SENT";
            _repository.SkillSet.Add(entity);
            await _repository.CommitAsync();

            return entity.Id;
        }
    }
}
