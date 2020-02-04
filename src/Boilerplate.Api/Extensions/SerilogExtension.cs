using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Core;
using System;

namespace Boilerplate.Api.Extensions
{
    public static class SerilogExtension
    {
        public static Logger CreateLogger()
        {
            var configuration = LoadAppConfiguration();
            return new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                .ReadFrom.Configuration(configuration)
                .Destructure.AsScalar<JObject>()
                .Destructure.AsScalar<JArray>()
                .Enrich.FromLogContext()
                .WriteTo.Console()
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
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddJsonFile("appsettings.local.json", optional: true)
                .Build();
        }
    }
}
