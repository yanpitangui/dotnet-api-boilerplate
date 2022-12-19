using Boilerplate.Domain.Entities.Common;
using MassTransit;

namespace Boilerplate.Domain.Entities;

public class User : Entity<UserId>
{
    public override UserId Id { get; set; } = NewId.NextGuid();
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
}