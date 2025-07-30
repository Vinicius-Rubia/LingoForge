using LingoForge.Domain.Entities;

namespace LingoForge.Domain.Interfaces.Repositories;

public interface ITurmaRepository
{
    Task<IEnumerable<Turma>> GetClassesByStudentIdAsync(Guid studentId);
    Task AddAsync(Turma turma);
    Task<bool> ExistsByNameAndProfessorIdAsync(string name, Guid teacherId);
    Task<Turma?> GetByIdWithEnrollmentsAsync(Guid classId);
    Task<Turma?> GetByIdAsync(Guid id);
    Task<bool> IsStudentEnrolledInClassAsync(Guid studentId, Guid classId);
}
