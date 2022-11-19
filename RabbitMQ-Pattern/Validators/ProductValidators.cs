using FluentValidation;
using RabbitMQ.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Validators
{
   public class ProductValidators: AbstractValidator<Product>
    {
        public ProductValidators()
        {
            RuleFor(m => m.Id)
                .NotNull().WithMessage("Id can not be null")
                .NotEmpty().WithMessage("Id can not be empty");

            RuleFor(m => m.Name)
                .NotNull().WithMessage("Name can not be null")
                .NotEmpty().WithMessage("Name can not be empty")
                .MaximumLength(50).WithMessage("NAME_MAX_LENTH_ERROR");

            RuleFor(m => m.Price)
                .NotNull()
                .InclusiveBetween(1,500).WithMessage("PRICE_RANGE_ERROR");
        }
    }
}
