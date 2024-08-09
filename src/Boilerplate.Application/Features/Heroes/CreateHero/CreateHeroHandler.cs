using Ardalis.Result;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Mapster;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.CreateHero;

public class CreateHeroHandler(IContext context) : IRequestHandler<CreateHeroRequest, Result<GetHeroResponse>>
{
    public async Task<Result<GetHeroResponse>> Handle(CreateHeroRequest request, CancellationToken cancellationToken)
    {
        Hero created = request.Adapt<Hero>();
        context.Heroes.Add(created);
        await context.SaveChangesAsync(cancellationToken);
        return created.Adapt<GetHeroResponse>();
    }
}