using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Users.DeleteUser;

public record DeleteUserRequest(UserId Id) : IRequest<Result>;