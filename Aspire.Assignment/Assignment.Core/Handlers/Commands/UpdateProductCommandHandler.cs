using Assignment.Contracts.Data;
using Assignment.Contracts.Data.Entities;
using Assignment.Contracts.DTO;
using Assignment.Core.Exceptions;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Assignment.Core.Handlers.Commands
{
    public class UpdateProductCommand : IRequest<Guid>
    {
        public ProductDTO Model { get; }
        public UpdateProductCommand(ProductDTO model)
        {
            this.Model = model;
        }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateProductDTO> _validator;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork repository, IValidator<CreateProductDTO> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            CreateProductDTO model = _mapper.Map<CreateProductDTO>(request.Model);

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = _mapper.Map<Product>(request.Model);
            _repository.Product.Update(entity);
            await _repository.CommitAsync();
            return entity.Id;
        }
    }
}
