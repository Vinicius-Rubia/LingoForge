using LingoForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingoForge.Infrastructure.DataAccess.Mappings;

internal class AnswerMapping : BaseMapping<Answer>
{
    public override void Configure(EntityTypeBuilder<Answer> builder)
    {
        base.Configure(builder);
        builder.ToTable("answers");

        builder.Property(s => s.FreeTextContent)
            .IsRequired(false) // Pode ser nulo para atividades de Q&A
            .HasColumnType("TEXT");

        // Relacionamento com Atividade: Uma resposta é para uma atividade
        builder.HasOne<Activity>()
               .WithMany() // Uma atividade pode ter muitas respostas
               .HasForeignKey(s => s.ActivityId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade); // Se a atividade for deletada, as respostas vão junto

        // Relacionamento com Aluno (User): Uma resposta é de um aluno
        builder.HasOne<User>()
               .WithMany() // Um aluno pode ter muitas respostas
               .HasForeignKey(s => s.StudentId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade); // Se o aluno for deletado, suas respostas vão junto

        // Relacionamento com RespostaItem: Uma resposta tem muitos itens
        builder.HasMany(s => s.Items)
               .WithOne() // Um item pertence a uma resposta (sem prop de navegação no item)
               .HasForeignKey(si => si.AnswerId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade); // Se a resposta for deletada, seus itens vão junto

        // ESSENCIAL: Configura o EF para acessar a coleção privada _items
        builder.Metadata.FindNavigation(nameof(Answer.Items))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}