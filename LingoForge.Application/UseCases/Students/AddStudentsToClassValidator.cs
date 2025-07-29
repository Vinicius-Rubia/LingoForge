using FluentValidation;
using LingoForge.Domain.DTOs.Requests;

namespace LingoForge.Application.UseCases.Students;

public class AddStudentsToClassValidator : AbstractValidator<RequestAddStudentsToClassDTO>
{
    public AddStudentsToClassValidator()
    {
        RuleFor(x => x.StudentIds)
            .NotEmpty().WithMessage("É necessário fornecer pelo menos um ID de aluno.")
            .ForEach(id => id.NotEmpty().WithMessage("O ID do aluno não pode ser vazio."));
    }
}