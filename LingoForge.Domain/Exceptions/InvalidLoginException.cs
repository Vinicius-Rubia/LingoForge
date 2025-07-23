using System.Net;

namespace LingoForge.Domain.Exceptions;

public class InvalidLoginException : LingoForgeException
{
    public InvalidLoginException() : base("E-mail e/ou senha são inválidos!") { }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [this.Message];
    }
}
