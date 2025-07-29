using LingoForge.Domain.DTOs.Requests;

namespace LingoForge.Domain.Interfaces.UseCases.Students;

public interface IAddStudentsToClassUseCase
{
    Task Execute(Guid classId, RequestAddStudentsToClassDTO request);
}
