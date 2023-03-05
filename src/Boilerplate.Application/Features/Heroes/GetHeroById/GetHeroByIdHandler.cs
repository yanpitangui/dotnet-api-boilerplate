using Ardalis.Result;
using Boilerplate.Application.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.GetHeroById;

public class GetHeroByIdHandler : IRequestHandler<GetHeroByIdRequest, Result<GetHeroResponse>>
{
    private readonly IContext _context;


    public GetHeroByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<Result<GetHeroResponse>> Handle(GetHeroByIdRequest request, CancellationToken cancellationToken)
    {
        var hero = await _context.Heroes.FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        if (hero is null) return Result.NotFound();
        return hero.Adapt<GetHeroResponse>();
    }
}