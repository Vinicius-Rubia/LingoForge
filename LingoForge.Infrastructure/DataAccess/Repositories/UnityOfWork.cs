using LingoForge.Domain.Interfaces.Repositories;

namespace LingoForge.Infrastructure.DataAccess.Repositories;

internal class UnityOfWork(LingoForgeDbContext dbContext) : IUnityOfWork
{
    private readonly LingoForgeDbContext _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
