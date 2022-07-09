using MediatR;
using System;

namespace Boilerplate.Application.Features.Heroes.GetHeroById;

public record GetHeroByIdRequest(Guid Id) : IRequest<GetHeroResponse?>;