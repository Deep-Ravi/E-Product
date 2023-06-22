using Assignment.Contracts.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assignment.Core.Validators
{
    public class CreateSkillSetDTOValidator : AbstractValidator<CreateSkillSetDTO>
    {
        public CreateSkillSetDTOValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Category Id is required");
            RuleFor(x => x.SkillId).NotEmpty().WithMessage("Skill Id is required");
            RuleFor(x => x.Proficiency).NotEmpty().WithMessage("Proficiency is required");
            RuleFor(x => x.LastWorkedDate).NotEmpty().WithMessage("Last Worked Date is required");
            RuleFor(x => x.YearsOfExperience).NotEmpty().WithMessage("Years of Experience is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name Not found");
            RuleFor(x => x.Proficiency).Must(ValidateProficiency).WithMessage("Please Enter Valid Proficiency");
            RuleFor(x => x.YearsOfExperience).Must(ValidateYearsOfExperience).WithMessage("Please Enter Valid Years Of Experience");
        }

        private bool ValidateProficiency(string proficiency)
        {
            var flag = Regex.Match(proficiency, @"\b(BEGINNER|INTERMEDIATE|ADVANCED|EXPERT)\b");
            if (!flag.Success)
            {
                return false;
            }
            return true;
        }
        private bool ValidateYearsOfExperience(string yearsOfExperience)
        {
            if (yearsOfExperience != ">10")
            { 
                var firstValue = Regex.Match(yearsOfExperience, @"^\b((\.5)|(\d)+(\.5)?)+-+\d+(\.5)?$");
                if (!firstValue.Success)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
