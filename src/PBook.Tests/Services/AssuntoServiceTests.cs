using Moq;
using PBook.Domain.Entidades;
using PBook.Domain.Services;

namespace PBook.Tests.Services
{
    public class AssuntoServiceTests
    {
        private readonly Mock<IAssuntoRepository> _mockAssuntoRepository;
        private readonly AssuntoService _assuntoService;

        public AssuntoServiceTests()
        {
            _mockAssuntoRepository = new Mock<IAssuntoRepository>();
            _assuntoService = new AssuntoService(_mockAssuntoRepository.Object);
        }

        [Fact]
        public async Task BuscarPorID_DeveRetornarAssunto()
        {
            // Arrange
            int assuntoId = 1;
            Assunto assuntoRetorno = new Assunto { Id = assuntoId, Nome = "Assunto Teste" };
            _mockAssuntoRepository.Setup(r => r.BuscarPorID(assuntoId)).ReturnsAsync(assuntoRetorno);

            // Act
            var result = await _assuntoService.BuscarPorID(assuntoId);

            // Assert
            Assert.Equal(assuntoId, result.Id);
            Assert.Equal("Assunto Teste", result.Nome);
        }

        [Fact]
        public async Task BuscarTodos_DeveRetornarListaDeAssuntos()
        {
            // Arrange
            List<Assunto> assuntosRetorno = new List<Assunto>
            {
                new Assunto { Id = 1, Nome = "Assunto 1" },
                new Assunto { Id = 2, Nome = "Assunto 2" }
            };
            _mockAssuntoRepository.Setup(r => r.BuscarTodos()).ReturnsAsync(assuntosRetorno);

            // Act
            var result = await _assuntoService.BuscarTodos();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Assunto 1", result.First().Nome);
            Assert.Equal("Assunto 2", result.Last().Nome);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarNovoAssunto()
        {
            // Arrange
            Assunto novoAssunto = new Assunto { Nome = "Novo Assunto" };
            _mockAssuntoRepository.Setup(r => r.Adicionar(novoAssunto)).ReturnsAsync(novoAssunto);

            // Act
            var result = await _assuntoService.Adicionar(novoAssunto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Novo Assunto", result.Nome);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarAssuntoAtualizado()
        {
            // Arrange
            Assunto assuntoExistente = new Assunto { Id = 1, Nome = "Assunto Existente" };
            _mockAssuntoRepository.Setup(r => r.Atualizar(assuntoExistente)).ReturnsAsync(assuntoExistente);

            // Act
            var result = await _assuntoService.Atualizar(assuntoExistente);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Assunto Existente", result.Nome);
        }

        [Fact]
        public async Task Apagar_DeveRetornarTrue()
        {
            // Arrange
            int assuntoId = 1;
            _mockAssuntoRepository.Setup(r => r.Apagar(assuntoId)).ReturnsAsync(true);

            // Act
            var result = await _assuntoService.Apagar(assuntoId);

            // Assert
            Assert.True(result);
        }
    }
}
