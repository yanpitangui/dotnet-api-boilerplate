using FluentValidation;

namespace Boilerplate.Application.Features.Users.UpdatePassword;

public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordRequest>
{
    public UpdatePasswordValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(255);

    }
}