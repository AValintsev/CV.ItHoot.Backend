using CVBuilder.Application.CV.Commands;
using FluentValidation;

namespace CVBuilder.Application.CV.Validators
{
    public class ValidationCreateCv: AbstractValidator<CreateCvCommand>
    {
        public ValidationCreateCv()
        {
            RuleForEach(x => x.Skills).SetValidator(new ValidationSkill());
            RuleForEach(x => x.UserLanguages).SetValidator(new ValidationLanguage());
        }

        private class ValidationSkill:AbstractValidator<CVSkill>
        {
            public ValidationSkill()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty().When(x=>x.Id == null).WithMessage("Skill name must be not empty");

            }
        }

        private class ValidationLanguage:AbstractValidator<CVLanguage>
        {
            public ValidationLanguage()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty().When(x=>x.Id == null).WithMessage("Language name must be not empty");

            }
        }
    }
    
}