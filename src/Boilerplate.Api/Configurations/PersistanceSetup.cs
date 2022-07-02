using Boilerplate.Application.Auth;
using Boilerplate.Domain.Auth.Interfaces;
using Boilerplate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Api.Configurations;

public static class PersistanceSetup
{
    public static IServiceCollection AddPersistenceSetup(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<ISession, Session>();
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}