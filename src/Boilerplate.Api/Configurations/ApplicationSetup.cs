using Boilerplate.Application.Common;
using Boilerplate.Application.MappingProfiles;
using Boilerplate.Infrastructure.Context;
using MassTransit;
using MassTransit.NewIdProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Api.Configurations;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
    {
        services.AddScoped<IContext, ApplicationDbContext>();
        NewId.SetProcessIdProvider(new CurrentProcessIdProvider());

        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}