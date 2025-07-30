using LingoForge.Domain.Exceptions;

namespace LingoForge.Domain.Entities;

public class Question : BaseEntity
{
    public Guid ActivityId { get; private set; }
    public string Statement { get; private set; } // O enunciado da questão
    public int Order { get; private set; }

    private readonly List<Alternative> _alternatives = [];
    public IReadOnlyCollection<Alternative> Alternatives => _alternatives.AsReadOnly();

    private Question(Guid activityId, string statement, int order)
    {
        ActivityId = activityId;
        Statement = statement;
        Order = order;

        Validate();
    }

    public static Question Create(Guid activityId, string statement, int order)
    {
        return new Question(activityId, statement, order);
    }

    public void AddAlternative(string text, bool isCorrect)
    {
        if (isCorrect && _alternatives.Any(a => a.IsCorrect))
            throw new DomainException("Uma questão só pode ter uma alternativa correta.");

        _alternatives.Add(Alternative.Create(Id, text, isCorrect));
        UpdatedAt = DateTime.UtcNow;
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Statement))
            throw new DomainException("Enunciado da questão é obrigatório.");

        if (Order <= 0)
            throw new DomainException("A ordem da questão deve ser maior que zero.");
    }
}
