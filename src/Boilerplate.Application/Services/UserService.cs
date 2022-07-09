using System;
using System.Threading.Tasks;
using AutoMapper;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.DTOs.User;
using Boilerplate.Application.Extensions;
using Boilerplate.Application.Features.Users;
using Boilerplate.Application.Filters;
using Boilerplate.Application.Interfaces;
using Boilerplate.Domain.Auth;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;


namespace Boilerplate.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _userRepository.Dispose();
        }
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        await _userRepository.Delete(id);
        return await _userRepository.SaveChangesAsync() > 0;
    }

    public async Task<PaginatedList<GetUserResponse>> GetAllUsers(GetUsersFilter filter)
    {
        filter ??= new GetUsersFilter();
        var users = _userRepository
            .GetAll()
            .WhereIf(!string.IsNullOrEmpty(filter.Email), x => EF.Functions.Like(x.Email, $"%{filter.Email}%"))
            .WhereIf(filter.IsAdmin, x => x.Role == Roles.Admin);
        return await _mapper.ProjectTo<GetUserResponse>(users).ToPaginatedListAsync(filter.CurrentPage, filter.PageSize);
    }
}