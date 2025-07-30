using LingoForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingoForge.Infrastructure.DataAccess.Mappings;

internal class AlternativeMapping : BaseMapping<Alternative>
{
    public override void Configure(EntityTypeBuilder<Alternative> builder)
    {
        base.Configure(builder);
        builder.ToTable("alternatives");

        builder.Property(a => a.Text).IsRequired();
        builder.Property(a => a.IsCorrect).IsRequired();
    }
}
