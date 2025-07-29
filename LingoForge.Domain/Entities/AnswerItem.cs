using LingoForge.Domain.Exceptions;

namespace LingoForge.Domain.Entities;

public class AnswerItem : BaseEntity
{
    public Guid AnswerId { get; private set; }
    public Guid QuestionId { get; private set; } // FK para a Questão original
    public string AnswerText { get; private set; }

    private AnswerItem(Guid answerId, Guid questionId, string answerText)
    {
        AnswerId = answerId;
        QuestionId = questionId;
        AnswerText = answerText;

        Validate();
    }

    public static AnswerItem Create(Guid answerId, Guid questionId, string answerText)
    {
        return new AnswerItem(answerId, questionId, answerText);
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(AnswerText))
            throw new DomainException("Texto de resposta é obrigatório.");
    }
}
