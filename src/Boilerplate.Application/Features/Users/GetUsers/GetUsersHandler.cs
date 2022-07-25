using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Boilerplate.Domain.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersRequest, PaginatedList<GetUserResponse>>
{
    private readonly IContext _context;

    private readonly IMapper _mapper;

    public GetUsersHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<GetUserResponse>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = _context.Users
            .WhereIf(!string.IsNullOrEmpty(request.Email), x => EF.Functions.Like(x.Email, $"%{request.Email}%"))
            .WhereIf(request.IsAdmin, x => x.Role == Roles.Admin);
        return await _mapper.ProjectTo<GetUserResponse>(users).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}