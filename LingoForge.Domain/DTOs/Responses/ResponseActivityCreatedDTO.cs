using LingoForge.Domain.Enums;

namespace LingoForge.Domain.DTOs.Responses;

public class ResponseActivityCreatedDTO
{
    public Guid Id { get; set; }
    public Guid ClassId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public EActivityType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<ResponseQuestionDTO> Questions { get; set; } = [];
}
