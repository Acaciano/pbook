using Moq;
using PBook.Domain.Entidades;
using PBook.Domain.Excel;
using PBook.Domain.Services;

namespace PBook.Tests.Services
{
    public class LivroServiceTests
    {
        private readonly Mock<ILivroRepository> _mockLivroRepository;
        private readonly Mock<IExcelService> _mockExcelService;
        private readonly LivroService _livroService;

        public LivroServiceTests()
        {
            _mockLivroRepository = new Mock<ILivroRepository>();
            _mockExcelService = new Mock<IExcelService>();
            _livroService = new LivroService(_mockLivroRepository.Object, _mockExcelService.Object);
        }

        [Fact]
        public async Task BuscarPorID_DeveRetornarLivro()
        {
            // Arrange
            int livroId = 1;
            Livro livroRetorno = new Livro { Id = livroId, Titulo = "Livro Teste" };
            _mockLivroRepository.Setup(r => r.BuscarPorID(livroId)).ReturnsAsync(livroRetorno);

            // Act
            var result = await _livroService.BuscarPorID(livroId);

            // Assert
            Assert.Equal(livroId, result.Id);
            Assert.Equal("Livro Teste", result.Titulo);
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
            var result = await _livroService.BuscarTodos();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Livro 1", result.First().Titulo);
            Assert.Equal("Livro 2", result.Last().Titulo);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarNovoLivro()
        {
            // Arrange
            Livro novoLivro = new Livro { Titulo = "Novo Livro" };
            _mockLivroRepository.Setup(r => r.Adicionar(novoLivro)).ReturnsAsync(novoLivro);

            // Act
            var result = await _livroService.Adicionar(novoLivro);

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
            var result = await _livroService.Atualizar(livroExistente);

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
            var result = await _livroService.Apagar(livroId);

            // Assert
            Assert.True(result);
        }
    }
}
