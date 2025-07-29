using FluentValidation;
using LingoForge.Domain.DTOs.Requests;

namespace LingoForge.Application.UseCases.Users;

public class CreateAccountValidator : AbstractValidator<RequestCreateAccountDTO>
{
    public CreateAccountValidator()
    {
        RuleFor(x => x.Name)
            .Length(6, 20)
            .WithMessage("O nome deve ter entre 6 e 20 caracteres.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("E-mail inválido.");

        RuleFor(x => x.Password)
            .Length(6, 20)
            .WithMessage("A senha deve ter entre 6 e 20 caracteres.");
    }
}
