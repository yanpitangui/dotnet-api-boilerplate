using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Heroes.UpdateHero;

public record UpdateHeroRequest : IRequest<Result<GetHeroResponse>>
{
    [JsonIgnore]
    public HeroId Id { get; init; }
    
    public string Name { get; init; } = null!;

    public string? Nickname { get; init; }
    public int? Age { get; init; }
    public string Individuality { get; init; } = null!;
    public HeroType HeroType { get; init; }

    public string? Team { get; init; }
}