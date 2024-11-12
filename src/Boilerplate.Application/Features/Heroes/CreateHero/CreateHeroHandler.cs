using Ardalis.Result;
using Boilerplate.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.CreateHero;

public class CreateHeroHandler : IRequestHandler<CreateHeroRequest, Result<GetHeroResponse>>
{
    private readonly IContext _context;
    
    
    public CreateHeroHandler(IContext context)
    {
        _context = context;
    }

    public async Task<Result<GetHeroResponse>> Handle(CreateHeroRequest request, CancellationToken cancellationToken)
    {
        var created = Mapper.ToHeroEntity(request);
        _context.Heroes.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return Mapper.ToHeroDto(created);
    }
}