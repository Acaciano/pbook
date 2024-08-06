using PBook.Domain.Entidades;

namespace PBook.Domain.Services
{
    public class AutorService : IAutorService
    {
        private IAutorRepository _repository;

        public AutorService(IAutorRepository autorRepository)
        {
            _repository = autorRepository;
        }

        public async Task<Autor> BuscarPorID(int id)
        {
            return await _repository.BuscarPorID(id);
        }

        public async Task<List<Autor>> BuscarTodos()
        {
            return await _repository.BuscarTodos();
        }

        public async Task<Autor> Adicionar(Autor autor)
        {
            return await _repository.Adicionar(autor);
        }

        public async Task<Autor> Atualizar(Autor autor)
        {
            return await _repository.Atualizar(autor);
        }

        public async Task<bool> Apagar(int id)
        {
            return await _repository.Apagar(id);
        }
    }
}
