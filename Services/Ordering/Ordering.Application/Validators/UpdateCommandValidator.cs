﻿using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class UpdateCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(o => o.Id)
                    .NotEmpty()
                    .WithMessage("{Id} can not be empty")
                    .NotNull()
                    .WithMessage("{Id} can not be null");

            RuleFor(o => o.UserName)
                    .NotEmpty()
                    .WithMessage("{UserName} is a required field")
                    .NotNull()
                    .MaximumLength(70)
                    .WithMessage("{UserName} can not exceed 70 characters");

            RuleFor(o => o.TotalPrice)
                    .NotEmpty()
                    .WithMessage("{TotalPrice} is required")
                    .GreaterThan(-1)
                    .WithMessage("{TotalPrice} can not be less than 0");

            RuleFor(o => o.EmailAddress)
                    .NotEmpty()
                    .WithMessage("{EmailAddress} is a required field")
                    .NotNull()
                    .MaximumLength(70)
                    .WithMessage("{EmailAddress} can not exceed 70 characters");

            RuleFor(o => o.FirstName)
                    .NotEmpty()
                    .WithMessage("{FirstName} is a required field")
                    .NotNull()
                    .MaximumLength(70)
                    .WithMessage("{FirstName} can not exceed 70 characters");

            RuleFor(o => o.FirstName)
                    .NotEmpty()
                    .WithMessage("{FirstName} is a required field")
                    .NotNull()
                    .MaximumLength(70)
                    .WithMessage("{FirstName} can not exceed 70 characters");
        }
    }
}
