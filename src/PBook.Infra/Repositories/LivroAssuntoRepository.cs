using PBook.Domain.Entidades;
using PBook.Domain.Services;
using PBook.Infra;

namespace PBook.UI.Repositorio
{
    public class LivroAssuntoRepository : ILivroAssuntoRepository
    {
        private readonly BancoContent _context;

        public LivroAssuntoRepository(BancoContent bancoContent)
        {
            this._context = bancoContent;
        }

        public async Task<List<LivroAssunto>> BuscarLivroAssuntosPorLivro(int livroId)
        {
            return _context.LivroAssuntos.Where(x => x.LivroId == livroId).ToList();
        }

        public async Task AdicionarVinculoLivro(List<LivroAssunto> livroAutores)
        {
            await _context.LivroAssuntos.AddRangeAsync(livroAutores);
        }

        public async Task RemoverVinculoLivro(int livroId)
        {
            var buscarLivroAssuntosPorLivro = await BuscarLivroAssuntosPorLivro(livroId);

            if (buscarLivroAssuntosPorLivro != null && buscarLivroAssuntosPorLivro.Any())
                _context.LivroAssuntos.RemoveRange(buscarLivroAssuntosPorLivro);
        }
    }
}
