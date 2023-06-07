using Assignment.Contracts;
using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Assignment.Core.Handlers.Commands
{
    public class UpdateUserCommand : IRequest<int>
    {
        public UserDTO Model { get; }
        public UpdateUserCommand(UserDTO model)
        {
            this.Model = model;
        }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateUserDTO> _validator;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UpdateUserCommandHandler(IUnitOfWork repository, IValidator<CreateUserDTO> validator, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            CreateUserDTO model = _mapper.Map<CreateUserDTO>(request.Model);
            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var userRecords = _repository.User.GetAll();
            var userRecord= userRecords.FirstOrDefault(u => u.Email == request.Model.Email && u.Id != request.Model.Id);
            if (userRecord != null)
            {
                throw new InvalidRequestBodyException
                {
                    Errors = new string[] { $"{request.Model.Email} Already Exists" }
                };
            }

            foreach (var operation in _repository.Operation.GetAll())
            {
                string[] operationsArr = operation.OperationAccess.Split(",");
                var unMatchedRolesParam = model.Operations.Except(operationsArr);
                var unMatchedRolesDB = operationsArr.Except(model.Operations);
                if (unMatchedRolesParam.SequenceEqual(unMatchedRolesDB))
                {
                    request.Model.OperationId = operation.Id;
                    break;
                }
            }

            var entity = _mapper.Map<User>(request.Model);
            var user= userRecords.FirstOrDefault(u => u.Id == request.Model.Id);
            PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(user, user.Password, Constants.TemparoryPassword);
            if (PasswordVerificationResult.Success == passwordResult)
            {
                entity.Password = _passwordHasher.HashPassword(entity, model.Password);
            }
            _repository.User.Update(entity);
            await _repository.CommitAsync();
            return entity.Id;
        }
    }
}
