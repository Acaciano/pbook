using Moq;
using PBook.Domain.Dtos;
using PBook.Domain.Entidades;
using PBook.Domain.Services;

namespace PBook.Tests.Repositories
{
    public class LivroRepositoryTests
    {
        private readonly Mock<ILivroRepository> _mockLivroRepository;

        public LivroRepositoryTests()
        {
            _mockLivroRepository = new Mock<ILivroRepository>();
        }

        [Fact]
        public async Task BuscarTodos_DeveRetornarListaDeLivros()
        {
            // Arrange
            List<Livro> livrosRetorno = new List<Livro>
            {
                new Livro { Id = 1, Titulo = "Livro 1" },
                new Livro { Id = 2, Titulo = "Livro 2" }
            };
            _mockLivroRepository.Setup(r => r.BuscarTodos()).ReturnsAsync(livrosRetorno);

            // Act
            var result = await _mockLivroRepository.Object.BuscarTodos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Livro 1", result.First().Titulo);
            Assert.Equal("Livro 2", result.Last().Titulo);
        }

        [Fact]
        public async Task ObterRelatorioLivros_DeveRetornarListaDeRelatorioLivroDto()
        {
            // Arrange
            List<RelatorioLivroDto> relatorioLivrosRetorno = new List<RelatorioLivroDto>
            {
                new RelatorioLivroDto { Titulo = "Livro 1", Autor = "Autor 1" },
                new RelatorioLivroDto { Titulo = "Livro 2", Autor = "Autor 2" }
            };
            _mockLivroRepository.Setup(r => r.ObterRelatorioLivros()).ReturnsAsync(relatorioLivrosRetorno);

            // Act
            var result = await _mockLivroRepository.Object.ObterRelatorioLivros();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Livro 1", result.First().Titulo);
            Assert.Equal("Livro 2", result.Last().Titulo);
        }

        [Fact]
        public async Task BuscarPorID_DeveRetornarLivro()
        {
            // Arrange
            int livroId = 1;
            Livro livroRetorno = new Livro { Id = livroId, Titulo = "Livro Teste" };
            _mockLivroRepository.Setup(r => r.BuscarPorID(livroId)).ReturnsAsync(livroRetorno);

            // Act
            var result = await _mockLivroRepository.Object.BuscarPorID(livroId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(livroId, result.Id);
            Assert.Equal("Livro Teste", result.Titulo);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarNovoLivro()
        {
            // Arrange
            Livro novoLivro = new Livro { Titulo = "Novo Livro" };
            _mockLivroRepository.Setup(r => r.Adicionar(novoLivro)).ReturnsAsync(novoLivro);

            // Act
            var result = await _mockLivroRepository.Object.Adicionar(novoLivro);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Novo Livro", result.Titulo);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarLivroAtualizado()
        {
            // Arrange
            Livro livroExistente = new Livro { Id = 1, Titulo = "Livro Existente" };
            _mockLivroRepository.Setup(r => r.Atualizar(livroExistente)).ReturnsAsync(livroExistente);

            // Act
            var result = await _mockLivroRepository.Object.Atualizar(livroExistente);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Livro Existente", result.Titulo);
        }

        [Fact]
        public async Task Apagar_DeveRetornarTrue()
        {
            // Arrange
            int livroId = 1;
            _mockLivroRepository.Setup(r => r.Apagar(livroId)).ReturnsAsync(true);

            // Act
            var result = await _mockLivroRepository.Object.Apagar(livroId);

            // Assert
            Assert.True(result);
        }
    }
}
