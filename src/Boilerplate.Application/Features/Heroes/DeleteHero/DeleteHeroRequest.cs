using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Heroes.DeleteHero;

public record DeleteHeroRequest(HeroId Id) : IRequest<bool>;