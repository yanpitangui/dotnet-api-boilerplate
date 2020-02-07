using System;
using Boilerplate.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Boilerplate.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = SerilogExtension.CreateLogger();
            try
            {
                Log.Logger.Information("Application starting up...");
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                Log.Logger.Fatal(ex, "Application startup failed.");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
