namespace LingoForge.Domain.Exceptions;

public abstract class LingoForgeException(string message) : SystemException(message)
{
    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
