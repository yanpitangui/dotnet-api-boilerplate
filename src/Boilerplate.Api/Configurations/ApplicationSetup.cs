using Boilerplate.Application.Common;
using Boilerplate.Application.MappingConfig;
using Boilerplate.Infrastructure.Context;
using FastExpressionCompiler;
using Mapster;
using MassTransit;
using MassTransit.NewIdProviders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Boilerplate.Api.Configurations;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
    {
        services.AddScoped<IContext, ApplicationDbContext>();
        NewId.SetProcessIdProvider(new CurrentProcessIdProvider());
        ApplyAllMappingConfigFromAssembly();
        TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileFast();
        
        return services;
    }
    
    
    private static IEnumerable<Type> GetTypesWithInterface<TInterface>(Assembly asm) {
        var it = typeof (TInterface);
        return asm.GetTypes().Where(x => it.IsAssignableFrom(x) && x is {IsInterface: false, IsAbstract: false});
    }


    private static void ApplyAllMappingConfigFromAssembly()
    {
        var mappers = GetTypesWithInterface<IMappingConfig>(typeof(IMappingConfig).Assembly);
        foreach (var mapperType in mappers)
        {
            var instance = (IMappingConfig)Activator.CreateInstance(mapperType)!;
            instance.ApplyConfig();
        }
    }
    
}