using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Infrastructure;

public class ApplicationDbInitializer(ILogger<ApplicationDbInitializer> logger, IServiceProvider serviceProvider)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
        logger.LogInformation("Running migrations for {Context}", nameof(ApplicationDbContext));
        await strategy.ExecuteAsync(async () => await context.Database.MigrateAsync(cancellationToken));
        await context.Database.MigrateAsync(cancellationToken);
        logger.LogInformation("Migrations applied successfully");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}