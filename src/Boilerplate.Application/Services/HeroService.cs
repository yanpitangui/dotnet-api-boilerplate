using System;
using System.Threading.Tasks;
using AutoMapper;
using Boilerplate.Application.Features.Hero;
using Boilerplate.Application.Features.Hero.UpdateHero;
using Boilerplate.Application.Interfaces;
using Boilerplate.Domain.Repositories;

namespace Boilerplate.Application.Services;

public class HeroService : IHeroService
{
    private readonly IHeroRepository _heroRepository;

    private readonly IMapper _mapper;

    public HeroService(IMapper mapper, IHeroRepository heroRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _heroRepository = heroRepository ?? throw new ArgumentNullException(nameof(heroRepository));
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) _heroRepository.Dispose();
    }

    #region Hero Methods
    
    public async Task<GetHeroResponse> UpdateHero(Guid id, UpdateHeroRequest updatedHero)
    {

    }

    #endregion
}