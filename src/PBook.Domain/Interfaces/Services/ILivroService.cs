using PBook.Domain.Entidades;

namespace PBook.Domain.Services
{
    public interface ILivroService
    {
        Task<List<Livro>> BuscarTodos();
        Task<byte[]> ObterRelatorioLivros();
        Task<Livro> BuscarPorID(int id);
        Task<Livro> Adicionar(Livro livro);
        Task<Livro> Atualizar(Livro livro);
        Task<bool> Apagar (int id);
    }
}
