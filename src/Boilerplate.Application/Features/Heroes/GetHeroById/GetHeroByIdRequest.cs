using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Heroes.GetHeroById;

public record GetHeroByIdRequest(HeroId Id) : IRequest<Result<GetHeroResponse>>;