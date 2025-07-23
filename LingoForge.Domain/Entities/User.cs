using LingoForge.Domain.Enums;

namespace LingoForge.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; private set; } = default!;
    public EUserRole Role { get; private set; } = EUserRole.STUDENT;
}
