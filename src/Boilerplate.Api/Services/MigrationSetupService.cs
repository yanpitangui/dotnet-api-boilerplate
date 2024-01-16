using Boilerplate.Application.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Api.Services;

public class MigrationSetupService(ILogger<IAssemblyMarker> logger, IEnumerable<IContext> contexts) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var context in contexts)
        {
            var strategy = context.Database.CreateExecutionStrategy();
            logger.LogInformation("Running migrations for {Context}", context.GetType().Name);
            await strategy.ExecuteAsync(async () => await context.Database.MigrateAsync(cancellationToken: cancellationToken));
            await context.Database.MigrateAsync(cancellationToken: cancellationToken);
            logger.LogInformation("Migrations applied successfully");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => 
        Task.CompletedTask;
}