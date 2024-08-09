using Boilerplate.Api.IntegrationTests.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Heroes;
using Boilerplate.Application.Features.Heroes.CreateHero;
using Boilerplate.Application.Features.Heroes.GetAllHeroes;
using Boilerplate.Application.Features.Heroes.UpdateHero;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace Boilerplate.Api.IntegrationTests;

public class HeroControllerTests(CustomWebApplicationFactory apiFactory) : BaseTest(apiFactory)
{
    #region GET

    [Fact]
    public async Task Get_AllHeroes_ReturnsOk()
    {
        // Act
        PaginatedList<GetHeroResponse>? response = await GetAsync<PaginatedList<GetHeroResponse>>("/api/Hero");

        // Assert
        response.Should().NotBeNull();
        response!.Result.Should().OnlyHaveUniqueItems();
        response.Result.Should().HaveCount(3);
        response.CurrentPage.Should().Be(1);
        response.TotalItems.Should().Be(3);
        response.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task Get_AllHeroesWithPaginationFilter_ReturnsOk()
    {
        // Act
        PaginatedList<GetHeroResponse>? response = await GetAsync<PaginatedList<GetHeroResponse>>("/api/Hero",
            new GetAllHeroesRequest(null, null, null, null, null, null, 1, 1));

        // Assert
        response.Should().NotBeNull();
        response!.Result.Should().OnlyHaveUniqueItems();
        response.Result.Should().HaveCount(1);
        response.CurrentPage.Should().Be(1);
        response.TotalItems.Should().Be(3);
        response.TotalPages.Should().Be(3);
    }

    [Fact]
    public async Task Get_AllHeroesWithNegativePageSize_ReturnsOk()
    {
        // Act
        PaginatedList<GetHeroResponse>? response = await GetAsync<PaginatedList<GetHeroResponse>>("/api/Hero",
            new GetAllHeroesRequest(
                null, null, null, null, null, null, 1, -1));

        // Assert
        response.Should().NotBeNull();
        response!.Result.Should().OnlyHaveUniqueItems();
        response.Result.Should().HaveCount(3);
        response.CurrentPage.Should().Be(1);
        response.TotalItems.Should().Be(3);
        response.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task Get_AllHeroesWithNegativeCurrentPage_ReturnsOk()
    {
        // Act
        PaginatedList<GetHeroResponse>? response = await GetAsync<PaginatedList<GetHeroResponse>>("/api/Hero",
            new GetAllHeroesRequest(null, null, null, null, null, null, -1));

        // Assert
        response.Should().NotBeNull();
        response!.Result.Should().OnlyHaveUniqueItems();
        response.Result.Should().HaveCount(3);
        response.CurrentPage.Should().Be(1);
        response.TotalItems.Should().Be(3);
        response.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task Get_ExistingHeroesWithFilter_ReturnsOk()
    {
        // Act
        PaginatedList<GetHeroResponse>? response = await GetAsync<PaginatedList<GetHeroResponse>>("/api/Hero",
            new GetAllHeroesRequest("Corban", null, null, null, null, null, 1, 10) { Name = "Corban" });

        // Assert
        response.Should().NotBeNull();
        response!.Result.Should().OnlyHaveUniqueItems();
        response.Result.Should().HaveCount(1);
        response.CurrentPage.Should().Be(1);
        response.TotalItems.Should().Be(1);
        response.TotalPages.Should().Be(1);
    }


    [Fact]
    public async Task Get_NonExistingHeroesWithFilter_ReturnsOk()
    {
        // Act
        PaginatedList<GetHeroResponse>? response = await GetAsync<PaginatedList<GetHeroResponse>>("/api/Hero",
            new GetAllHeroesRequest { Name = "asdsadsadsadsadasdsasadsa" });

        // Assert
        response.Should().NotBeNull();
        response!.Result.Should().BeEmpty();
        response.CurrentPage.Should().Be(1);
        response.TotalItems.Should().Be(0);
        response.TotalPages.Should().Be(0);
    }

    [Fact]
    public async Task GetById_ExistingHero_ReturnsOk()
    {
        // Act
        GetHeroResponse? response = await GetAsync<GetHeroResponse>("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1");

        // Assert
        response.Should().NotBeNull();
        response!.Id.Should().NotBe(HeroId.Empty);
        response.Name.Should().NotBeNull();
        response.HeroType.Should().NotBeNull();
    }

    [Fact]
    public async Task GetById_ExistingHero_ReturnsNotFound()
    {
        // Act
        HttpResponseMessage response = await GetAsync($"/api/Hero/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion

    #region POST

    [Fact]
    public async Task Post_ValidHero_ReturnsCreated()
    {
        // Act
        CreateHeroRequest newHero = new()
        {
            Name = "Name hero success", HeroType = HeroType.Student, Individuality = "all for one"
        };
        HttpResponseMessage response = await PostAsync("/api/Hero", newHero);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        GetHeroResponse? json = await response.Content.ReadFromJsonAsync<GetHeroResponse>();
        json.Should().NotBeNull();
        json!.Id.Should().NotBe(HeroId.Empty);
        json.Name.Should().NotBeNull();
        json.HeroType.Should().NotBeNull();
    }

    [Fact]
    public async Task Post_NamelessHero_ReturnsBadRequest()
    {
        // Act
        CreateHeroRequest newHero = new() { Individuality = "Individuality hero badrequest" };
        HttpResponseMessage response = await PostAsync("/api/Hero", newHero);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_Negative_Age_Hero_ReturnsBadRequest()
    {
        // Act
        CreateHeroRequest newHero = new()
        {
            Individuality = "Individuality hero badrequest", Name = "Test hero", Age = -1
        };
        HttpResponseMessage response = await PostAsync("/api/Hero", newHero);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_EmptyHero_ReturnsBadRequest()
    {
        // Act
        CreateHeroRequest newHero = new();
        HttpResponseMessage response = await PostAsync("/api/Hero", newHero);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion


    #region PUT

    [Fact]
    public async Task Update_ValidHero_Should_Return_Ok()
    {
        // Arrange


        // Act
        UpdateHeroRequest newHero = new()
        {
            Name = "Name hero success", HeroType = HeroType.Villain, Individuality = "Invisibility"
        };
        HttpResponseMessage response = await PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", newHero);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }


    [Fact]
    public async Task Put_NamelessHero_ReturnsBadRequest()
    {
        // Act
        UpdateHeroRequest newHero = new() { HeroType = HeroType.Student };
        HttpResponseMessage response = await PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", newHero);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_Individualityless_ReturnsBadRequest()
    {
        // Act
        UpdateHeroRequest newHero = new() { Name = "Name hero badrequest" };
        HttpResponseMessage response = await PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", newHero);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_EmptyHero_ReturnsBadRequest()
    {
        // Act
        UpdateHeroRequest newHero = new();
        HttpResponseMessage response = await PutAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1", newHero);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_InvalidHeroId_ReturnsNotFound()
    {
        // Act
        UpdateHeroRequest newHero = new()
        {
            Name = "Name hero not found", HeroType = HeroType.Teacher, Individuality = "one for all"
        };
        HttpResponseMessage response = await PutAsync($"/api/Hero/{Guid.NewGuid()}", newHero);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion

    #region DELETE

    [Fact]
    public async Task Delete_ValidHero_Returns_NoContent()
    {
        // Arrange
        HttpResponseMessage response = await DeleteAsync("/api/Hero/824a7a65-b769-4b70-bccb-91f880b6ddf1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task DeleteHero_EmptyId_Should_Return_BadRequest()
    {
        // Arrange
        HttpResponseMessage response = await DeleteAsync($"/api/Hero/{Guid.Empty}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Delete_InvalidHero_ReturnsNotFound()
    {
        // Arrange
        HttpResponseMessage response = await DeleteAsync($"/api/Hero/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion
}