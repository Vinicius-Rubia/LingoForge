using LingoForge.Domain.Entities;
using LingoForge.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LingoForge.Infrastructure.DataAccess.Repositories;

internal class TurmaRepository(LingoForgeDbContext dbContext) : ITurmaRepository
{
    private readonly LingoForgeDbContext _dbContext = dbContext;

    public async Task AddAsync(Turma turma)
    {
        await _dbContext.Classes.AddAsync(turma);
    }

    public async Task<bool> ExistsByNameAndProfessorIdAsync(string name, Guid teacherId)
    {
        return await _dbContext.Classes
            .AnyAsync(t => t.Name.ToLower() == name.ToLower() && t.TeacherId == teacherId);
    }

    public async Task<Turma?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Classes
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Turma?> GetByIdWithEnrollmentsAsync(Guid classId)
    {
        return await _dbContext.Classes
            .Include(t => t.Enrollments)
            .FirstOrDefaultAsync(t => t.Id == classId);
    }

    public async Task<IEnumerable<Turma>> GetClassesByStudentIdAsync(Guid studentId)
    {
        return await _dbContext.Classes
            .Where(t => t.Enrollments.Any(e => e.StudentId == studentId))
            .ToListAsync();
    }
}
