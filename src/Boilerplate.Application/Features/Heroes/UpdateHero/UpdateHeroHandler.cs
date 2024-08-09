using Ardalis.Result;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.UpdateHero;

public class UpdateHeroHandler(IContext context) : IRequestHandler<UpdateHeroRequest, Result<GetHeroResponse>>
{
    public async Task<Result<GetHeroResponse>> Handle(UpdateHeroRequest request,
        CancellationToken cancellationToken)
    {
        Hero? originalHero = await context.Heroes
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalHero == null)
            return Result.NotFound();

        originalHero.Name = request.Name;
        originalHero.Nickname = request.Nickname;
        originalHero.Team = request.Team;
        originalHero.Individuality = request.Individuality;
        originalHero.Age = request.Age;
        originalHero.HeroType = request.HeroType;
        context.Heroes.Update(originalHero);
        await context.SaveChangesAsync(cancellationToken);
        return originalHero.Adapt<GetHeroResponse>();
    }
}