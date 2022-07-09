using AutoMapper;
using Boilerplate.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.GetHeroById;

public class GetHeroByIdHandler : IRequestHandler<GetHeroByIdRequest, GetHeroResponse?>
{
    private readonly IHeroRepository _heroRepository;

    private readonly IMapper _mapper;

    public GetHeroByIdHandler(IMapper mapper, IHeroRepository heroRepository)
    {
        _mapper = mapper;
        _heroRepository = heroRepository;
    }
    public async Task<GetHeroResponse?> Handle(GetHeroByIdRequest request, CancellationToken cancellationToken)
    {
        return _mapper.Map<GetHeroResponse>(await _heroRepository.GetById(request.Id));
    }
}