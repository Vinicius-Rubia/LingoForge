using LingoForge.Domain.Entities;
using LingoForge.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LingoForge.Infrastructure.Security.Tokens;

internal class JwtTokenGenerator(uint expirationTimeMinutes, string signingkey) : IJwtTokenGenerator
{
    private readonly uint _expirationTimeMinutes = expirationTimeMinutes;
    private readonly string _signingkey = signingkey;

    public string Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(ClaimTypes.Name, user.Name),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_signingkey);
        return new SymmetricSecurityKey(key);
    }
}
