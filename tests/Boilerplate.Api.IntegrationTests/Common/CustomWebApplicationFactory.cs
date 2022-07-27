using Boilerplate.Application.Common;
using Boilerplate.Infrastructure.Context;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Boilerplate.Api.IntegrationTests.Common;

public class CustomWebApplicationFactory : WebApplicationFactory<IAssemblyMarker>
{
    private readonly SqliteConnection? _sqliteConnection;

    public CustomWebApplicationFactory()
    {
        _sqliteConnection = new SqliteConnection("DataSource=" + NewId.NextSequentialGuid() + ".db");

        _sqliteConnection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            // Replace sql server context for sqlite
            var serviceTypes = new List<Type>
            {
                typeof(DbContextOptions<ApplicationDbContext>),
                typeof(IContext),
            };
            var contextsDescriptor = services.Where(d => serviceTypes.Contains(d.ServiceType)).ToList();
            foreach (var descriptor in contextsDescriptor)
                services.Remove(descriptor);

            services.AddScoped(_ => CreateContext());
        }).ConfigureLogging(o => o.AddFilter(loglevel => loglevel >= LogLevel.Error));
        base.ConfigureWebHost(builder);
    }
    
    public IContext CreateContext()
    {
        SQLitePCL.Batteries.Init();
        return new SqliteContext(_sqliteConnection!);
    }

    public override async ValueTask DisposeAsync()
    {
        if (_sqliteConnection is IAsyncDisposable conn)
        {
            SqliteConnection.ClearPool(_sqliteConnection);
            await using var context = CreateContext();
            await _sqliteConnection.CloseAsync();
            await context.Database.EnsureDeletedAsync();
            await conn.DisposeAsync();
            
        }
        GC.SuppressFinalize(this);
        await base.DisposeAsync();
    }
}