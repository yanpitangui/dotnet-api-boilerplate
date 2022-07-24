using AutoMapper;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Boilerplate.Domain.Auth;
using Boilerplate.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.GetUsers;

public class GetUsersHandler : IRequestHandler<GetUsersRequest, PaginatedList<GetUserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetUserResponse>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = _userRepository
            .GetAll()
            .WhereIf(!string.IsNullOrEmpty(request.Email), x => EF.Functions.Like(x.Email, $"%{request.Email}%"))
            .WhereIf(request.IsAdmin, x => x.Role == Roles.Admin);
        return await _mapper.ProjectTo<GetUserResponse>(users).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}