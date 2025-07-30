using LingoForge.Domain.Entities;
using LingoForge.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LingoForge.Infrastructure.DataAccess.Repositories;

internal class ActivityRepository(LingoForgeDbContext dbContext) : IActivityRepository
{
    private readonly LingoForgeDbContext _dbContext = dbContext;

    public async Task AddAsync(Activity activity)
    {
        await _dbContext.Activities.AddAsync(activity);
    }

    public async Task<Activity?> GetByIdWithDetailsAsync(Guid activityId)
    {
        return await _dbContext.Activities
            .Include(a => a.Questions)
                .ThenInclude(q => q.Alternatives)
            .FirstOrDefaultAsync(a => a.Id == activityId);
    }
}
