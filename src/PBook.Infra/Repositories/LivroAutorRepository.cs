using Microsoft.EntityFrameworkCore;
using PBook.Domain.Entidades;
using PBook.Domain.Services;
using PBook.Infra;

namespace PBook.UI.Repositorio
{
    public class LivroAutorRepository : ILivroAutorRepository
    {
        private readonly BancoContent _context;

        public LivroAutorRepository(BancoContent bancoContent)
        {
            this._context = bancoContent;
        }

        public async Task<List<LivroAutor>> BuscarLivroAutoresPorLivro(int livroId)
        {
            return await _context.LivroAutores.Where(x => x.LivroId == livroId).ToListAsync();
        }

        public async Task AdicionarVinculoLivro(List<LivroAutor> livroAutores)
        {
            await _context.LivroAutores.AddRangeAsync(livroAutores);
        }

        public async Task RemoverVinculoLivro(int livroId)
        {
            var buscarLivroAutoresPorLivro = await BuscarLivroAutoresPorLivro(livroId);

            if (buscarLivroAutoresPorLivro != null && buscarLivroAutoresPorLivro.Any())
                _context.LivroAutores.RemoveRange(buscarLivroAutoresPorLivro);
        }
    }
}
