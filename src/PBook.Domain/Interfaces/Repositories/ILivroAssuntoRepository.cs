using PBook.Domain.Entidades;

namespace PBook.Domain.Services
{
    public interface ILivroAssuntoRepository
    {
        Task<List<LivroAssunto>> BuscarLivroAssuntosPorLivro(int livroId);
        Task AdicionarVinculoLivro(List<LivroAssunto> livroAssuntos);
        Task RemoverVinculoLivro(int livroId);
    }
}
