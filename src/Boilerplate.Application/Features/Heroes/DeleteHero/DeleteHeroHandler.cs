using Ardalis.Result;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.DeleteHero;

public class DeleteHeroHandler(IContext context) : IRequestHandler<DeleteHeroRequest, Result>
{
    public async Task<Result> Handle(DeleteHeroRequest request, CancellationToken cancellationToken)
    {
        Hero? hero = await context.Heroes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (hero is null)
        {
            return Result.NotFound();
        }

        context.Heroes.Remove(hero);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}