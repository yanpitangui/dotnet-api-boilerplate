using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace Boilerplate.Api.Configurations;

public static class LoggingSetup
{
    public static IHostBuilder UseLoggingSetup(this IHostBuilder host, IConfiguration configuration)
    {
        host.UseSerilog((_, services, lc) =>
        {
            lc.ConfigureBaseLogging(configuration, services);
        });

        return host;
    }

}

public static class LoggerExtension
{
    public static LoggerConfiguration ConfigureBaseLogging(
        this LoggerConfiguration loggerConfiguration, IConfiguration configuration, IServiceProvider services
    )
    {
        loggerConfiguration
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Infrastructure", LogEventLevel.Warning)
            .WriteTo.Async(a =>
            {
                a.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Code);
            })
            .Enrich.FromLogContext();

        return loggerConfiguration;
    }
}