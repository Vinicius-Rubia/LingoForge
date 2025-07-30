using LingoForge.Domain.Entities;
using LingoForge.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LingoForge.Infrastructure.DataAccess.Repositories;

internal class AnswerRepository(LingoForgeDbContext dbContext) : IAnswerRepository
{
    private readonly LingoForgeDbContext _dbContext = dbContext;

    public async Task AddAsync(Answer resposta)
    {
        await _dbContext.Awnsers.AddAsync(resposta);
    }

    public async Task<bool> ExistsByActivityAndStudentAsync(Guid activityId, Guid studentId)
    {
        return await _dbContext.Awnsers
            .AnyAsync(r => r.ActivityId == activityId && r.StudentId == studentId);
    }
}
