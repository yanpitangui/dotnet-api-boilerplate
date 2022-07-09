using Boilerplate.Application.Common.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Application.Features.Auth.Authenticate;

public record AuthenticateRequest : IRequest<Jwt?>
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; init; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
    public string Password { get; init; }
}