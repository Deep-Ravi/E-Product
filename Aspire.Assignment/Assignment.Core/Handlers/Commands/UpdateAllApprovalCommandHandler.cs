using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Core.Handlers.Commands
{
    public class UpdateAllApprovalCommand : IRequest<bool>
    {
        public IEnumerable<SkillSetDTO> List { get; }
        public UpdateAllApprovalCommand(IEnumerable<SkillSetDTO> list)
        {
            this.List = list;
        }
    }

    public class UpdateAllApprovalCommandHandler : IRequestHandler<UpdateAllApprovalCommand, bool>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public UpdateAllApprovalCommandHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateAllApprovalCommand request, CancellationToken cancellationToken)
        {
            var entities=_mapper.Map<IEnumerable<SkillSet>>(request.List);
            _repository.SkillSet.UpdateRange(entities);
            await _repository.CommitAsync();
            return true;
        }
    }
}
