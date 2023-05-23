using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using Assignment.Core.Service;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Assignment.Providers.Handlers.Commands
{
    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserDTO Model { get; }
        public CreateUserCommand(CreateUserDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateUserDTO> _validator;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public CreateUserCommandHandler(IUnitOfWork repository, IValidator<CreateUserDTO> validator, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _repository = repository;
            _validator = validator;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            CreateUserDTO model = request.Model;
            int operationId = 0;
            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var record = _repository.User.GetAll().FirstOrDefault(u=>u.Email == request.Model.Email);
            if (record != null)
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
                    operationId = operation.Id;
                    break;
                }
            }

            if(operationId == 0)
            {
                var operationsInfo = new Operation {
                    OperationAccess = string.Join(",", model.Operations)
                };
                _repository.Operation.Add(operationsInfo);
                await _repository.CommitAsync();
                operationId = operationsInfo.Id;
            }

            var entity = new User
            {
                Username = model.Username,
                Email=model.Email,
                RoleId= model.RoleId,
                OperationId= operationId,
                LastPasswordChange= DateTime.Now.AddMinutes(-5),
            };

            if (model.Password == "Guest@945")
            {
                NewUserResetPasswordService.SendPasswordResetMail(entity,_configuration);
                model.Password = DateTime.Now.Year.ToString() + "@E-ProGuest";
            };

            entity.Password= _passwordHasher.HashPassword(entity, model.Password);
            _repository.User.Add(entity);
            await _repository.CommitAsync();

            return entity.Id;
        }
       
    }
}