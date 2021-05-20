using Boilerplate.Api.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Boilerplate.Api.IntegrationTests
{
    [Collection("Integration tests")] // workaround to make the conflict between tests not crash itself
    public abstract class IntegrationTest: IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> Factory;

        protected IntegrationTest(WebApplicationFactory<Startup> fixture)
        {
            Factory = fixture.BuildApplicationFactory();
        }
    }
}
