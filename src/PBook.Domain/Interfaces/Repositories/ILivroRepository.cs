using PBook.Domain.Dtos;
using PBook.Domain.Entidades;

namespace PBook.Domain.Services
{
    public interface ILivroRepository
    {
        Task<List<Livro>> BuscarTodos();
        Task<IEnumerable<RelatorioLivroDto>> ObterRelatorioLivros();
        Task<Livro> BuscarPorID(int id);
        Task<Livro> Adicionar(Livro livro);
        Task<Livro> Atualizar(Livro livro);
        Task<bool> Apagar (int id);
    }
}
