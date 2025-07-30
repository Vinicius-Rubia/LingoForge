using LingoForge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LingoForge.Infrastructure.DataAccess.Mappings;

internal class AnswerItemMapping : BaseMapping<AnswerItem>
{
    public override void Configure(EntityTypeBuilder<AnswerItem> builder)
    {
        base.Configure(builder);
        builder.ToTable("answer_items");

        builder.Property(s => s.AnswerText)
            .IsRequired(false)
            .HasColumnType("TEXT");

        builder.Property(si => si.ChosenAlternativeId)
               .IsRequired(false);

        // Relacionamento com Questao: Um item de resposta se refere a uma questão
        builder.HasOne<Question>()
               .WithMany() // Uma questão pode estar em muitos itens de resposta
               .HasForeignKey(si => si.QuestionId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict); // IMPORTANTE: Não deixa deletar uma Questão se ela já foi respondida.
                                                   // Isso previne que dados históricos sejam corrompidos.

        builder.HasOne<Alternative>()
               .WithMany()
               .HasForeignKey(si => si.ChosenAlternativeId)
               .IsRequired(false) // Confirma que a FK é opcional
               .OnDelete(DeleteBehavior.Restrict);
    }
}
