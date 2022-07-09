using Boilerplate.Application.Common.Responses;
using System;
using System.Threading.Tasks;
using Boilerplate.Application.Features.Users;
using Boilerplate.Application.Filters;


namespace Boilerplate.Application.Interfaces;

public interface IUserService : IDisposable
{
    Task<bool> DeleteUser(Guid id);
    Task<PaginatedList<GetUserResponse>> GetAllUsers(GetUsersFilter filter);
}