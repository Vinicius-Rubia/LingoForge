using LingoForge.Domain.Entities;
using LingoForge.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LingoForge.Infrastructure.DataAccess.Repositories;

internal class UserRepository(LingoForgeDbContext dbContext) : IUserRepository
{
    private readonly LingoForgeDbContext _dbContext = dbContext;

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public void Delete(User user)
    {
        _dbContext.Users.Remove(user);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _dbContext.Users.AnyAsync(x => x.Name == name);
    }

    public async Task<User?> GetByEmailOrUsernameAsync(string identifier)
    {
        var lowerIdentifier = identifier.ToLower();

        return await _dbContext.Users
            .FirstOrDefaultAsync(x =>
                x.Email.ToLower() == lowerIdentifier ||
                x.Name.ToLower() == lowerIdentifier);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }
}
