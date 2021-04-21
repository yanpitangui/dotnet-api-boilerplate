using Boilerplate.Api.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Boilerplate.Api.IntegrationTests
{
    public class IntegrationTest
    {
        public static WebApplicationFactory<Startup> Factory;

        static IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>().BuildApplicationFactory();
            Factory = appFactory;
        }

    }
}
