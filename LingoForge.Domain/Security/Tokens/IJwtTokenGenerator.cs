using LingoForge.Domain.Entities;

namespace LingoForge.Domain.Security.Tokens;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}
