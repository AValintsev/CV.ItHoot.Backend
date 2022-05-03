using CVBuilder.Application.CV.Commands;
using CVBuilder.Application.CV.Commands.SharedCommands;
using FluentValidation;

namespace CVBuilder.Application.CV.Validators
{
    public class CreateCvValidation: AbstractValidator<CreateCvCommand>
    {
        public CreateCvValidation()
        {
            RuleForEach(x => x.Skills).SetValidator(new ValidationSkill());
            RuleForEach(x => x.UserLanguages).SetValidator(new ValidationLanguage());
        }

        public class ValidationSkill:AbstractValidator<SkillCommand>
        {
            public ValidationSkill()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty().When(x=>x.Id == null).WithMessage("Skill name must be not empty");

            }
        }

        public class ValidationLanguage:AbstractValidator<UserLanguageCommand>
        {
            public ValidationLanguage()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty().When(x=>x.Id == null).WithMessage("Language name must be not empty");

            }
        }
    }
    
}