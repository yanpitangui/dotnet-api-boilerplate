using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Heroes.DeleteHero;

public record DeleteHeroRequest(HeroId Id) : IRequest<Result>;