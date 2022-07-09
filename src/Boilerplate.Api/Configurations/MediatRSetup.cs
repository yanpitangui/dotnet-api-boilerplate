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
        services.AddMediatR(typeof(Boilerplate.Application.IAssemblyMarker).GetTypeInfo().Assembly);
        
        services.AddScoped<ValidationErrorHandler>();
        
        services.AddScoped<INotificationHandler<ValidationError>, ValidationErrorHandler>();


        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        return services;
    }
}