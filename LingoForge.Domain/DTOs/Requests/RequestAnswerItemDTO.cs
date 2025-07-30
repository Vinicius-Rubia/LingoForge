namespace LingoForge.Domain.DTOs.Requests;

public record RequestAnswerItemDTO
{
    public Guid QuestionId { get; set; }
    public Guid ChosenAlternativeId { get; set; }
}
