using PBook.Domain.Entidades;
using PBook.Domain.Excel;

namespace PBook.Domain.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _repository;
        private readonly IExcelService _excelService;

        public LivroService(ILivroRepository livroRepository,
                            IExcelService excelService)
        {
            _repository = livroRepository;
            _excelService = excelService;
        }

        public async Task<Livro> BuscarPorID(int id)
        {
            return await _repository.BuscarPorID(id);
        }

        public async Task<List<Livro>> BuscarTodos()
        {
            return await _repository.BuscarTodos();
        }

        public async Task<byte[]> ObterRelatorioLivros()
        {
            IEnumerable<Dtos.RelatorioLivroDto> livros = await _repository.ObterRelatorioLivros();
            return _excelService.GenerateExcel(livros);
        }

        public async Task<Livro> Adicionar(Livro livro)
        {
            return await _repository.Adicionar(livro);
        }

        public async Task<Livro> Atualizar(Livro livro)
        {
            return await _repository.Atualizar(livro);
        }

        public async Task<bool> Apagar(int id)
        {
            return await _repository.Apagar(id);
        }
    }
}
