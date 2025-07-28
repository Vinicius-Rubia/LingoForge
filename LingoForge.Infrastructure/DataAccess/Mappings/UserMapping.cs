using LingoForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingoForge.Infrastructure.DataAccess.Mappings;

internal class UserMapping : BaseMapping<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.ToTable("users");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.HasIndex(x => x.Email)
           .IsUnique();

        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnType("CHAR(60)");

        builder.Property(x => x.Role)
           .IsRequired()
           .HasColumnType("INT");
    }
}
