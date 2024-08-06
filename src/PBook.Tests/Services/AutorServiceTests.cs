using Moq;
using PBook.Domain.Entidades;
using PBook.Domain.Services;

namespace PBook.Tests.Services
{
    public class AutorServiceTests
    {
        private readonly Mock<IAutorRepository> _mockAutorRepository;
        private readonly AutorService _autorService;

        public AutorServiceTests()
        {
            _mockAutorRepository = new Mock<IAutorRepository>();
            _autorService = new AutorService(_mockAutorRepository.Object);
        }

        [Fact]
        public async Task BuscarPorID_DeveRetornarAutor()
        {
            // Arrange
            int autorId = 1;
            Autor autorRetorno = new Autor { Id = autorId, Nome = "Autor Teste" };
            _mockAutorRepository.Setup(r => r.BuscarPorID(autorId)).ReturnsAsync(autorRetorno);

            // Act
            var result = await _autorService.BuscarPorID(autorId);

            // Assert
            Assert.Equal(autorId, result.Id);
            Assert.Equal("Autor Teste", result.Nome);
        }

        [Fact]
        public async Task BuscarTodos_DeveRetornarListaDeAutores()
        {
            // Arrange
            List<Autor> autoresRetorno = new List<Autor>
            {
                new Autor { Id = 1, Nome = "Autor 1" },
                new Autor { Id = 2, Nome = "Autor 2" }
            };
            _mockAutorRepository.Setup(r => r.BuscarTodos()).ReturnsAsync(autoresRetorno);

            // Act
            var result = await _autorService.BuscarTodos();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Autor 1", result.First().Nome);
            Assert.Equal("Autor 2", result.Last().Nome);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarNovoAutor()
        {
            // Arrange
            Autor novoAutor = new Autor { Nome = "Novo Autor" };
            _mockAutorRepository.Setup(r => r.Adicionar(novoAutor)).ReturnsAsync(novoAutor);

            // Act
            var result = await _autorService.Adicionar(novoAutor);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Novo Autor", result.Nome);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarAutorAtualizado()
        {
            // Arrange
            Autor autorExistente = new Autor { Id = 1, Nome = "Autor Existente" };
            _mockAutorRepository.Setup(r => r.Atualizar(autorExistente)).ReturnsAsync(autorExistente);

            // Act
            var result = await _autorService.Atualizar(autorExistente);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Autor Existente", result.Nome);
        }

        [Fact]
        public async Task Apagar_DeveRetornarTrue()
        {
            // Arrange
            int autorId = 1;
            _mockAutorRepository.Setup(r => r.Apagar(autorId)).ReturnsAsync(true);

            // Act
            var result = await _autorService.Apagar(autorId);

            // Assert
            Assert.True(result);
        }
    }
}
