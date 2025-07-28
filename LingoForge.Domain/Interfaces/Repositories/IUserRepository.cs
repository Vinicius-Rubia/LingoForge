using LingoForge.Domain.Entities;

namespace LingoForge.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByNameAsync(string name);
    Task<User?> GetByEmailOrUsernameAsync(string identifier);
}
