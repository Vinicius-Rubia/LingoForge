using LingoForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingoForge.Infrastructure.DataAccess.Mappings;

internal class ActivityMapping : BaseMapping<Activity>
{
    public override void Configure(EntityTypeBuilder<Activity> builder)
    {
        base.Configure(builder);
        builder.ToTable("activities");

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.Instructions)
           .HasColumnType("TEXT");

        builder.Property(x => x.Type)
           .IsRequired()
           .HasColumnType("INT");

        // Relacionamento 1-N: Uma atividade pertence a uma turma
        builder.HasOne<Turma>()
               .WithMany()
               .HasForeignKey(a => a.TurmaId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade); // Se a turma for deletada, as atividades também

        // Relacionamento 1-N: Uma atividade tem muitas questões
        builder.HasMany(x => x.Questions)
               .WithOne()
               .HasForeignKey(q => q.ActivityId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(Activity.Questions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
