using System;

namespace Boilerplate.Application.DTOs.User
{
    public class GetUserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }
    }
}
