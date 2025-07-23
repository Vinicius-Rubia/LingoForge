using System.Net;

namespace LingoForge.Domain.Exceptions;

public class ConflictException(string message) : LingoForgeException(message)
{
    public override int StatusCode => (int)HttpStatusCode.Conflict;

    public override List<string> GetErrors()
    {
        return [this.Message];
    }
}