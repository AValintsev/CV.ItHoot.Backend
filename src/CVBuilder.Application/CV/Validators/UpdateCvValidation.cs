using CVBuilder.Application.CV.Commands;
using FluentValidation;

namespace CVBuilder.Application.CV.Validators
{
    public class UpdateCvValidation : AbstractValidator<UpdateCvCommand>
    {
        public UpdateCvValidation()
        {
            RuleForEach(x => x.Skills).SetValidator(new CreateCvValidation.ValidationSkill());
            RuleForEach(x => x.UserLanguages).SetValidator(new CreateCvValidation.ValidationLanguage());
        }
    }
}