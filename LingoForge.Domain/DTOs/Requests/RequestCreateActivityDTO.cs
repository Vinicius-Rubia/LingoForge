using LingoForge.Domain.Enums;

namespace LingoForge.Domain.DTOs.Requests;

public class RequestCreateActivityDTO
{
    public string Title { get; set; } = default!;
    public string? Instructions { get; set; }
    public EActivityType Type { get; set; }

    // Esta lista só será preenchida se o tipo for QuestionAndAnswer
    public List<RequestQuestionDTO>? Questions { get; set; }
}
