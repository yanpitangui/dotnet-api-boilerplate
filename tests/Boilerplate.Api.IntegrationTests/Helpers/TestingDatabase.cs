using Boilerplate.Application.Common;
using System;
using System.Collections.Generic;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Api.IntegrationTests.Helpers;

public static class TestingDatabase
{
    public static async Task SeedDatabase(Func<IContext> contextFactory)
    {
        await using var db = contextFactory();
        await db.Users.ExecuteDeleteAsync();
        db.Heroes.AddRange(GetSeedingHeroes);
        db.Users.AddRange(GetSeedingUsers);
        await db.SaveChangesAsync();
    }

    public static readonly User[] GetSeedingUsers = new[]
    {
        new User()
        {
            Id = new Guid("2e3b7a21-f06e-4c47-b28a-89bdaa3d2a37"),
            Password = BC.HashPassword("testpassword123"),
            Email = "admin@boilerplate.com",
            Role = "Admin"
        },
        new User()
        {
            Id = new Guid("c68acd7b-9054-4dc3-b536-17a1b81fa7a3"),
            Password = BC.HashPassword("testpassword123"),
            Email = "user@boilerplate.com",
            Role = "User"
        }
    };

    public static readonly List<Hero> GetSeedingHeroes =
        new()
        {
            new(){ Id = new Guid("824a7a65-b769-4b70-bccb-91f880b6ddf1"), Name = "Corban Best", HeroType = HeroType.ProHero },
            new() { Id = new Guid("b426070e-ccb3-42e6-8fb4-ef6aa5a62cc4"), Name = "Priya Hull", HeroType = HeroType.Student },
            new() { Id = new Guid("634769f7-a7b8-4146-9cb2-ff2dd90e886b"), Name = "Harrison Vu", HeroType = HeroType.Teacher }
        };
}