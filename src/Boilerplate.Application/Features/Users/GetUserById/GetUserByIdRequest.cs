using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Users.GetUserById;

public record GetUserByIdRequest(UserId Id) : IRequest<GetUserResponse?>;