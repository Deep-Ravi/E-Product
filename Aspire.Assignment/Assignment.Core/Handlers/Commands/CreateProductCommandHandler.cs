using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.Data;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using FluentValidation;
using MediatR;
using AutoMapper;

namespace Assignment.Core.Handlers.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public CreateProductDTO Model { get; }
        public CreateProductCommand(CreateProductDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateProductDTO> _validator;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork repository, IValidator<CreateProductDTO> validator,IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            CreateProductDTO model = request.Model;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = _mapper.Map<Product>(model);
            _repository.Product.Add(entity);
            await _repository.CommitAsync();

            return entity.Id;
        }
    }
}
