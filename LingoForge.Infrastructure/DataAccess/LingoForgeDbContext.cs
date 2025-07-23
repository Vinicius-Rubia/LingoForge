using LingoForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LingoForge.Infrastructure.DataAccess;

internal class LingoForgeDbContext(DbContextOptions<LingoForgeDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
       => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
