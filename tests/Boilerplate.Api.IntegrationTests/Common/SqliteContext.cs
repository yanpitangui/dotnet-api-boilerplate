using Boilerplate.Infrastructure.Context;
using EntityFramework.Exceptions.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Boilerplate.Api.IntegrationTests.Common;

public class SqliteContext : ApplicationDbContext
{
    private readonly SqliteConnection _connection;

    private static readonly ILoggerFactory DebugLoggerFactory
        = LoggerFactory.Create(builder => builder.AddDebug());

    public SqliteContext(SqliteConnection connection)
    {
        _connection = connection;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder
            .UseExceptionProcessor()
            .UseLoggerFactory(DebugLoggerFactory)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseSqlite(_connection);
    }
}