namespace PBook.Domain.Entidades
{
    public class LivroAutor
    {
        public LivroAutor()
        {

        }

        public int Id { get; set; }
        public int LivroId { get; set; }
        public int AutorId { get; set; }

        public Livro Livro { get; set; }
        public Autor Autor { get; set; }
    }
}
