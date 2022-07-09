using MediatR;
using System;

namespace Boilerplate.Application.Features.Heroes.DeleteHero;

public record DeleteHeroRequest(Guid Id) : IRequest<bool>;