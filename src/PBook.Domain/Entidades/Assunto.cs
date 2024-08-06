using System.ComponentModel.DataAnnotations;

namespace PBook.Domain.Entidades
{
    public class Assunto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do assunto")]
        public string Nome { get; set; }
    }
}
