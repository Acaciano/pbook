using Microsoft.EntityFrameworkCore;
using PBook.Domain.Entidades;
using PBook.Domain.Services;
using PBook.Infra;

namespace PBook.UI.Repositorio
{
    public class AutorRepository : IAutorRepository
    {
        private readonly BancoContent _context;

        public AutorRepository(BancoContent bancoContent)
        {
            this._context = bancoContent;
        }

        public async Task<Autor> BuscarPorID(int id)
        {
            return await _context.Autores.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Autor>> BuscarTodos()
        {
            return await _context.Autores.ToListAsync();
        }

        public async Task<Autor> Adicionar(Autor autor)
        {
            await _context.Autores.AddAsync(autor);
            await _context.SaveChangesAsync();
            return autor;
        }

        public async Task<Autor> Atualizar(Autor autor)
        {
            Autor autorDB = await BuscarPorID(autor.Id);

            if (autorDB == null) throw new Exception("Houve um erro na atualização do autor!");

            autorDB.Nome = autor.Nome;

            _context.Autores.Update(autorDB);
            await _context.SaveChangesAsync();

            return autorDB;
        }

        public async Task<bool> Apagar(int id)
        {
            Autor autorDB = await BuscarPorID(id);

            if (autorDB == null) throw new Exception("Houve um erro na deleção do autor!");

            _context.Autores.Remove(autorDB);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
