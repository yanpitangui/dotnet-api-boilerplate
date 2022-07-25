﻿using FluentValidation;

namespace Boilerplate.Application.Features.Users.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(254)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(255);
        
    }
}