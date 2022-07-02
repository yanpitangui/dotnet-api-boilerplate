using Boilerplate.Api.IntegrationTests.Helpers;
using Boilerplate.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Boilerplate.Api.IntegrationTests;

public class WebApplicationFactoryFixture : IDisposable
{
    public readonly WebApplicationFactory<IAssemblyMarker> Factory;

    public WebApplicationFactoryFixture()
    {
        Factory = new WebApplicationFactory<IAssemblyMarker>().BuildApplicationFactory();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            using var _scope = (Factory.Services.GetRequiredService<IServiceScopeFactory>()).CreateScope();
            using var _context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            _context?.Database.EnsureDeleted();
            Factory.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}