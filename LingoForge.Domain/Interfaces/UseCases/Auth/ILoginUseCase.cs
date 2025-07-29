using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;

namespace LingoForge.Domain.Interfaces.UseCases.Auth;

public interface ILoginUseCase
{
    Task<ResponseAuthenticatedUserDTO> Execute(RequestLoginDTO request);
}
