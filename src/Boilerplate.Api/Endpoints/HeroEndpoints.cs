using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Heroes;
using Boilerplate.Application.Features.Heroes.CreateHero;
using Boilerplate.Application.Features.Heroes.DeleteHero;
using Boilerplate.Application.Features.Heroes.GetAllHeroes;
using Boilerplate.Application.Features.Heroes.GetHeroById;
using Boilerplate.Application.Features.Heroes.UpdateHero;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Boilerplate.Api.Endpoints;

public static class HeroEndpoints
{
    public static void MapHeroEndpoints(this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("api/Hero")
            .WithTags("Hero");

        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAllHeroesRequest request) =>
        {
            PaginatedList<GetHeroResponse> result = await mediator.Send(request);
            return result;
        });

        group.MapGet("{id}", async (IMediator mediator, HeroId id) =>
        {
            Result<GetHeroResponse> result = await mediator.Send(new GetHeroByIdRequest(id));
            return result.ToMinimalApiResult();
        });

        group.MapPost("/", async (IMediator mediator, CreateHeroRequest request) =>
        {
            Result<GetHeroResponse> result = await mediator.Send(request);
            return result.ToMinimalApiResult();
        });

        group.MapPut("{id}", async (IMediator mediator, HeroId id, UpdateHeroRequest request) =>
        {
            Result<GetHeroResponse> result = await mediator.Send(request with { Id = id });
            return result.ToMinimalApiResult();
        });

        group.MapDelete("{id}", async (IMediator mediator, HeroId id) =>
        {
            Result result = await mediator.Send(new DeleteHeroRequest(id));
            return result.ToMinimalApiResult();
        });
    }
}