using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Boilerplate.Domain.Auth;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersRequest, PaginatedList<GetUserResponse>>
{
    private readonly IContext _context;
    
    public GetUsersHandler(IContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<GetUserResponse>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = _context.Users
            .WhereIf(!string.IsNullOrEmpty(request.Email), x => EF.Functions.Like(x.Email, $"%{request.Email}%"))
            .WhereIf(request.IsAdmin, x => x.Role == Roles.Admin);
        return await users.ProjectToType<GetUserResponse>().ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}