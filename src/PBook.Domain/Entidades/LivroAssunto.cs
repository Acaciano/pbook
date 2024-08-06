namespace PBook.Domain.Entidades
{
    public class LivroAssunto
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public int AssuntoId { get; set; }

        public Livro Livro { get; set; }
        public Assunto Assunto { get; set; }
    }
}
