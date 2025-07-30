using LingoForge.Domain.Exceptions;

namespace LingoForge.Domain.Entities;

public class AnswerItem : BaseEntity
{
    public Guid AnswerId { get; private set; }
    public Guid QuestionId { get; private set; } // FK para a Questão original
    public string? AnswerText { get; private set; }
    public Guid? ChosenAlternativeId { get; private set; }

    // Construtor para resposta dissertativa
    private AnswerItem(Guid answerId, Guid questionId, string answerText)
    {
        AnswerId = answerId;
        QuestionId = questionId;
        AnswerText = answerText;

        Validate();
    }

    // Construtor para resposta de múltipla escolha
    public AnswerItem(Guid answerId, Guid questionId, Guid chosenAlternativeId)
    {
        AnswerId = answerId;
        QuestionId = questionId;
        ChosenAlternativeId = chosenAlternativeId;
    }

    public static AnswerItem Create(Guid answerId, Guid questionId, string answerText)
    {
        return new AnswerItem(answerId, questionId, answerText);
    }

    public static AnswerItem CreateMultiplyAnswerItems(Guid answerId, Guid questionId, Guid chosenAlternativeId)
    {
        return new AnswerItem(answerId, questionId, chosenAlternativeId);
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(AnswerText))
            throw new DomainException("Texto de resposta é obrigatório.");
    }
}
