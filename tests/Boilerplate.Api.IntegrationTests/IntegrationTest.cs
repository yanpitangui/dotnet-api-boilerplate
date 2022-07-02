using Microsoft.AspNetCore.Mvc.Testing;

namespace Boilerplate.Api.IntegrationTests;

public abstract class IntegrationTest: IClassFixture<WebApplicationFactoryFixture>
{
    protected readonly WebApplicationFactory<IAssemblyMarker> Factory;

    protected IntegrationTest(WebApplicationFactoryFixture fixture)
    {
        Factory = fixture.Factory;
    }
}