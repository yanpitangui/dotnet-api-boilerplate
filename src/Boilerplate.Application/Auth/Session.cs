using Boilerplate.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using ISession = Boilerplate.Domain.Auth.Interfaces.ISession;

namespace Boilerplate.Application.Auth;

public class Session : ISession
{
    public Session(IHttpContextAccessor httpContextAccessor)
    {
        ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;

        Claim? nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);

        if (nameIdentifier != null)
        {
            UserId = new Guid(nameIdentifier.Value);
        }
    }

    public UserId UserId { get; }

    public DateTime Now => DateTime.Now;
}