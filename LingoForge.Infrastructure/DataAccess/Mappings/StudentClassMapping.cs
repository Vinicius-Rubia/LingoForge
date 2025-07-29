using LingoForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingoForge.Infrastructure.DataAccess.Mappings;

internal class StudentClassMapping : BaseMapping<StudentClass>
{
    public override void Configure(EntityTypeBuilder<StudentClass> builder)
    {
        base.Configure(builder);
        builder.ToTable("class_enrollments");

        builder.HasKey(ta => new { ta.TurmaId, ta.StudentId });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(ta => ta.StudentId)
            .OnDelete(DeleteBehavior.Cascade); // Se o aluno for deletado, a matrícula some
    }
}
