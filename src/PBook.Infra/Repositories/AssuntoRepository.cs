using Microsoft.EntityFrameworkCore;
using PBook.Domain.Entidades;
using PBook.Domain.Services;
using PBook.Infra;

namespace PBook.UI.Repositorio
{
    public class AssuntoRepository : IAssuntoRepository
    {
        private readonly BancoContent _context;

        public AssuntoRepository(BancoContent bancoContent)
        {
            this._context = bancoContent;
        }

        public async Task<Assunto> BuscarPorID(int id)
        {
            return await _context.Assuntos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Assunto>> BuscarTodos()
        {
            return await _context.Assuntos.ToListAsync();
        }

        public async Task<Assunto> Adicionar(Assunto assunto)
        {
            await _context.Assuntos.AddAsync(assunto);
            await _context.SaveChangesAsync();
            return assunto;
        }

        public async Task<Assunto> Atualizar(Assunto assunto)
        {
            Assunto assuntoDB = await BuscarPorID(assunto.Id);

            if (assuntoDB == null) throw new Exception("Houve um erro na atualização do assunto!");

            assuntoDB.Nome = assunto.Nome;

            _context.Assuntos.Update(assuntoDB);
            await _context.SaveChangesAsync();

            return assuntoDB;
        }

        public async Task<bool> Apagar(int id)
        {
            Assunto assuntoDB = await BuscarPorID(id);

            if (assuntoDB == null) throw new Exception("Houve um erro na deleção do assunto!");

            _context.Assuntos.Remove(assuntoDB);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
