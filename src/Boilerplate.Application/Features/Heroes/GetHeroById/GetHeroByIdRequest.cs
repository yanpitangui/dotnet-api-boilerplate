using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Heroes.GetHeroById;

public record GetHeroByIdRequest(HeroId Id) : IRequest<GetHeroResponse?>;