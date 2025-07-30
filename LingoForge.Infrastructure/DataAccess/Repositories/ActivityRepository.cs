using LingoForge.Domain.Entities;
using LingoForge.Domain.Interfaces.Repositories;

namespace LingoForge.Infrastructure.DataAccess.Repositories;

internal class ActivityRepository(LingoForgeDbContext dbContext) : IActivityRepository
{
    private readonly LingoForgeDbContext _dbContext = dbContext;

    public async Task AddAsync(Activity activity)
    {
        await _dbContext.Activities.AddAsync(activity);
    }
}
