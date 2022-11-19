using FluentValidation;
using RabbitMQ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ_Pattern.Validators
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Name can not be null")
                .NotEmpty().WithMessage("Name can not be empty")
                .MaximumLength(100).WithMessage("NAME_MAX_LENTH_ERROR");

            RuleFor(c => c.Email)
            .NotEmpty().WithMessage("E-mail addres can not be empty")
            .EmailAddress().WithMessage("E-mail addres not in correct format");

            RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password can not be empty")
            .Matches("[A-Z]").WithMessage("Password must include UPPERCASE letters")
            .Matches("[a-z]").WithMessage("Password must include lowercase letters")
            .Matches("[0-9]").WithMessage("Password must include digits")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must include special chars");

            RuleFor(c => c.Role)
                .NotNull().WithMessage("Role can not be null")
                .NotEmpty().WithMessage("Role can not be empty");
        }
    }
}
