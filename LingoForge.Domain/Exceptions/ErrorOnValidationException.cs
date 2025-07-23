using System.Net;

namespace LingoForge.Domain.Exceptions;

public class ErrorOnValidationException(List<string> errorMessages) : LingoForgeException(string.Empty)
{
    private readonly List<string> _errors = errorMessages;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public override List<string> GetErrors()
    {
        return _errors;
    }
}
