using CVBuilder.Application.User.Commands;
using FluentValidation;

namespace CVBuilder.Application.User.Validators
{
    public class UserEditValidator : AbstractValidator<EditUserCommand>
    {
        public UserEditValidator()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0);
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .Length(2, 25)
                .Matches("[א-תa-zA-Z]{2,25}");
            RuleFor(c => c.LastName)
                .NotEmpty()
                .Length(2, 25)
                .Matches("[א-תa-zA-Z]{2,25}");
            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .Matches("^[0][5][0-9]{1}[-]{0,1}[0-9]{7}$");
            RuleFor(c => c.NotifyByEmailUpdates)
                .NotNull();
            RuleFor(c => c.NotifyByEmailDailySummary)
                .NotNull();
            RuleFor(c => c.NotifyByEmailMarketingInformation)
                .NotNull();
            RuleFor(c => c.NotifyBySmsUpdates)
                .NotNull();
            RuleFor(c => c.NotifyBySmsDailySummary)
                .NotNull();
            RuleFor(c => c.NotifyBySmsMarketingInformation)
                .NotNull();
            RuleFor(c => c.NotifyByWhatsappUpdates)
                .NotNull();
            RuleFor(c => c.NotifyByWhatsappDailySummary)
                .NotNull();
            RuleFor(c => c.NotifyByWhatsappMarketingInformation)
                .NotNull();
        }
    }
}