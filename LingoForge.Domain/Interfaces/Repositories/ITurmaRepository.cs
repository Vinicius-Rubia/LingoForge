using LingoForge.Domain.Entities;

namespace LingoForge.Domain.Interfaces.Repositories;

public interface ITurmaRepository
{
    Task<IEnumerable<Turma>> GetClassesByStudentIdAsync(Guid studentId);
    Task AddAsync(Turma turma);
    Task<bool> ExistsByNameAndProfessorIdAsync(string name, Guid teacherId);
}
