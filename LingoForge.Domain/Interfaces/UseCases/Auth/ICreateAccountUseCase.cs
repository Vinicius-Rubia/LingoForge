using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;

namespace LingoForge.Domain.Interfaces.UseCases.Auth;

public interface ICreateAccountUseCase
{
    Task<ResponseCreatedUserDTO> Execute(RequestCreateAccountDTO request);
}
