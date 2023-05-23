using Assignment.Contracts.DTO;
using FluentValidation;

namespace Assignment.Core.Validators
{
    public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Provide a brief description about the App");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
            RuleFor(x => x.ExpiryDate).NotEmpty().WithMessage("Expiry Date is required");
            RuleFor(x => x.Name).Matches("[a-zA-Z]$").WithMessage("Please Enter Valid Name");
            RuleFor(x => x.ExpiryDate).Must(ValidateDate).WithMessage("Please Enter Valid Expiry Date");
            RuleFor(x => x.Price).Matches("(^\\d+(\\.\\d{1,2})?$)").WithMessage("Please Enter Valid Price");
        }
        private bool ValidateDate(DateTime date)
        {
            return date > DateTime.Today;
        }
    }
}
