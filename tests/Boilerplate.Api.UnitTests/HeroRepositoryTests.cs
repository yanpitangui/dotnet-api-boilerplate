using Boilerplate.Domain.Entities;
using Boilerplate.Infrastructure.Context;
using Boilerplate.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Boilerplate.Api.UnitTests
{
    public class HeroRepositoryTests
    {
        public HeroDbContext HeroContext { get; set; }

        [Fact]
        public async Task Create_Hero()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<HeroDbContext>()
                .UseInMemoryDatabase("Create_Hero")
                .Options;
            int result;


            // Act
            var hero = new Hero();
            using (var context = new HeroDbContext(options))
            {
                var repository = new HeroRepository(context);
                repository.Create(hero);
                result = await repository.SaveChangesAsync();
            }


            // Assert
            Assert.Equal(1, result);
            // Simulate access from another context to verifiy that correct data was saved to database
            using (var context = new HeroDbContext(options))
            {
                Assert.Equal(1, await context.Heroes.CountAsync());
                Assert.Equal(hero, await context.Heroes.FirstAsync());
            }
        }
    }
}
