using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Users.UpdatePassword;

public record UpdatePasswordRequest : IRequest<GetUserResponse?>
{
    [JsonIgnore]
    public Guid Id { get; init; }
    
    public string Password { get; init; } = null!;
}