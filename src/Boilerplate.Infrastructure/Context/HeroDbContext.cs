using Boilerplate.Domain.Entities;
using Boilerplate.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Context
{
    public class HeroDbContext : DbContext
    {
        public HeroDbContext(DbContextOptions<HeroDbContext> options) : base(options) { }

        public DbSet<Hero> Heroes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new System.ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.ApplyConfiguration(new HeroMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
