using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Application.Features.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, GetUserResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    
    
    public CreateUserHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<GetUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var created = _mapper.Map<User>(request);
        _context.Users.Add(created);
        created.Password = BC.HashPassword(request.Password);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GetUserResponse>(created);
    }
}