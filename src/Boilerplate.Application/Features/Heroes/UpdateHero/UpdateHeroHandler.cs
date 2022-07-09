using AutoMapper;
using Boilerplate.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.UpdateHero;

public class UpdateHeroHandler : IRequestHandler<UpdateHeroRequest, GetHeroResponse?>
{
    private readonly IHeroRepository _heroRepository;
    private readonly IMapper _mapper;

    public UpdateHeroHandler(IMapper mapper, IHeroRepository heroRepository)
    {
        _mapper = mapper;
        _heroRepository = heroRepository;
    }

    public async Task<GetHeroResponse?> Handle(UpdateHeroRequest request, CancellationToken cancellationToken)
    {
        var originalHero = await _heroRepository.GetById(request.Id);
        if (originalHero == null) return null;

        originalHero.Name = request.Name;
        originalHero.Nickname = request.Nickname;
        originalHero.Team = request.Team;
        originalHero.Individuality = request.Individuality;
        originalHero.Age = request.Age;
        originalHero.HeroType = request.HeroType;
        _heroRepository.Update(originalHero);
        await _heroRepository.SaveChangesAsync();
        return _mapper.Map<GetHeroResponse>(originalHero);
    }
}