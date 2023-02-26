using Ardalis.Result;
using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.CreateHero;

public class CreateHeroHandler : IRequestHandler<CreateHeroRequest, Result<GetHeroResponse>>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    
    
    public CreateHeroHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<GetHeroResponse>> Handle(CreateHeroRequest request, CancellationToken cancellationToken)
    {
        var created = _mapper.Map<Domain.Entities.Hero>(request);
        _context.Heroes.Add(created);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GetHeroResponse>(created);
    }
}