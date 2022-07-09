using System;
using System.Net;
using Boilerplate.Api.IntegrationTests.Helpers;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Heroes;
using Boilerplate.Application.Features.Heroes.CreateHero;
using Boilerplate.Application.Features.Heroes.UpdateHero;
using Boilerplate.Domain.Entities.Enums;
using FluentAssertions;

namespace Boilerplate.Api.IntegrationTests;

public class HeroControllerTests : IntegrationTest
{

    public HeroControllerTests(WebApplicationFactoryFixture fixture) : base(fixture) { }

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
        var json = await response.DeserializeContent<PaginatedList<GetHeroResponse>>();
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
        var json = await response.DeserializeContent<PaginatedList<GetHeroResponse>>();
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
        var json = await response.DeserializeContent<PaginatedList<GetHeroResponse>>();
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
        var json = await response.DeserializeContent<PaginatedList<GetHeroResponse>>();
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
        var json = await response.DeserializeContent<PaginatedList<GetHeroResponse>>();
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
        var json = await response.DeserializeContent<PaginatedList<GetHeroResponse>>();
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
        var json = await response.DeserializeContent<GetHeroResponse>();
        json.Should().NotBeNull();
        json.Id.Should().NotBeEmpty();
        json.Name.Should().NotBeNull();
        json.HeroType.Should().NotBeNull();
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
        var newHero = new CreateHeroRequest()
        {
            Name = "Name hero success",
            HeroType = HeroType.Student,
            Individuality = "all for one"
            
        };
        var response = await client.PostAsync("/api/Hero", newHero.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var json = await response.DeserializeContent<GetHeroResponse>();
        json.Should().NotBeNull();
        json.Id.Should().NotBeEmpty();
        json.Name.Should().NotBeNull();
        json.HeroType.Should().NotBeNull();
    }

    [Fact]
    public async Task Post_NamelessHero_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();

        // Act
        var newHero = new CreateHeroRequest()
        {
            Individuality = "Individuality hero badrequest",
            
        };
        var response = await client.PostAsync("/api/Hero", newHero.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_IndividualitylessHero_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();

        // Act
        var newHero = new CreateHeroRequest()
        {
            Name = "Name hero badrequest"
        };
        var response = await client.PostAsync("/api/Hero", newHero.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_EmptyHero_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();

        // Act
        var newHero = new CreateHeroRequest()
        {
        };
        var response = await client.PostAsync("/api/Hero", newHero.GetStringContent());

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
        var newHero = new UpdateHeroRequest()
        {
            Name = "Name hero success",
            HeroType = HeroType.Villain,
            Individuality = "Invisibility"
            
        };
        var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", newHero.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }


    [Fact]
    public async Task Put_NamelessHero_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();

        // Act
        var newHero = new UpdateHeroRequest()
        {
            HeroType = HeroType.Student
        };
        var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", newHero.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_Individualityless_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();

        // Act
        var newHero = new UpdateHeroRequest()
        {
            Name = "Name hero badrequest"
        };
        var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", newHero.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_EmptyHero_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();

        // Act
        var newHero = new UpdateHeroRequest()
        {
        };
        var response = await client.PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", newHero.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_InvalidHeroId_ReturnsNotFound()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();

        // Act
        var newHero = new UpdateHeroRequest()
        {
            Name = "Name hero not found",
            HeroType = HeroType.Teacher,
            Individuality = "one for all"
        };
        var response = await client.PutAsync("/api/Hero/1d2c03e0-cc51-4f22-b1be-cdee04b1f896", newHero.GetStringContent());

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