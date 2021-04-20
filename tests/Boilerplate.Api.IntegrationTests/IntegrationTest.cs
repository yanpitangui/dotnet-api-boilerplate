using Boilerplate.Api.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Boilerplate.Api.IntegrationTests
{
    public class IntegrationTest
    {
        protected static WebApplicationFactory<Startup> _factory;

        static IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>().BuildApplicationFactory();
            _factory = appFactory;
        }

    }
}
