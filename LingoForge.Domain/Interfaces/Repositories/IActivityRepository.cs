using LingoForge.Domain.Entities;

namespace LingoForge.Domain.Interfaces.Repositories;

public interface IActivityRepository
{
    Task AddAsync(Activity activity);
}
