using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Users.UpdatePassword;

public record UpdatePasswordRequest : IRequest<Result>
{
    [JsonIgnore]
    public UserId Id { get; init; }
    
    public string Password { get; init; } = null!;
}