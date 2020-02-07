using Boilerplate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Api.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddHeroDbContext(this IServiceCollection services)
        {

            services.AddDbContextPool<HeroDbContext>(o =>
            {
                o.UseInMemoryDatabase(databaseName: "heroesdb");
            });

            return services;
        }
    }
}
