using Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Hero> Heroes { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
