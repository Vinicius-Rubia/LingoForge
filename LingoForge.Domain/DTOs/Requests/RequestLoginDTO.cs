namespace LingoForge.Domain.DTOs.Requests;

public class RequestLoginDTO
{
    public string LoginIdentifier { get; set; } = default!;
    public string Password { get; set; } = default!;
}
