using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Infrastructure;

public class ApplicationDbInitializer : IHostedService
{
    private readonly ILogger<ApplicationDbInitializer> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ApplicationDbInitializer(ILogger<ApplicationDbInitializer> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var strategy = context.Database.CreateExecutionStrategy();
        _logger.LogInformation("Running migrations for {Context}", nameof(ApplicationDbContext));
        await strategy.ExecuteAsync(async () => await context.Database.MigrateAsync(cancellationToken: cancellationToken));
        await context.Database.MigrateAsync(cancellationToken: cancellationToken);
        _logger.LogInformation("Migrations applied successfully");
    }

    public Task StopAsync(CancellationToken cancellationToken) =>
        Task.CompletedTask;
}