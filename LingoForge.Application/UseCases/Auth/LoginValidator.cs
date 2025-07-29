using FluentValidation;
using LingoForge.Domain.DTOs.Requests;

namespace LingoForge.Application.UseCases.Auth;

public class LoginValidator : AbstractValidator<RequestLoginDTO>
{
    public LoginValidator()
    {
        RuleFor(x => x.LoginIdentifier).NotEmpty().WithMessage("E-mail ou nome de usuário é obrigatório.");

        RuleFor(x => x.Password)
            .Length(6, 20)
            .WithMessage("A senha deve ter entre 6 e 20 caracteres.");
    }
}
