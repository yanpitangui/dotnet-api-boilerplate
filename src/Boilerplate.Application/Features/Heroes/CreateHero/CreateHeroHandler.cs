using AutoMapper;
using Boilerplate.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.CreateHero;

public class CreateHeroHandler : IRequestHandler<CreateHeroRequest, GetHeroResponse>
{
    private readonly IHeroRepository _heroRepository;
    private readonly IMapper _mapper;
    
    
    public CreateHeroHandler(IHeroRepository heroRepository, IMapper mapper)
    {
        _heroRepository = heroRepository;
        _mapper = mapper;
    }

    public async Task<GetHeroResponse> Handle(CreateHeroRequest request, CancellationToken cancellationToken)
    {
        var created = _heroRepository.Create(_mapper.Map<Domain.Entities.Hero>(request));
        await _heroRepository.SaveChangesAsync();
        return _mapper.Map<GetHeroResponse>(created);
    }
}