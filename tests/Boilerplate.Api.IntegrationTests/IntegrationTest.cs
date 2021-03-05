using Boilerplate.Api.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Boilerplate.Api.IntegrationTests
{
    [TestClass]
    public class IntegrationTest
    {
        protected static WebApplicationFactory<Startup> _factory;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            var appFactory = new WebApplicationFactory<Startup>().BuildApplicationFactory();
            _factory = appFactory;
        }
    }
}
