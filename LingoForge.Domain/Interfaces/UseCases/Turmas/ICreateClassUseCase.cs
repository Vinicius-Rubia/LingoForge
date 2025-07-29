using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;

namespace LingoForge.Domain.Interfaces.UseCases.Turmas;

public interface ICreateClassUseCase
{
    Task<ResponseClassCreatedDTO> Execute(RequestCreateClassDTO request);
}
