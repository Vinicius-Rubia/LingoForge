using LingoForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingoForge.Infrastructure.DataAccess.Mappings;

internal class QuestionMapping : BaseMapping<Question>
{
    public override void Configure(EntityTypeBuilder<Question> builder)
    {
        base.Configure(builder);
        builder.ToTable("questions");

        builder.Property(q => q.Statement)
              .IsRequired() // O enunciado é um texto longo, não precisa de MaxLength (será 'text' no Postgres)
              .HasColumnType("TEXT");

        builder.Property(q => q.Order)
               .IsRequired()
               .HasColumnType("INT");
    }
}
