using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Repositories;
using Boilerplate.Infrastructure.Context;
namespace Boilerplate.Infrastructure.Repositories
{
    public class HeroRepository : Repository<Hero>, IHeroRepository
    {
        public HeroRepository(HeroDbContext dbContext) : base(dbContext)
        {
        }
    }
}
