using Boilerplate.Domain.Entities.Common;
using MediatR;
using OneOf;

namespace Boilerplate.Application.Features.Users.DeleteUser;

public record DeleteUserRequest(UserId Id) : IRequest<OneOf<bool, UserNotFound>>;