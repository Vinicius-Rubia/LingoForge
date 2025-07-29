namespace LingoForge.Domain.Interfaces.UseCases.Users;

public interface IDeleteStudentAccountUseCase
{
    Task Execute(Guid studentId);
}
