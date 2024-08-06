using PBook.Domain.Entidades;

namespace PBook.Domain.Services
{
    public interface IAssuntoRepository
    {
        Task<List<Assunto>> BuscarTodos();
        Task<Assunto> BuscarPorID(int id);
        Task<Assunto> Adicionar(Assunto assunto);
        Task<Assunto> Atualizar(Assunto assunto);
        Task<bool> Apagar (int id);
    }
}
