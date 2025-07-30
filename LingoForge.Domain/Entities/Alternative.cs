using LingoForge.Domain.Enums;
using LingoForge.Domain.Exceptions;

namespace LingoForge.Domain.Entities;

public class Alternative : BaseEntity
{
    public Guid QuestaoId { get; private set; }
    public string Text { get; private set; } = default!;
    public bool IsCorrect { get; private set; }

    private Alternative(Guid questaoId, string text, bool isCorrect)
    {
        QuestaoId = questaoId;
        Text = text;
        IsCorrect = isCorrect;

        Validate();
    }

    public static Alternative Create(Guid questaoId, string text, bool isCorrect)
    {
        return new Alternative(questaoId, text, isCorrect);
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Text))
            throw new DomainException("O texto da alternativa não pode ser vazio.");
    }
}
