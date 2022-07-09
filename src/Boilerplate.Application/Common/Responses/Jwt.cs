using System;

namespace Boilerplate.Application.Common.Responses;

public record Jwt
{
    public string Token { get; init; }
    public DateTime ExpDate { get; init; }
}