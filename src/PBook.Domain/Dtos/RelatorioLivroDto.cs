using PBook.Domain.Excel;

namespace PBook.Domain.Dtos
{
    public class RelatorioLivroDto
    {
        [ExcelColumn("Titulo", 1)]
        public string Titulo { get; set; }

        [ExcelColumn("Editora", 2)]
        public string Editora { get; set; }

        [ExcelColumn("Edicao", 3)]
        public string Edicao { get; set; }

        [ExcelColumn("AnoPublicacao", 4)]
        public string AnoPublicacao { get; set; }

        [ExcelColumn("Preco", 5)]
        public string Preco { get; set; }

        [ExcelColumn("Assunto", 6)]
        public string Assunto { get; set; }

        [ExcelColumn("Autor", 7)]
        public string Autor { get; set; }
    }
}
