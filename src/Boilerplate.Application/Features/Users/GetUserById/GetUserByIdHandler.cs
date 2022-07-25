using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserResponse?>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<GetUserResponse?> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        return _mapper.Map<GetUserResponse>(await _context.Users
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken));
    }
}