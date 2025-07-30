using LingoForge.Domain.Entities;

namespace LingoForge.Domain.Interfaces.Repositories;

public interface IAnswerRepository
{
    Task<bool> ExistsByActivityAndStudentAsync(Guid activityId, Guid studentId);
    Task AddAsync(Answer resposta);
}
