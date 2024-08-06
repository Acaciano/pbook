using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBook.Domain.Entidades
{
    public class Livro
    {
        public Livro()
        {
            Assuntos = new List<LivroAssunto>();
            Autores = new List<LivroAutor>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o titulo do livro")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Digite a editora do livro")]
        public string Editora { get; set; }

        [Required(ErrorMessage = "Digite a edição do livro")]
        public string Edicao { get; set; }

        [Required(ErrorMessage = "Digite o ano de publicação")]
        public int? AnoPublicacao { get; set; }

        [Required(ErrorMessage = "Digite o preço do livro")]
        public decimal? Preco { get; set; }

        public virtual ICollection<LivroAutor> Autores { get; set; }
        public virtual ICollection<LivroAssunto> Assuntos { get; set; }

        [NotMapped]
        public List<int> AutoresSelecionados { get; set; }

        [NotMapped]
        public List<int> AssuntosSelecionados { get; set; }
    }
}
