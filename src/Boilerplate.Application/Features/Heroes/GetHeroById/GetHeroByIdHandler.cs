using Ardalis.Result;
using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Heroes.GetHeroById;

public class GetHeroByIdHandler : IRequestHandler<GetHeroByIdRequest, Result<GetHeroResponse>>
{
    private readonly IContext _context;

    private readonly IMapper _mapper;

    public GetHeroByIdHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<Result<GetHeroResponse>> Handle(GetHeroByIdRequest request, CancellationToken cancellationToken)
    {
        var hero = await _context.Heroes.FirstOrDefaultAsync(x => x.Id == request.Id,
            cancellationToken: cancellationToken);
        if (hero is null) return Result.NotFound();
        return _mapper.Map<GetHeroResponse>(hero);
    }
}