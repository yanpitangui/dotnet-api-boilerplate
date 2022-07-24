using MediatR;
using System;

namespace Boilerplate.Application.Features.Users.DeleteUser;

public record DeleteUserRequest(Guid Id) : IRequest<bool>;