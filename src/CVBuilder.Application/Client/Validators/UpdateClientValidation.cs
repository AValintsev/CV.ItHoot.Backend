using CVBuilder.Application.Client.Commands;
using CVBuilder.Application.Core.Constants;
using FluentValidation;

namespace CVBuilder.Application.Resume.Validators
{
    public class UpdateClientValidation : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientValidation()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty();
            RuleFor(c => c.LastName)
                .MinimumLength(70)
                .NotEmpty();
            RuleFor(c => c.PhoneNumber)
                .Matches(RegexConstants.PHONE_ALL_REGEX)
                .MinimumLength(7);
            RuleFor(c => c.Site)
                .Matches(RegexConstants.SITE_REGEX);
        }
    }
}