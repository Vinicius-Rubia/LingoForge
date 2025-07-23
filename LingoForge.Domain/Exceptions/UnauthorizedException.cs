using System.Net;

namespace LingoForge.Domain.Exceptions;

public class UnauthorizedException() : LingoForgeException("Você não tem permissão para realizar esta ação.")
{
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [this.Message];
    }
}
