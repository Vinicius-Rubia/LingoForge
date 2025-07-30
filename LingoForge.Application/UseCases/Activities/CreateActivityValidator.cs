using FluentValidation;
using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.Enums;

namespace LingoForge.Application.UseCases.Activities;

public class CreateActivityValidator : AbstractValidator<RequestCreateActivityDTO>
{
    public CreateActivityValidator()
    {
        RuleFor(x => x.Title)
            .Length(6, 20)
            .WithMessage("O título da atividade precisa estar entre 6 e 20 caracteres.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Tipo de atividade inválido.");

        // Validação condicional: Questions só é obrigatório para Q&A
        When(x => x.Type == EActivityType.QUESTION_AND_ANSWER, () =>
        {
            RuleFor(x => x.Questions)
                .NotEmpty()
                .ForEach(questionRule =>
                {
                    questionRule.ChildRules(q =>
                    {
                        q.RuleFor(x => x.Statement).NotEmpty();
                        q.RuleFor(x => x.Alternatives)
                            .NotEmpty().WithMessage("Cada questão deve ter alternativas.")
                            // VALIDAÇÃO CRUCIAL: Garante a regra de negócio já na entrada de dados.
                            .Must(alts => alts.Count(a => a.IsCorrect) == 1)
                            .WithMessage("Cada questão deve ter exatamente uma alternativa correta.");
                    });
                });
        });

        // Validação condicional: Questions deve ser nulo ou vazio para Escrita Livre
        When(x => x.Type == EActivityType.FREE_WRITING, () =>
        {
            RuleFor(x => x.Questions)
                .Empty().WithMessage("Para atividades de escrita livre, a lista de questões deve estar vazia.");
        });
    }
}
