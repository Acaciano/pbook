using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PBook.Domain.Entidades;

namespace PBook.UI.Data.Map
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Titulo).HasMaxLength(255);
            builder.Property(x => x.Editora).HasMaxLength(150);
            builder.Property(x => x.Edicao).HasMaxLength(10);

            builder.HasMany(x => x.Autores);
            builder.HasMany(x => x.Assuntos);
        }
    }
}
