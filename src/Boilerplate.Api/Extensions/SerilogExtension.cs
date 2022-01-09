using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.SystemConsole.Themes;

namespace Boilerplate.Api.Extensions
{
    public static class SerilogExtension
    {
        public static Logger CreateLogger()
        {
            var configuration = LoadAppConfiguration();
            var loggerConfiguration = new LoggerConfiguration();
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing") return loggerConfiguration.MinimumLevel.Fatal().CreateLogger();
            return loggerConfiguration
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Infrastructure", LogEventLevel.Warning)
                .Destructure.AsScalar<JObject>()
                .Destructure.AsScalar<JArray>()
                .WriteTo.Async(a => a.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}", theme: AnsiConsoleTheme.Code))
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        public static IApplicationBuilder UseCustomSerilogRequestLogging(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(c =>
            {
                c.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    //Add your useful information here
                };
            });

            return app;
        }

        private static IConfigurationRoot LoadAppConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                .AddJsonFile("appsettings.local.json", true)
                .Build();
        }
    }
}
