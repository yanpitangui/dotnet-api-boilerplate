using Boilerplate.Domain.Core.Entities;

namespace Boilerplate.Domain.Entities
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

