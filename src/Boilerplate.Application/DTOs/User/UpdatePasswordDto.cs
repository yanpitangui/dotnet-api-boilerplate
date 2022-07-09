using Boilerplate.Application.Features.Users;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.DTOs.User;

public record UpdatePasswordDto : IRequest<GetUserResponse?>
{
    [JsonIgnore]
    public Guid Id { get; init; }
    
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
    [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
    [DataType(DataType.Password)]
    public string Password { get; init; }
}