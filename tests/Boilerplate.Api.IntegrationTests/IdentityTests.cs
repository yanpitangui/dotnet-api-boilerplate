using Boilerplate.Api.IntegrationTests.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using System.Net.Http.Json;

namespace Boilerplate.Api.IntegrationTests;

public class IdentityTests : BaseTest
{
    private const string Email = "yadayada@gmail.com";
    private const string Password = "NXD8fvw3pqc!uhr_mwn";

    public IdentityTests(CustomWebApplicationFactory apiFactory) : base(apiFactory)
    {
    }



    [Fact]
    public async Task Register_ValidUser_ReturnsOk()
    {
        // Arrange
        var request = new RegisterRequest() {Email = Email, Password = Password};
        
        // act
        var result = await PostAsync("api/Identity/Register", request);
        
        // assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public async Task Login_with_registered_account_should_return_access_token()
    {
        // arrange
        var request = new RegisterRequest() {Email = Email, Password = Password};
        await PostAsync("api/Identity/Register", request, ensureSuccess: true);
        
        // act
        var result = await PostAsync("api/Identity/Login", request);
        
        // assert
        result.Should().BeSuccessful();
        var payload = await result.Content.ReadFromJsonAsync<JsonElement>();
        payload.GetProperty("accessToken").GetString().Should().NotBeNullOrWhiteSpace();
    }
}