using System;
using Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "admin@boilerplate.com",
                    Role = "Admin",
                    Password = BC.HashPassword("adminpassword")
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "user@boilerplate.com",
                    Role = "User",
                    Password = BC.HashPassword("userpassword")
                }
            );
        }
    }
}
