using LingoForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingoForge.Infrastructure.DataAccess.Mappings;

internal class TurmaMapping : BaseMapping<Turma>
{
    public override void Configure(EntityTypeBuilder<Turma> builder)
    {
        base.Configure(builder);
        builder.ToTable("classes");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.Description)
            .HasColumnType("TEXT");

        // Relacionamento 1-N: Uma turma tem um professor
        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(x => x.TeacherId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict); // Não deixa deletar um professor se ele tiver turmas

        // Relacionamento N-N com User (Alunos) através da entidade TurmaAluno
        // Configura o acesso ao campo privado _enrollments
        builder.HasMany(x => x.Enrollments)
               .WithOne()
               .HasForeignKey(ta => ta.TurmaId)
               .OnDelete(DeleteBehavior.Cascade); // Se a turma for deletada, as matrículas somem

        builder.Metadata.FindNavigation(nameof(Turma.Enrollments))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
