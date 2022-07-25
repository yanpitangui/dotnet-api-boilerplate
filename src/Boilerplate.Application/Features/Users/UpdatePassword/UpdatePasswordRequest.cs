using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Users.UpdatePassword;

public record UpdatePasswordRequest : IRequest<GetUserResponse?>
{
    [JsonIgnore]
    public UserId Id { get; init; }
    
    public string Password { get; init; } = null!;
}