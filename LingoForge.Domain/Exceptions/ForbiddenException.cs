using System.Net;

namespace LingoForge.Domain.Exceptions;

public class ForbiddenException(string message) : LingoForgeException(message)
{
    public override int StatusCode => (int)HttpStatusCode.Forbidden;

    public override List<string> GetErrors()
    {
        return [this.Message];
    }
}
