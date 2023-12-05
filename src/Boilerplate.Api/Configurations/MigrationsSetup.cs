using Boilerplate.Application.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Boilerplate.Api.Configurations;

public static class MigrationsSetup
{
    public static async Task Migrate(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<IAssemblyMarker>>();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<IContext>();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        logger.LogInformation("Running migrations...");
        await strategy.ExecuteAsync(async () => await dbContext.Database.MigrateAsync());
        await dbContext.Database.MigrateAsync();
        logger.LogInformation("Migrations applied succesfully");
    }
}