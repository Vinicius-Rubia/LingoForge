using LingoForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LingoForge.Infrastructure.DataAccess;

internal class LingoForgeDbContext(DbContextOptions<LingoForgeDbContext> options) : DbContext(options)
{
    // DbSet para cada RAIZ DE AGREGADO.
    // Você não precisa de um DbSet para entidades filhas como Questao ou RespostaItem,
    // pois elas serão acessadas e persistidas através de suas raízes (Atividade e Resposta).
    public DbSet<User> Users { get; set; }
    public DbSet<Turma> Classes { get; set; }
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Answer> Awnsers { get; set; }

    // DbSet para a entidade de junção que tem chave primária composta
    // e precisa ser manipulada diretamente em alguns cenários.
    public DbSet<StudentClass> ClassEnrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
       => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
