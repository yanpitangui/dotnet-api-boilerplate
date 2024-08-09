using Ardalis.Result;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.GetHeroById;

public class GetHeroByIdHandler(IContext context) : IRequestHandler<GetHeroByIdRequest, Result<GetHeroResponse>>
{
    public async Task<Result<GetHeroResponse>> Handle(GetHeroByIdRequest request, CancellationToken cancellationToken)
    {
        Hero? hero = await context.Heroes.FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken);
        if (hero is null)
            return Result.NotFound();

        return hero.Adapt<GetHeroResponse>();
    }
}