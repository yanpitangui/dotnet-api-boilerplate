using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
namespace Boilerplate.Api.Configurations;

public static class LoggingSetup
{
    public static IHostBuilder UseLoggingSetup(this IHostBuilder host, IConfiguration configuration)
    {
        host.UseSerilog((_, _, lc) =>
        {
            lc.ReadFrom.Configuration(configuration);
        });

        return host;
    }

}