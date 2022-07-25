using Boilerplate.Domain.Entities.Common;
using MediatR;
using OneOf;

namespace Boilerplate.Application.Features.Heroes.GetHeroById;

public record GetHeroByIdRequest(HeroId Id) : IRequest<OneOf<GetHeroResponse, HeroNotFound>>;