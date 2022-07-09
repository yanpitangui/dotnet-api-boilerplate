using MediatR;
using System;

namespace Boilerplate.Application.Features.Users.GetUserById;

public record GetUserByIdRequest(Guid Id) : IRequest<GetUserResponse>;