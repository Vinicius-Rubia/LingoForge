using System.Net;

namespace LingoForge.Domain.Exceptions;

public class NotFoundException(string message) : LingoForgeException(message)
{
    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [this.Message];
    }
}

