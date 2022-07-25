using Boilerplate.Domain.Entities.Common;
using MediatR;
using OneOf;

namespace Boilerplate.Application.Features.Heroes.DeleteHero;

public record DeleteHeroRequest(HeroId Id) : IRequest<OneOf<bool, HeroNotFound>>;