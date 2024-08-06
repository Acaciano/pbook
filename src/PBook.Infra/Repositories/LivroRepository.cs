using PBook.Domain.Dtos;
using PBook.Domain.Entidades;
using PBook.Domain.Services;
using PBook.Infra;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PBook.UI.Repositorio
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BancoContent _context;
        private readonly ILivroAutorRepository _livroAutorRepository;
        private readonly ILivroAssuntoRepository _livroAssuntoRepository;

        public LivroRepository(BancoContent bancoContent,
                                ILivroAutorRepository livroAutorRepository,
                                ILivroAssuntoRepository livroAssuntoRepository)
        {
            this._context = bancoContent;
            this._livroAutorRepository = livroAutorRepository;
            this._livroAssuntoRepository = livroAssuntoRepository; 

        }

        public async Task<Livro> BuscarPorID(int id)
        {
            return _context.Livros
                        .Include(x => x.Assuntos)
                        .Include(x => x.Autores)
                        .FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<Livro>> BuscarTodos()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task<IEnumerable<RelatorioLivroDto>> ObterRelatorioLivros()
        {
            string query = "SELECT * FROM VW_RelatorioLivros ORDER BY Titulo";
            return await _context.Database.GetDbConnection().QueryAsync<RelatorioLivroDto>(query);
        }

        public async Task<Livro> Adicionar(Livro livro)
        {
            if (livro.AutoresSelecionados != null && livro.AutoresSelecionados.Any())
            {
                foreach (var autorId in livro.AutoresSelecionados)
                    livro.Autores.Add(new LivroAutor() { AutorId = autorId });
            }

            if (livro.AssuntosSelecionados != null && livro.AssuntosSelecionados.Any())
            {
                foreach (var assuntoId in livro.AssuntosSelecionados)
                    livro.Assuntos.Add((new LivroAssunto() { AssuntoId = assuntoId }));
            }

            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<Livro> Atualizar(Livro livro)
        {
            Livro livroDB = await BuscarPorID(livro.Id);
            List<LivroAutor> livroAutores = new List<LivroAutor>();
            List<LivroAssunto> livroAssuntos = new List<LivroAssunto>();

            if (livroDB == null) throw new Exception("Houve um erro na atualização do livro!");

            livroDB.Titulo = livro.Titulo;
            livroDB.Editora = livro.Editora;
            livroDB.Edicao = livro.Edicao;
            livroDB.AnoPublicacao = livro.AnoPublicacao;
            livroDB.Preco = livro.Preco;

            var buscarLivroAssuntosPorLivro = await _context.LivroAssuntos.Where(x => x.LivroId == livro.Id).ToListAsync();

            if (buscarLivroAssuntosPorLivro != null && buscarLivroAssuntosPorLivro.Any())
                _context.LivroAssuntos.RemoveRange(buscarLivroAssuntosPorLivro);

            await _livroAutorRepository.RemoverVinculoLivro(livro.Id);

            if (livro.AutoresSelecionados != null && livro.AutoresSelecionados.Any())
            {
                foreach (var autorId in livro.AutoresSelecionados)
                    livroAutores.Add(new LivroAutor() { AutorId = autorId, LivroId = livro.Id });
            }

            if (livro.AssuntosSelecionados != null && livro.AssuntosSelecionados.Any())
            {
                foreach (var assuntoId in livro.AssuntosSelecionados)
                    livroAssuntos.Add((new LivroAssunto() { AssuntoId = assuntoId, LivroId = livro.Id }));
            }

            if (livroAutores.Any())
                await _livroAutorRepository.AdicionarVinculoLivro(livroAutores);

            if (livroAssuntos.Any())
                await _livroAssuntoRepository.AdicionarVinculoLivro(livroAssuntos);

            _context.Livros.Update(livroDB);

            await _context.SaveChangesAsync();

            return livroDB;
        }

        public async Task<bool> Apagar(int id)
        {
            Livro livroDB = await BuscarPorID(id);

            if (livroDB == null) throw new Exception("Houve um erro na deleção do livro!");

            _context.Livros.Remove(livroDB);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
