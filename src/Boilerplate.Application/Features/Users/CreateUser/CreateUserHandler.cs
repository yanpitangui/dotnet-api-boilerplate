using AutoMapper;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Application.Features.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, GetUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    
    public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<GetUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var created = _userRepository.Create(_mapper.Map<User>(request));
        created.Password = BC.HashPassword(request.Password);
        await _userRepository.SaveChangesAsync();
        return _mapper.Map<GetUserResponse>(created);
    }
}