using LingoForge.Domain.Enums;

namespace LingoForge.Domain.DTOs.Responses;

public class ResponseCreatedUserDTO
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public EUserRole Role { get; set; }
}
