using System.Net;

namespace LingoForge.Domain.Exceptions;

public class DomainException(string message) : LingoForgeException(message)
{
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return [this.Message];
    }
}