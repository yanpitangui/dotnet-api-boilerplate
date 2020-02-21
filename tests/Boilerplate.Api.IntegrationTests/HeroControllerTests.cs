using Boilerplate.Domain.Entities;
using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Boilerplate.Api.IntegrationTests
{
    public class HeroControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyHero_ReturnsEmptyResponse()
        {
            // Arrange
            /* Maybe add some authentication later*/
            // Act
            var result = await testClient.GetAsync("/api/Hero");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            (await result.Content.ReadAsAsync<IEnumerable<Hero>>()).Should().BeEmpty();
        }
    }
}
