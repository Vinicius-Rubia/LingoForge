using LingoForge.Domain.DTOs.Requests;
using LingoForge.Domain.DTOs.Responses;

namespace LingoForge.Domain.Interfaces.UseCases.Answers;

public interface ISubmitActivityUseCase
{
    Task<ResponseSubmissionDTO> Execute(Guid activityId, RequestSubmitActivityDTO request);
}
