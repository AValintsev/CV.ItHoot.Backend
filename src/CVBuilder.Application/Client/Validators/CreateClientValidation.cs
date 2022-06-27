﻿using CVBuilder.Application.Client.Commands;
using CVBuilder.Application.Core.Constants;
using CVBuilder.Application.Resume.Commands;
using FluentValidation;

namespace CVBuilder.Application.Resume.Validators
{
    public class CreateClientValidation : AbstractValidator<CreateClientCommand>
    {
		public CreateClientValidation()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty();
            RuleFor(c => c.LastName)
                .NotEmpty()
                .MinimumLength(70);
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(c => c.PhoneNumber)
                .Matches(RegexConstants.PHONE_ALL_REGEX)
                .MinimumLength(7);
            RuleFor(c => c.Site)
                .Matches(RegexConstants.SITE_REGEX);
        }
	}
}