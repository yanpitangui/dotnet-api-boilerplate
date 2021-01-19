using Boilerplate.Api.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Boilerplate.Api.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly WebApplicationFactory<Startup> _factory;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>().BuildApplicationFactory();
            _factory = appFactory;
        }
    }
}
