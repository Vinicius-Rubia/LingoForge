namespace LingoForge.Domain.DTOs.Requests;

public class RequestAddStudentsToClassDTO
{
    public List<Guid> StudentIds { get; set; } = [];
}
