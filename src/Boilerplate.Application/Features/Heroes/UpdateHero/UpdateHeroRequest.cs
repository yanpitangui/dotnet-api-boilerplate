﻿using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Boilerplate.Application.Features.Heroes.UpdateHero;

public record UpdateHeroRequest : IRequest<GetHeroResponse?>
{
    [JsonIgnore]
    public Guid Id { get; init; }
    
    public string Name { get; init; } = null!;

    public string? Nickname { get; init; }
    public int? Age { get; init; }
    public string Individuality { get; init; } = null!;
    public HeroType HeroType { get; init; }

    public string? Team { get; init; }
}