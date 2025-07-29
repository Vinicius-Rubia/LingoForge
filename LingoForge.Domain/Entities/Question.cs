using LingoForge.Domain.Exceptions;

namespace LingoForge.Domain.Entities;

public class Question : BaseEntity
{
    public Guid ActivityId { get; private set; }
    public string Statement { get; private set; } // O enunciado da questão
    public int Order { get; private set; }

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

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Statement))
            throw new DomainException("Enunciado da questão é obrigatório.");

        if (Order <= 0)
            throw new DomainException("A ordem da questão deve ser maior que zero.");
    }
}
