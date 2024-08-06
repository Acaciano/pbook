using PBook.Domain.Entidades;

namespace PBook.Domain.Services
{
    public interface IAutorService
    {
        Task<List<Autor>> BuscarTodos();
        Task<Autor> BuscarPorID(int id);
        Task<Autor> Adicionar(Autor autor);
        Task<Autor> Atualizar(Autor autor);
        Task<bool> Apagar (int id);
    }
}
