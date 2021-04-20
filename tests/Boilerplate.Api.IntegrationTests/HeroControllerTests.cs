using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Boilerplate.Api.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Boilerplate.Api.IntegrationTests
{
    [TestClass]
    public class HeroControllerTests : IntegrationTest
    {
        #region GET

        [TestMethod]
        public async Task Get_AllHeroes_ReturnsOk()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hero");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.Content.ReadAsStringAsync();
            var array = JArray.Parse(json);
            array.HasValues.Should().BeTrue();
            array.Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        public async Task Get_ExistingHeroesWithFilter_ReturnsOk()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hero?Name=Corban");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.Content.ReadAsStringAsync();
            var array = JArray.Parse(json);
            array.HasValues.Should().BeTrue();
            array.Should().OnlyHaveUniqueItems();
            array.Should().ContainSingle();
        }


        [TestMethod]
        public async Task Get_NonExistingHeroesWithFilter_ReturnsOk()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hero?Name=asdfsdlkafhsduifhasduifhsdui");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.Content.ReadAsStringAsync();
            var array = JArray.Parse(json);
            array.Should().BeEmpty();
        }

        [TestMethod]
        public async Task GetById_ExistingHero_ReturnsOk()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = JObject.Parse(await response.Content.ReadAsStringAsync());
            json["id"].Should().NotBeNull();
            json["name"].Should().NotBeNull();
            json["heroType"].Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetById_ExistingHero_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var response = await client.GetAsync($"/api/Hero/{Guid.NewGuid()}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion

        #region POST

        [TestMethod]
        public async Task Post_ValidHero_ReturnsCreated()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonConvert.SerializeObject(new
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

        [TestMethod]
        public async Task Post_NamelessHero_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonConvert.SerializeObject(new
            {
                Individuality = "Individuality hero badrequest"
            });
            var response = await client.PostAsync("/api/Hero", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task Post_IndividualitylessHero_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonConvert.SerializeObject(new
            {
                Name = "Name hero badrequest"
            });
            var response = await client.PostAsync("/api/Hero", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task Post_EmptyHero_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonConvert.SerializeObject(new
            {
            });
            var response = await client.PostAsync("/api/Hero", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion


        #region PUT

        [TestMethod]
        public async Task Put_ValidHero_ReturnsNoContent()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonConvert.SerializeObject(new
            {
                Name = "Name hero success",
                HeroType = 1
            });
            var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }


        [TestMethod]
        public async Task Put_NamelessHero_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonConvert.SerializeObject(new
            {
                HeroType = 1
            });
            var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task Put_Individualityless_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonConvert.SerializeObject(new
            {
                Name = "Name hero badrequest"
            });
            var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task Put_EmptyHero_ReturnsBadRequest()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonConvert.SerializeObject(new
            {
            });
            var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", new StringContent(newHero, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public async Task Put_InvalidHeroId_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            // Act
            var newHero = JsonConvert.SerializeObject(new
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

        [TestMethod]
        public async Task Delete_ValidHero_ReturnsNoContent()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            var response = await client.DeleteAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [TestMethod]
        public async Task Delete_InvalidHero_ReturnsNotFound()
        {
            // Arrange
            var client = _factory.RebuildDb().CreateClient();

            var response = await client.DeleteAsync("/api/Hero/88d59ace-2c1a-49b0-8190-49b8304f8120");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        #endregion
    }
}
