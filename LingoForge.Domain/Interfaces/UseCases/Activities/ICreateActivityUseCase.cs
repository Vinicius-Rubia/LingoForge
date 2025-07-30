using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;

namespace LingoForge.Domain.Interfaces.UseCases.Activities;

public interface ICreateActivityUseCase
{
    Task<ResponseActivityCreatedDTO> Execute(Guid classId, RequestCreateActivityDTO request);
}
