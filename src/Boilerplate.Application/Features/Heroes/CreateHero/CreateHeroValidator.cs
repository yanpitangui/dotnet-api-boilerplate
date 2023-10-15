using Boilerplate.Application.Common;
using FluentValidation;

namespace Boilerplate.Application.Features.Heroes.CreateHero;

public class CreateHeroValidator : AbstractValidator<CreateHeroRequest>
{
    public CreateHeroValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.Individuality)
            .NotEmpty()
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.HeroType)
            .IsInEnum();

        RuleFor(x => x.Age)
            .GreaterThan(0);

        RuleFor(x => x.Nickname)
            .MaximumLength(StringSizes.Max);

        RuleFor(x => x.Team)
            .MaximumLength(StringSizes.Max);
    }
}