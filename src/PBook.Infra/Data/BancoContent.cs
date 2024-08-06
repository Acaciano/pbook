using Microsoft.EntityFrameworkCore;
using PBook.Domain.Entidades;
using PBook.UI.Data.Map;

namespace PBook.Infra
{
    public class BancoContent : DbContext
    {
        public BancoContent(DbContextOptions<BancoContent> options) :
            base(options)
        {
        }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<LivroAutor> LivroAutores { get; set; }
        public DbSet<LivroAssunto> LivroAssuntos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LivroMap());
            modelBuilder.ApplyConfiguration(new AutorMap());
            modelBuilder.ApplyConfiguration(new AssuntoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
