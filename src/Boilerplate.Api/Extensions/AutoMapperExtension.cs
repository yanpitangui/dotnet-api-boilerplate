using AutoMapper;
using Boilerplate.Api.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Boilerplate.Api.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
