using Boilerplate.Application.MappingProfiles;
using Boilerplate.Domain.Repositories;
using Boilerplate.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Api.Configurations;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
    {
        
        services.AddScoped<IHeroRepository, HeroRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}