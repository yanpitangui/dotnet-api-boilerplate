using AutoMapper;
using Boilerplate.Application.DTOs.User;
using Boilerplate.Application.Features.Users;
using Boilerplate.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Application.Features.Auth.UpdatePassword;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordDto, GetUserResponse?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdatePasswordHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }


    public async Task<GetUserResponse?> Handle(UpdatePasswordDto request, CancellationToken cancellationToken)
    {
        var originalUser = await _userRepository.GetById(request.Id);
        if (originalUser == null) return null;

        originalUser.Password = BC.HashPassword(request.Password);
        _userRepository.Update(originalUser);
        await _userRepository.SaveChangesAsync();
        return _mapper.Map<GetUserResponse>(originalUser);
    }
}