namespace LingoForge.Domain.Interfaces.UseCases.Students;

public interface IRemoveStudentFromClassUseCase
{
    Task Execute(Guid classId, Guid studentId);
}
