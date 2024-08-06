using PBook.Domain.Entidades;

namespace PBook.Domain.Services
{
    public class AssuntoService : IAssuntoService
    {
        private readonly IAssuntoRepository _assuntoRepository;

        public AssuntoService(IAssuntoRepository assuntoRepository)
        {
            _assuntoRepository = assuntoRepository;
        }

        public async Task<Assunto> BuscarPorID(int id)
        {
            return await _assuntoRepository.BuscarPorID(id);
        }

        public async Task<List<Assunto>> BuscarTodos()
        {
            return await _assuntoRepository.BuscarTodos();
        }

        public async Task<Assunto> Adicionar(Assunto assunto)
        {
            return await _assuntoRepository.Adicionar(assunto);
        }

        public async Task<Assunto> Atualizar(Assunto assunto)
        {
            return await _assuntoRepository.Atualizar(assunto);
        }

        public async Task<bool> Apagar(int id)
        {
            return await _assuntoRepository.Apagar(id);
        }
    }
}
