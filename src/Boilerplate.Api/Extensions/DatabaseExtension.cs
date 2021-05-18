using Boilerplate.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Api.Extensions
{
    public static class DatabaseExtension
    {
        public static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {

            if (environment?.EnvironmentName == "Testing")
                services.AddDbContextPool<ApplicationDbContext>(o =>
                {
                    o.UseSqlite("Data Source=test.db");
                });
            else
                services.AddDbContextPool<ApplicationDbContext>(o =>
                {
                    o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                    //o.UseInMemoryDatabase(databaseName: "heroesdb");
                });
        }
    }
}
