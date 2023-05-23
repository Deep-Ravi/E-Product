using Assignment.Contracts.DTO;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Assignment.Core.Validators
{
    public class CreateuserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateuserDTOValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Provide passsword");
            RuleFor(x=>x.Email).EmailAddress().Length(10, 50).WithMessage("Please Enter Valid Email ID");
            RuleFor(x=>x.Operations).Must(ValidateOperations).WithMessage("Please Enter Valid Operations");
        }
        private bool ValidateOperations(string[] operations)
        {
            bool value=true;
            foreach (var operation in operations)
            {
                var flag= Regex.Match(operation, @"\b(ADD|EDIT|VIEW|DELETE)\b");
                if (!flag.Success)
                {
                    value=false;
                    break;
                }
            }
            return value;
        }
    }
}
