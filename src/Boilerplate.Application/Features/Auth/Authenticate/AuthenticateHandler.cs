using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;


namespace Boilerplate.Application.Features.Auth.Authenticate;

public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, Jwt?>
{
    private readonly IContext _context;
    
    private readonly TokenConfiguration _appSettings;
    
    public AuthenticateHandler(IOptions<TokenConfiguration> appSettings, IContext context)
    {
        _context = context;
        _appSettings = appSettings.Value;

    }

    public async Task<Jwt?> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == request.Email.ToLower(), cancellationToken);
        if (user == null || !BC.Verify(request.Password, user.Password))
        {
            return null;
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var claims = new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role)
        });

        var expDate = DateTime.UtcNow.AddHours(1);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = expDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = _appSettings.Audience,
            Issuer = _appSettings.Issuer
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new Jwt
        {
            Token = tokenHandler.WriteToken(token),
            ExpDate = expDate
        };
    }
}