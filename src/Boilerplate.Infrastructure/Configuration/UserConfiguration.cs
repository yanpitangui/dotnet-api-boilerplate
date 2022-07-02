using System;
using Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email).IsRequired().HasMaxLength(254);
        builder.HasIndex(x => x.Email).IsUnique();



        builder.HasData(
            new User
            {
                Id = new Guid("687d9fd5-2752-4a96-93d5-0f33a49913c6"),
                Email = "admin@boilerplate.com",
                Role = "Admin",
                Password = BC.HashPassword("adminpassword")
            },
            new User
            {
                Id = new Guid("6648c89f-e894-42bb-94f0-8fd1059c86b4"),
                Email = "user@boilerplate.com",
                Role = "User",
                Password = BC.HashPassword("userpassword")
            }
        );
    }
}