using System;
using System.Net;
using Boilerplate.Api.IntegrationTests.Helpers;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Users;
using Boilerplate.Application.Features.Users.CreateUser;
using FluentAssertions;
using Newtonsoft.Json;

namespace Boilerplate.Api.IntegrationTests;

public class UserControllerTests : IntegrationTest, IAsyncLifetime
{
    public UserControllerTests(WebApplicationFactoryFixture fixture) : base(fixture)
    {

    }

    public async Task InitializeAsync()
    {
        _adminToken ??= await GetAdminToken();
        _userToken ??= await GetUserToken();
    }

    private static string? _adminToken;

    private static string? _userToken;

    #region GET

    [Fact]
    public async Task Get_AllUsers_ReturnsOk()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);


        // Act
        var response = await client.GetAsync("/api/User");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK, await response.Content.ReadAsStringAsync());
        var json = await response.DeserializeContent<PaginatedList<GetUserResponse>>();
        json.Should().NotBeNull();
        json!.Result.Should().OnlyHaveUniqueItems();
        json.Result.Should().HaveCount(2);
        json.CurrentPage.Should().Be(1);
        json.TotalItems.Should().Be(2);
        json.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task Get_AllUsersWithPaginationFilter_ReturnsOk()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);


        // Act
        var response = await client.GetAsync("/api/User?PageSize=1&CurrentPage=1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.DeserializeContent<PaginatedList<GetUserResponse>>();
        json.Should().NotBeNull();
        json!.Result.Should().OnlyHaveUniqueItems();
        json.Result.Should().HaveCount(1);
        json.CurrentPage.Should().Be(1);
        json.TotalItems.Should().Be(2);
        json.TotalPages.Should().Be(2);
    }

    [Fact]
    public async Task Get_AllUsersWithNegativePageSize_ReturnsOk()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);


        // Act
        var response = await client.GetAsync("/api/User?PageSize=-1&CurrentPage=1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.DeserializeContent<PaginatedList<GetUserResponse>>();
        json.Should().NotBeNull();
        json!.Result.Should().OnlyHaveUniqueItems();
        json.Result.Should().HaveCount(2);
        json.CurrentPage.Should().Be(1);
        json.TotalItems.Should().Be(2);
        json.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task Get_AllUsersWithNegativeCurrentPage_ReturnsOk()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);


        // Act
        var response = await client.GetAsync("/api/User?PageSize=15&CurrentPage=-1");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.DeserializeContent<PaginatedList<GetUserResponse>>();
        json.Should().NotBeNull();
        json!.Result.Should().OnlyHaveUniqueItems();
        json.Result.Should().HaveCount(2);
        json.CurrentPage.Should().Be(1);
        json.TotalItems.Should().Be(2);
        json.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task Get_ExistingUsersWithFilter_ReturnsOk()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);


        // Act
        var response = await client.GetAsync("/api/User?Email=admin@boilerplate.com");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.DeserializeContent<PaginatedList<GetUserResponse>>();
        json.Should().NotBeNull();
        json!.Result.Should().OnlyHaveUniqueItems();
        json.Result.Should().HaveCount(1);
        json.CurrentPage.Should().Be(1);
        json.TotalItems.Should().Be(1);
        json.TotalPages.Should().Be(1);
    }


    [Fact]
    public async Task Get_NonExistingUsersWithFilter_ReturnsOk()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);


        // Act
        var response = await client.GetAsync("/api/User?Email=asdfsdlkafhsduifhasduifhsdui");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.DeserializeContent<PaginatedList<GetUserResponse>>();
        json.Should().NotBeNull();
        json!.Result.Should().BeEmpty();
        json.CurrentPage.Should().Be(1);
        json.TotalItems.Should().Be(0);
        json.TotalPages.Should().Be(0);
    }

    [Fact]
    public async Task GetById_ExistingUser_ReturnsOk()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);


        // Act
        var response = await client.GetAsync("/api/User/2e3b7a21-f06e-4c47-b28a-89bdaa3d2a37");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.DeserializeContent<GetUserResponse>();
        json.Should().NotBeNull();
        json!.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetById_ExistingUser_ReturnsNotFound()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);


        // Act
        var response = await client.GetAsync($"/api/User/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion

    #region POST

    [Fact]
    public async Task<string> GetAdminToken()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();

        // Act
        var loginData = new
        {
            Email = "admin@boilerplate.com",
            Password = "testpassword123"
        };

        var response = await client.PostAsync("/api/User/authenticate", loginData.GetStringContent());
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.DeserializeContent<Jwt>();
        json.Should().NotBeNull();
        json!.ExpDate.Should().NotBe(DateTime.MinValue);
        json.Token.Should().NotBeNullOrWhiteSpace();

        return json.Token;
    }

    [Fact]
    public async Task<string> GetUserToken()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();

        // Act
        var loginData = new
        {
            Email = "user@boilerplate.com",
            Password = "testpassword123"
        };

        var response = await client.PostAsync("/api/User/authenticate", loginData.GetStringContent());
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.DeserializeContent<Jwt>();
        json.Should().NotBeNull();
        json!.ExpDate.Should().NotBe(DateTime.MinValue);
        json.Token.Should().NotBeNullOrWhiteSpace();

        return json.Token;
    }

    [Theory]
    [InlineData("admin@boilerplate.com", "incorrect")]
    [InlineData("admin@incorrect.com", "testpassword123")]
    public async Task Authenticate_IncorretUserOrPassword(string email, string password)
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();

        // Act
        var loginData = new
        {
            Email = email,
            Password = password
        };
        var response = await client.PostAsync("/api/User/authenticate", loginData.GetStringContent());
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Post_ValidUser_ReturnsCreated()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);
        var userFaker = new Bogus.Faker<CreateUserRequest>();

        // Act
        var newUser = userFaker
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, f=> f.Internet.Password())
            .RuleFor(x => x.IsAdmin, _ => true)
            .Generate();
        var response = await client.PostAsync("/api/User", newUser.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var json = JsonConvert.DeserializeObject<GetUserResponse>(await response.Content.ReadAsStringAsync());
        json.Should().NotBeNull();
        json!.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Post_EmaillessUser_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);

        // Act
        var newUser = new
        {
            Password = "mypasswordisnice"
        };
        var response = await client.PostAsync("/api/User", newUser.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_PasswordlessUser_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);
        var userFaker = new Bogus.Faker<CreateUserRequest>();

        // Act
        var newUser = userFaker
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, _=> null!)
            .RuleFor(x => x.IsAdmin, _ => false)
            .Generate();
        var response = await client.PostAsync("/api/User", newUser.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_EmptyUser_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);

        // Act
        var newUser = new
        {
        };
        var response = await client.PostAsync("/api/User", newUser.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion

    #region PATCH

    [Fact]
    public async Task Patch_ValidUser_UpdatePassword_NoContent()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);

        var userFaker = new Bogus.Faker<CreateUserRequest>();

        var newUser = userFaker
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, f=> f.Internet.Password())
            .RuleFor(x => x.IsAdmin, _ => true)
            .Generate();
        var response = await client.PostAsync("/api/User", newUser.GetStringContent());
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        response = await client.PostAsync("/api/User/authenticate", newUser.GetStringContent());
        var newUserToken = await response.DeserializeContent<Jwt>();
        client.UpdateBearerToken(newUserToken!.Token);

        // Act
        response = await client.PatchAsync($"/api/User/password",
            new {Password = "mypasswordisverynice"}.GetStringContent());

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

    }
    #endregion

    #region DELETE

    [Fact]
    public async Task Delete_ValidUser_ReturnsNoContent()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);

        var response = await client.DeleteAsync("/api/User/c68acd7b-9054-4dc3-b536-17a1b81fa7a3");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_InvalidUser_ReturnsNotFound()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_adminToken);

        var response = await client.DeleteAsync($"/api/User/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_AsUserRole_ReturnsForbidden()
    {
        // Arrange
        var client = Factory.RebuildDb().CreateClient();
        client.UpdateBearerToken(_userToken);

        var response = await client.DeleteAsync($"/api/User/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    #endregion


    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}