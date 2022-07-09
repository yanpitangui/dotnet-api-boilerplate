using AutoMapper;
using Boilerplate.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.GetUserById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, GetUserResponse?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<GetUserResponse?> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        return _mapper.Map<GetUserResponse>(await _userRepository.GetById(request.Id));
    }
}