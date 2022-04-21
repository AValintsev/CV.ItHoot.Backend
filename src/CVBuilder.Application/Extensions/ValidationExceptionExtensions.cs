﻿using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace CVBuilder.Application.Extensions
{
    public static class ValidationException
    {
        public static FluentValidation.ValidationException Build(string propertyName, string message)
        {

            return new(new List<ValidationFailure>
            {
                new(propertyName, message)
            });
        }
        
        public static FluentValidation.ValidationException Build(string propertyName, IEnumerable<IdentityError> messages)
        {
            var errors = messages
                .Select(r => new ValidationFailure(propertyName, r.ToString()))
                .ToList();

            return new FluentValidation.ValidationException(errors);
        }
    }
}
