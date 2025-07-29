using LingoForge.Domain.Enums;
using LingoForge.Domain.Exceptions;

namespace LingoForge.Domain.Entities;

public class Activity : BaseEntity
{
    public string Title { get; private set; }
    public string? Instructions { get; private set; } // Instruções
    public EActivityType Type { get; private set; }
    public Guid TurmaId { get; private set; } // FK para a Turma

    // Para atividades do tipo Q&A
    private readonly List<Question> _questions = [];
    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

    private Activity(string title, EActivityType type, Guid turmaId, string? instructions)
    {
        Title = title;
        Type = type;
        TurmaId = turmaId;
        Instructions = instructions;

        Validate();
    }

    public static Activity Create(string title, EActivityType type, Guid turmaId, string? instructions)
    {
        return new Activity(title, type, turmaId, instructions);
    }

    public void AddQuestion(string statement)
    {
        if (Type != EActivityType.QUESTION_AND_ANSWER)
            throw new DomainException("Questões só podem ser adicionadas a atividades do tipo 'Perguntas e Respostas'.");

        var order = _questions.Count + 1;
        _questions.Add(Question.Create(Id, statement, order));

        UpdatedAt = DateTime.UtcNow;
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new DomainException("Título é obrigatório.");

        int titleLength = Title.Trim().Length;

        if (titleLength < 6)
            throw new DomainException("Nome deve ter no mínimo 6 caracteres.");

        if (titleLength > 20)
            throw new DomainException("Nome deve ter no máximo 20 caracteres.");

        if (!Enum.IsDefined(typeof(EActivityType), Type))
            throw new DomainException("Tipo da atividade não é válido!");
    }
}
