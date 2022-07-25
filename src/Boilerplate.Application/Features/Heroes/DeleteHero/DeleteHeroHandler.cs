using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.DeleteHero;

public class DeleteHeroHandler : IRequestHandler<DeleteHeroRequest, bool>
{
    private readonly IContext _context;
    public DeleteHeroHandler(IContext context)
    {
        _context = context;
    }
    public async Task<bool> Handle(DeleteHeroRequest request, CancellationToken cancellationToken)
    {
        var hero = await _context.Heroes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        _context.Heroes.Remove(hero!);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}