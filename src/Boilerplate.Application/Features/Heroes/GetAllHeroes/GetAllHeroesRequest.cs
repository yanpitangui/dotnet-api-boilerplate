using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.Heroes.GetAllHeroes;

public record GetAllHeroesRequest : PaginatedRequest, IRequest<PaginatedList<GetHeroResponse>>
{
    public string? Name { get; init; }
    public string? Nickname { get; init; }

    public int? Age { get; init; }

    public string? Individuality { get; init; }
    public HeroType? HeroType { get; init; }

    public string? Team { get; init; }
}