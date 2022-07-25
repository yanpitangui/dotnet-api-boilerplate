﻿using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using System.Threading;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Application.Features.Users.UpdatePassword;

public class UpdatePasswordHandler : IRequestHandler<UpdatePasswordRequest, GetUserResponse>
{
    private readonly IContext _context;

    private readonly IMapper _mapper;

    public UpdatePasswordHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    public async Task<GetUserResponse> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
    {
        // Guaranteed to be valid, because it comes from the session.
        var originalUser = await _context.Users
            .FirstAsync(x => x.Id == request.Id, cancellationToken);
        originalUser.Password = BC.HashPassword(request.Password);
        _context.Users.Update(originalUser);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GetUserResponse>(originalUser);
    }
}