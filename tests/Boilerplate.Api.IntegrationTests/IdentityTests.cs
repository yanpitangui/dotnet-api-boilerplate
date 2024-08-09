using Boilerplate.Api.IntegrationTests.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Identity.Data;
using System.Net.Http;
using System.Net.Http.Json;

namespace Boilerplate.Api.IntegrationTests;

public class IdentityTests(CustomWebApplicationFactory apiFactory) : BaseTest(apiFactory)
{
    private const string Email = "yadayada@gmail.com";
    private const string Password = "NXD8fvw3pqc!uhr_mwn";


    [Fact]
    public async Task Register_ValidUser_ReturnsOk()
    {
        // Arrange
        RegisterRequest request = new() { Email = Email, Password = Password };

        // act
        HttpResponseMessage result = await PostAsync("api/Identity/Register", request);

        // assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public async Task Login_with_registered_account_should_return_access_token()
    {
        // arrange
        RegisterRequest request = new() { Email = Email, Password = Password };
        await PostAsync("api/Identity/Register", request, true);

        // act
        HttpResponseMessage result = await PostAsync("api/Identity/Login", request);

        // assert
        result.Should().BeSuccessful();
        JsonElement payload = await result.Content.ReadFromJsonAsync<JsonElement>();
        payload.GetProperty("accessToken").GetString().Should().NotBeNullOrWhiteSpace();
    }
}