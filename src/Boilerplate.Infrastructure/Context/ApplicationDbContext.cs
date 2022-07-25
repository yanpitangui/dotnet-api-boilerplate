using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Context;

public class ApplicationDbContext : DbContext, IContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Hero> Heroes { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}