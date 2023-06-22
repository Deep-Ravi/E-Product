using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Assignment.Core.Handlers.Commands
{
    public class UpdateSkillSetCommand : IRequest<Guid>
    {
        public SkillSetDTO Model { get; }
        public UpdateSkillSetCommand(SkillSetDTO model)
        {
            this.Model = model;
        }
    }

    public class UpdateSkillSetCommandHandler : IRequestHandler<UpdateSkillSetCommand, Guid>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<SkillSetDTO> _validator;
        private readonly IMapper _mapper;

        public UpdateSkillSetCommandHandler(IUnitOfWork repository, IValidator<SkillSetDTO> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateSkillSetCommand request, CancellationToken cancellationToken)
        {
            var existingRecord = _repository.SkillSet.GetAllDeveloperSkillSet(request.Model.UserId).FirstOrDefault(s => s.SkillId == request.Model.SkillId);
            if (existingRecord != null)
            {
                throw new InvalidRequestBodyException
                {
                    Errors = new string[] { $"Skill Already Exists" }
                };
            }
            var result = _validator.Validate(request.Model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }
            var skillRecord = _repository.Skill.GetAll().FirstOrDefault(s => s.Id == request.Model.SkillId);
            if (skillRecord == null || skillRecord.CategoryId != request.Model.CategoryId)
            {
                throw new InvalidRequestBodyException
                {
                    Errors = new string[] { $"No Such Skill Type for the Selected Category" }
                };
            }
            var entity = _mapper.Map<SkillSet>(request.Model);
            _repository.SkillSet.Update(entity);
            await _repository.CommitAsync();
            return entity.Id;
        }
    }
}
