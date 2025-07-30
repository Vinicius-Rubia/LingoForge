using FluentValidation;
using LingoForge.Domain.DTOs.Requests;

namespace LingoForge.Application.UseCases.Answers;

public class SubmitActivityValidator : AbstractValidator<RequestSubmitActivityDTO>
{
    public SubmitActivityValidator()
    {
        // Regra: ou FreeTextContent é fornecido, ou Answers é fornecido, mas não ambos.
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.FreeTextContent) ^ (x.Answers != null && x.Answers.Count != 0))
            .WithMessage("Forneça o conteúdo da escrita livre OU a lista de respostas, mas não ambos.");

        When(x => x.Answers != null, () =>
        {
            RuleFor(x => x.Answers).NotEmpty();
        });
    }
}
