namespace LingoForge.Domain.Entities;

public class Answer : BaseEntity
{
    public Guid ActivityId { get; private set; }
    public Guid StudentId { get; private set; }
    public string? FreeTextContent { get; private set; } // Para atividades de escrita livre

    // Para atividades de Q&A
    private readonly List<AnswerItem> _items = [];
    public IReadOnlyCollection<AnswerItem> Items => _items.AsReadOnly();

    // Construtor para Escrita Livre
    private Answer(Guid activityId, Guid studentId, string freeTextContent)
    {
        ActivityId = activityId;
        StudentId = studentId;
        FreeTextContent = freeTextContent;
    }

    // Construtor para Perguntas e Respostas
    private Answer(Guid activityId, Guid studentId, List<AnswerItem> items)
    {
        ActivityId = activityId;
        StudentId = studentId;
        _items.AddRange(items);
    }

    public static Answer Create(Guid activityId, Guid studentId, string freeTextContent)
    {
        return new Answer(activityId, studentId, freeTextContent);
    }

    public static Answer CreateWithQuestionAndAnswers(Guid activityId, Guid studentId, List<AnswerItem> items)
    {
        return new Answer(activityId, studentId, items);
    }
}
