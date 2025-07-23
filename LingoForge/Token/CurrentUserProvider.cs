using LingoForge.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LingoForge.Token;

public class CurrentUserProvider(IHttpContextAccessor httpContextAccessor) : IUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public Guid? GetUserIdentifier()
    {
        var identifierClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Sid);
        if (Guid.TryParse(identifierClaim, out var identifier))
            return identifier;
        return null;
    }
}
