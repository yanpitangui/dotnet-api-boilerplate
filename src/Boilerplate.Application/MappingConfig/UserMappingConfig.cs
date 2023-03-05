using Boilerplate.Application.Features.Users;
using Boilerplate.Application.Features.Users.CreateUser;
using Boilerplate.Domain.Auth;
using Boilerplate.Domain.Entities;
using Mapster;

namespace Boilerplate.Application.MappingConfig;

public class UserMappingConfig : IMappingConfig
{
    public void ApplyConfig()
    {
        TypeAdapterConfig<CreateUserRequest, User>
            .ForType()
            .Map(dest => dest.Role,
                opt => opt.IsAdmin ? Roles.Admin : Roles.User);
        
        TypeAdapterConfig<User, GetUserResponse>
            .ForType()
            .Map(dest => dest.IsAdmin,
                x => x.Role == Roles.Admin);
    }
}