using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace Boilerplate.Api.Configurations;

public static class CompressionSetup
{
    public static IServiceCollection AddCompressionSetup(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.Configure<BrotliCompressionProviderOptions>(options =>
            options.Level = CompressionLevel.Fastest
        );

        services.Configure<GzipCompressionProviderOptions>(options =>
            options.Level = CompressionLevel.Fastest
        );

        return services;
    }
}