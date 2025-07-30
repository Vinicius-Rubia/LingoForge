using LingoForge.Domain.Exceptions;

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

    // Método para o aluno registrar sua escolha em uma questão de múltipla escolha
    public void AddChoice(Question question, Guid chosenAlternativeId)
    {
        // Regra de negócio: a alternativa escolhida deve pertencer à questão.
        if (!question.Alternatives.Any(a => a.Id == chosenAlternativeId))
            throw new DomainException("A alternativa escolhida não pertence a esta questão.");

        // Evita respostas duplicadas para a mesma questão
        if (_items.Any(i => i.QuestionId == question.Id)) return;

        _items.Add(AnswerItem.CreateMultiplyAnswerItems(Id, question.Id, chosenAlternativeId));
        UpdatedAt = DateTime.UtcNow;
    }

    // Método para o aluno registrar sua resposta dissertativa (já existente, mas agora mais explícito)
    public void AddWrittenAnswer(Question question, string answerText)
    {
        if (_items.Any(i => i.QuestionId == question.Id)) return;
        _items.Add(AnswerItem.Create(Id, question.Id, answerText));
        UpdatedAt = DateTime.UtcNow;
    }
}
