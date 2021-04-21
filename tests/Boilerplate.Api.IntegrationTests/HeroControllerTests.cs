using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Boilerplate.Api.IntegrationTests.Helpers;
using Boilerplate.Application.DTOs;
using Boilerplate.Application.DTOs.Hero;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Boilerplate.Api.IntegrationTests
{
    public class HeroControllerTests : IntegrationTest
    {
        #region GET

        [Fact]
        public async Task Get_AllHeroes_ReturnsOk()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hero");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = JsonConvert.DeserializeObject<PaginatedList<GetHeroDto>>(await response.Content.ReadAsStringAsync());
            json.Should().NotBeNull();
            json.Result.Should().OnlyHaveUniqueItems();
            json.Result.Should().HaveCount(3);
            json.CurrentPage.Should().Be(1);
            json.TotalItems.Should().Be(3);
            json.TotalPages.Should().Be(1);
        }

        [Fact]
        public async Task Get_AllHeroesWithPaginationFilter_ReturnsOk()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hero?PageSize=1&CurrentPage=1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = JsonConvert.DeserializeObject<PaginatedList<GetHeroDto>>(await response.Content.ReadAsStringAsync());
            json.Should().NotBeNull();
            json.Result.Should().OnlyHaveUniqueItems();
            json.Result.Should().HaveCount(1);
            json.CurrentPage.Should().Be(1);
            json.TotalItems.Should().Be(3);
            json.TotalPages.Should().Be(3);
        }

        [Fact]
        public async Task Get_AllHeroesWithNegativePageSize_ReturnsOk()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hero?PageSize=-1&CurrentPage=1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = JsonConvert.DeserializeObject<PaginatedList<GetHeroDto>>(await response.Content.ReadAsStringAsync());
            json.Should().NotBeNull();
            json.Result.Should().OnlyHaveUniqueItems();
            json.Result.Should().HaveCount(3);
            json.CurrentPage.Should().Be(1);
            json.TotalItems.Should().Be(3);
            json.TotalPages.Should().Be(1);
        }

        [Fact]
        public async Task Get_AllHeroesWithNegativeCurrentPage_ReturnsOk()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hero?PageSize=15&CurrentPage=-1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = JsonConvert.DeserializeObject<PaginatedList<GetHeroDto>>(await response.Content.ReadAsStringAsync());
            json.Should().NotBeNull();
            json.Result.Should().OnlyHaveUniqueItems();
            json.Result.Should().HaveCount(3);
            json.CurrentPage.Should().Be(1);
            json.TotalItems.Should().Be(3);
            json.TotalPages.Should().Be(1);
        }

        [Fact]
        public async Task Get_ExistingHeroesWithFilter_ReturnsOk()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hero?Name=Corban");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = JsonConvert.DeserializeObject<PaginatedList<GetHeroDto>>(await response.Content.ReadAsStringAsync());
            json.Should().NotBeNull();
            json.Result.Should().OnlyHaveUniqueItems();
            json.Result.Should().HaveCount(1);
            json.CurrentPage.Should().Be(1);
            json.TotalItems.Should().Be(1);
            json.TotalPages.Should().Be(1);
        }


        [Fact]
        public async Task Get_NonExistingHeroesWithFilter_ReturnsOk()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hero?Name=asdfsdlkafhsduifhasduifhsdui");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = JsonConvert.DeserializeObject<PaginatedList<GetHeroDto>>(await response.Content.ReadAsStringAsync());
            json.Should().NotBeNull();
            json.Result.Should().BeEmpty();
            json.CurrentPage.Should().Be(1);
            json.TotalItems.Should().Be(0);
            json.TotalPages.Should().Be(0);
        }

        [Fact]
        public async Task GetById_ExistingHero_ReturnsOk()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = JObject.Parse(await response.Content.ReadAsStringAsync());
            json["id"].Should().NotBeNull();
            json["name"].Should().NotBeNull();
            json["heroType"].Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ExistingHero_ReturnsNotFound()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync($"/api/Hero/{Guid.NewGuid()}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion

        #region POST

        [Fact]
        public async Task Post_ValidHero_ReturnsCreated()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonSerializer.Serialize(new
            {
                Name = "Name hero success",
                HeroType = 1
            });
            var response = await client.PostAsync("/api/Hero", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var json = JObject.Parse(await response.Content.ReadAsStringAsync());
            json["id"].Should().NotBeNull();
            json["name"].Should().NotBeNull();
            json["heroType"].Should().NotBeNull();
        }

        [Fact]
        public async Task Post_NamelessHero_ReturnsBadRequest()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonSerializer.Serialize(new
            {
                Individuality = "Individuality hero badrequest"
            });
            var response = await client.PostAsync("/api/Hero", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_IndividualitylessHero_ReturnsBadRequest()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonSerializer.Serialize(new
            {
                Name = "Name hero badrequest"
            });
            var response = await client.PostAsync("/api/Hero", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Post_EmptyHero_ReturnsBadRequest()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonSerializer.Serialize(new
            {
            });
            var response = await client.PostAsync("/api/Hero", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion


        #region PUT

        [Fact]
        public async Task Put_ValidHero_ReturnsNoContent()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonSerializer.Serialize(new
            {
                Name = "Name hero success",
                HeroType = 1
            });
            var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }


        [Fact]
        public async Task Put_NamelessHero_ReturnsBadRequest()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonSerializer.Serialize(new
            {
                HeroType = 1
            });
            var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_Individualityless_ReturnsBadRequest()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonSerializer.Serialize(new
            {
                Name = "Name hero badrequest"
            });
            var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_EmptyHero_ReturnsBadRequest()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonSerializer.Serialize(new
            {
            });
            var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Put_InvalidHeroId_ReturnsNotFound()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonSerializer.Serialize(new
            {
                Name = "Name hero not found",
                HeroType = 1
            });
            var response = await client.PutAsync("/api/Hero/1d2c03e0-cc51-4f22-b1be-cdee04b1f896", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion

        #region DELETE

        [Fact]
        public async Task Delete_ValidHero_ReturnsNoContent()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            var response = await client.DeleteAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_InvalidHero_ReturnsNotFound()
        {
            // Arrange
            var client = Factory.RebuildDb().CreateClient();

            var response = await client.DeleteAsync("/api/Hero/88d59ace-2c1a-49b0-8190-49b8304f8120");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion
    }
}
