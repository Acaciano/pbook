using System.ComponentModel.DataAnnotations;

namespace PBook.Domain.Entidades
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do autor")]
        public string Nome { get; set; }
    }
}
