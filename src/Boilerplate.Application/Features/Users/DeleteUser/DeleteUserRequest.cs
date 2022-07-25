using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Users.DeleteUser;

public record DeleteUserRequest(UserId Id) : IRequest<bool>;