using FluentValidation;
using LingoForge.Domain.DTOs.Requests;

namespace LingoForge.Application.UseCases.Turmas;

public class CreateClassValidator : AbstractValidator<RequestCreateClassDTO>
{
    public CreateClassValidator()
    {
        RuleFor(x => x.Name)
            .Length(6, 20)
            .WithMessage("O nome da turma deve ter entre 6 e 20 caracteres.");
    }
}
