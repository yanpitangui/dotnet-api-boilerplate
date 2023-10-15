using Boilerplate.Application.Common;
using Boilerplate.Infrastructure;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using Respawn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Testcontainers.PostgreSql;

namespace Boilerplate.Api.IntegrationTests.Common;

public class CustomWebApplicationFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{
    
    // Db connection
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithUsername("app_user")
        .WithPassword("myHardCoreTestDb123")
        .WithDatabase("HeroDb")
        .WithName($"integration-tests-{Guid.NewGuid()}")
        .Build();
    private string _connString = default!;
    private NpgsqlConnection _dbConnection = default!;
    private Respawner _respawner = default!;
    
    public HttpClient Client { get; private set; } = default!;
    
    public CustomWebApplicationFactory()
    {
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            var serviceTypes = new List<Type>
            {
                typeof(DbContextOptions<ApplicationDbContext>),
            };
            var contextsDescriptor = services.Where(d => serviceTypes.Contains(d.ServiceType)).ToList();
            foreach (var descriptor in contextsDescriptor)
                services.Remove(descriptor);

            services.AddSingleton(_ => new DbContextOptionsBuilder<ApplicationDbContext>()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseExceptionProcessor()
                .UseNpgsql(_connString)
                .Options);
        }).ConfigureLogging(o => o.AddFilter(loglevel => loglevel >= LogLevel.Error));
        base.ConfigureWebHost(builder);
    }
    
    public IContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseExceptionProcessor()
            .UseNpgsql(_connString)
            .Options;
        return new ApplicationDbContext(options);
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _connString = _dbContainer.GetConnectionString();
        _dbConnection = new NpgsqlConnection(_connString);
        Client = CreateClient();
        await using var context = CreateContext();
        await SetupRespawnerAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
    }

    private async Task SetupRespawnerAsync()
    {
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
        });
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbConnection.DisposeAsync();
        await _dbContainer.DisposeAsync();
    }
}