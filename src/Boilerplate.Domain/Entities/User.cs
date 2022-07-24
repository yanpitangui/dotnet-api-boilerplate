using Boilerplate.Domain.Core.Entities;

namespace Boilerplate.Domain.Entities;

public class User : Entity
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
}