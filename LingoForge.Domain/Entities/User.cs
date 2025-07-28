using LingoForge.Domain.Enums;
using LingoForge.Domain.Exceptions;

namespace LingoForge.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string Email { get; init; } = default!;
    public string Password { get; private set; } = default!;
    public EUserRole Role { get; private set; } = EUserRole.STUDENT;

    private User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;

        Validate();
    }

    public static User Create(string name, string email, string password)
    {
        return new User(name, email, password);
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new DomainException("Nome é obrigatório.");

        int nameLength = Name.Trim().Length;

        if (nameLength < 6)
            throw new DomainException("Nome deve ter no mínimo 6 caracteres.");

        if (nameLength > 20)
            throw new DomainException("Nome deve ter no máximo 20 caracteres.");

        if (string.IsNullOrWhiteSpace(Email) || !Email.Contains('@') || !Email.Contains('.'))
            throw new DomainException("E-mail inválido.");

        if (string.IsNullOrWhiteSpace(Password))
            throw new DomainException("Senha não pode ser vazia.");

        if (!Enum.IsDefined(typeof(EUserRole), Role))
            throw new DomainException("Nível de usuário não é válido!");
    }
}
