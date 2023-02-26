using Boilerplate.Api.IntegrationTests.Common;
using Boilerplate.Application.Features.Heroes.GetAllHeroes;
using FluentAssertions;
using System.Linq;
using System.Net;

namespace Boilerplate.Api.IntegrationTests;

public class ExceptionHandlerTest : BaseTest
{
    public ExceptionHandlerTest(CustomWebApplicationFactory apiFactory) : base(apiFactory)
    {
    }


    [Fact]
    public async Task Forced_Error_Should_Return_InternalServerError()
    {
        // Since this get request input is not validated, should cause a error
        // act
        var response = await GetAsync("/api/Hero", new GetAllHeroesRequest()
        {
            Name = string.Join("", Enumerable.Repeat("a", 5000))
        });  
        
        // assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}