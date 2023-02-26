using Ardalis.Result;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Users.GetUserById;

public record GetUserByIdRequest(UserId Id) : IRequest<Result<GetUserResponse>>;