using Boilerplate.Api.IntegrationTests.Common;
using System;
using System.Net;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Auth.Authenticate;
using Boilerplate.Application.Features.Users;
using Boilerplate.Application.Features.Users.CreateUser;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Application.Features.Users.UpdatePassword;
using Boilerplate.Domain.Entities.Common;
using FluentAssertions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Boilerplate.Api.IntegrationTests;

public class UserControllerTests : BaseTest
{
    
    public UserControllerTests(CustomWebApplicationFactory apiFactory) : base(apiFactory)
    {
    }
    
    private static string? _adminToken;

    private static string? _userToken;
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _adminToken ??= await GetAdminToken();
        _userToken ??= await GetUserToken();
        LoginAsAdmin();
    }

    protected void UpdateBearerToken(string? token)
    {
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    } 

    private void LoginAsAdmin()
    {
        UpdateBearerToken(_adminToken);
    }

    private void LoginAsUser()
    {
        UpdateBearerToken(_userToken);
    }
    
    #region GET

    [Fact]
    public async Task Get_AllUsers_ReturnsOk()
    {
        // Act
        var response = await GetAsync<PaginatedList<GetUserResponse>>("/api/User");

        // Assert
        response.Should().NotBeNull();
        response!.Result.Should().OnlyHaveUniqueItems();
        response.Result.Should().HaveCount(2);
        response.CurrentPage.Should().Be(1);
        response.TotalItems.Should().Be(2);
        response.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task Get_AllUsersWithPaginationFilter_ReturnsOk()
    {
        // Act
        var response = await GetAsync<PaginatedList<GetUserResponse>>("/api/User", new GetUsersRequest()
        {
            CurrentPage = 1,
            PageSize = 1,
        });

        // Assert
        response.Should().NotBeNull();
        response!.Result.Should().OnlyHaveUniqueItems();
        response.Result.Should().HaveCount(1);
        response.CurrentPage.Should().Be(1);
        response.TotalItems.Should().Be(2);
        response.TotalPages.Should().Be(2);
    }

    [Fact]
    public async Task Get_ExistingUsersWithFilter_ReturnsOk()
    {
        // Act
        var response = await GetAsync<PaginatedList<GetUserResponse>>("/api/User", new GetUsersRequest()
        {
            Email = "admin@boilerplate.com"
        });

        // Assert
        response.Should().NotBeNull();
        response!.Result.Should().OnlyHaveUniqueItems();
        response.Result.Should().HaveCount(1);
        response.CurrentPage.Should().Be(1);
        response.TotalItems.Should().Be(1);
        response.TotalPages.Should().Be(1);
    }


    [Fact]
    public async Task Get_NonExistingUsersWithFilter_ReturnsOk()
    {
        // Act
        var response = await GetAsync<PaginatedList<GetUserResponse>>("/api/User", new GetUsersRequest()
        {
            Email = "admifsdfsdfsdjma"
        });

        // Assert
        response.Should().NotBeNull();
        response!.Result.Should().BeEmpty();
        response.CurrentPage.Should().Be(1);
        response.TotalItems.Should().Be(0);
        response.TotalPages.Should().Be(0);
    }

    [Fact]
    public async Task GetById_ExistingUser_ReturnsOk()
    {
        // Act
        var response = await GetAsync<GetUserResponse>("/api/User/2e3b7a21-f06e-4c47-b28a-89bdaa3d2a37");

        // Assert
        response.Should().NotBeNull();
        response!.Id.Should().NotBe(UserId.Empty);
    }

    [Fact]
    public async Task GetById_ExistingUser_ReturnsNotFound()
    {
        // Act
        var response = await GetAsync($"/api/User/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    #endregion

    #region POST

    [Fact]
    public async Task<string> GetAdminToken()
    {
        // Act
        var loginData = new AuthenticateRequest()
        {
            Email = "admin@boilerplate.com",
            Password = "testpassword123"
        };

        var response = await PostAsync<Jwt>("/api/User/authenticate", loginData);
        response.Should().NotBeNull();
        response!.ExpDate.Should().NotBe(DateTime.MinValue);
        response.Token.Should().NotBeNullOrWhiteSpace();

        return response.Token;
    }

    [Fact]
    public async Task<string> GetUserToken()
    {
        // Act
        var loginData = new AuthenticateRequest()
        {
            Email = "user@boilerplate.com",
            Password = "testpassword123"
        };

        var response = await PostAsync<Jwt>("/api/User/authenticate", loginData);
        response.Should().NotBeNull();
        response!.ExpDate.Should().NotBe(DateTime.MinValue);
        response.Token.Should().NotBeNullOrWhiteSpace();

        return response.Token;
    }

    [Theory]
    [InlineData("admin@boilerplate.com", "incorrect")]
    [InlineData("admin@incorrect.com", "testpassword123")]
    public async Task Authenticate_IncorretUserOrPassword(string email, string password)
    {
        // Act
        var loginData = new AuthenticateRequest()
        {
            Email = email,
            Password = password
        };
        var response = await PostAsync("/api/User/authenticate", loginData);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Post_ValidUser_ReturnsCreated()
    {
        // Arrange
        var userFaker = new Bogus.Faker<CreateUserRequest>();

        // Act
        var newUser = userFaker
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, f=> f.Internet.Password())
            .RuleFor(x => x.IsAdmin, _ => true)
            .Generate();
        var response = await PostAsync<GetUserResponse>("/api/User", newUser);

        // Assert
        response.Should().NotBeNull();
        response!.Id.Should().NotBe(UserId.Empty);
    }

    [Fact]
    public async Task Post_EmaillessUser_ReturnsBadRequest()
    {
        // Act
        var newUser = new CreateUserRequest()
        {
            Password = "mypasswordisnice"
        };
        var response = await PostAsync("/api/User", newUser);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_PasswordlessUser_ReturnsBadRequest()
    {
        // Arrange
        var userFaker = new Bogus.Faker<CreateUserRequest>();

        // Act
        var newUser = userFaker
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, _=> null!)
            .RuleFor(x => x.IsAdmin, _ => false)
            .Generate();
        var response = await PostAsync("/api/User", newUser);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_EmptyUser_ReturnsBadRequest()
    {
        // Act
        var newUser = new CreateUserRequest();
        var response = await PostAsync("/api/User", newUser);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    #endregion

    #region PATCH

    [Fact]
    public async Task Patch_ValidUser_UpdatePassword_NoContent()
    {
        // Arrange
        var userFaker = new Bogus.Faker<CreateUserRequest>();

        var newUser = userFaker
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Password, f=> f.Internet.Password())
            .RuleFor(x => x.IsAdmin, _ => true)
            .Generate();
        var response = await PostAsync("/api/User", newUser);
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        response = await PostAsync("/api/User/authenticate", newUser);
        var newUserToken = await response.Content.ReadFromJsonAsync<Jwt>();
        UpdateBearerToken(newUserToken!.Token);

        // Act
        response = await PatchAsync($"/api/User/password",
            new UpdatePasswordRequest() {Password = "mypasswordisverynice"});

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

    }
    #endregion

    #region DELETE

    [Fact]
    public async Task Delete_ValidUser_ReturnsNoContent()
    {

        // Act
        var response = await DeleteAsync("/api/User/c68acd7b-9054-4dc3-b536-17a1b81fa7a3");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_InvalidUser_ReturnsNotFound()
    {

        // Act
        var response = await DeleteAsync($"/api/User/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_AsUserRole_ReturnsForbidden()
    {
        // Arrange
        LoginAsUser();

        // act
        var response = await DeleteAsync($"/api/User/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    #endregion

}