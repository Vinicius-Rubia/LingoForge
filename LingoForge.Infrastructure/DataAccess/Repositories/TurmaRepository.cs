using LingoForge.Domain.Entities;
using LingoForge.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LingoForge.Infrastructure.DataAccess.Repositories;

internal class TurmaRepository(LingoForgeDbContext dbContext) : ITurmaRepository
{
    private readonly LingoForgeDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Turma>> GetClassesByStudentIdAsync(Guid studentId)
    {
        return await _dbContext.Classes
            .Where(t => t.Enrollments.Any(e => e.StudentId == studentId))
            .ToListAsync();
    }
}
