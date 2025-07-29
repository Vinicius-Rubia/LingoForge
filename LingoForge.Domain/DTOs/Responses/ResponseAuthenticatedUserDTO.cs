using LingoForge.Domain.Enums;

namespace LingoForge.Domain.DTOs.Responses;

public class ResponseAuthenticatedUserDTO
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public EUserRole Role { get; set; }
    public string Token { get; set; } = default!;
}
