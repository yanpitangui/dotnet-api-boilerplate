using Boilerplate.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.DeleteHero;

public class DeleteHeroHandler : IRequestHandler<DeleteHeroRequest, bool>
{
    private readonly IHeroRepository _heroRepository;
    public DeleteHeroHandler(IHeroRepository heroRepository)
    {
        _heroRepository = heroRepository;
    }
    public async Task<bool> Handle(DeleteHeroRequest request, CancellationToken cancellationToken)
    {
        await _heroRepository.Delete(request.Id);
        return await _heroRepository.SaveChangesAsync() > 0;
    }
}