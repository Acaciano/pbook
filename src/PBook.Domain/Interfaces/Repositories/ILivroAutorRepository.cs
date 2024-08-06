using PBook.Domain.Entidades;

namespace PBook.Domain.Services
{
    public interface ILivroAutorRepository
    {
        Task<List<LivroAutor>> BuscarLivroAutoresPorLivro(int livroId);
        Task AdicionarVinculoLivro(List<LivroAutor> livroAutores);
        Task RemoverVinculoLivro(int livroId);
    }
}
