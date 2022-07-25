using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.GetHeroById;

public class GetHeroByIdHandler : IRequestHandler<GetHeroByIdRequest, GetHeroResponse?>
{
    private readonly IContext _context;

    private readonly IMapper _mapper;

    public GetHeroByIdHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<GetHeroResponse?> Handle(GetHeroByIdRequest request, CancellationToken cancellationToken)
    {
        return _mapper.Map<GetHeroResponse>(await _context.Heroes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken));
    }
}