using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Behaviors;
using Boilerplate.Application.Common.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Boilerplate.Api.Configurations;

public static class MediatRSetup
{
    public static IServiceCollection AddMediatRSetup(this IServiceCollection services)
    {
        services.AddMediatR((config) =>
        {
            config.RegisterServicesFromAssemblyContaining(typeof(Boilerplate.Application.IAssemblyMarker));
        });

        services.AddScoped<INotificationHandler<ValidationError>, ValidationErrorHandler>();


        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        return services;
    }
}