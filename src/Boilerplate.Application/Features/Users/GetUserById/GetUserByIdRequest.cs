using Boilerplate.Domain.Entities.Common;
using MediatR;
using OneOf;

namespace Boilerplate.Application.Features.Users.GetUserById;

public record GetUserByIdRequest(UserId Id) : IRequest<OneOf<GetUserResponse, UserNotFound>>;