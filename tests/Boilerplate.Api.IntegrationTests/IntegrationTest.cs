using Boilerplate.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;

namespace Boilerplate.Api.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient testClient;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(HeroDbContext));
                        services.AddDbContext<HeroDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });
            testClient = appFactory.CreateClient();
        }
    }
}
