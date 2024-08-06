using Moq;
using PBook.Domain.Entidades;
using PBook.Domain.Services;

namespace PBook.Tests.Repositories
{
    public class LivroAutorRepositoryTests
    {
        private readonly Mock<ILivroAutorRepository> _mockLivroAutorRepository;

        public LivroAutorRepositoryTests()
        {
            _mockLivroAutorRepository = new Mock<ILivroAutorRepository>();
        }

        [Fact]
        public async Task BuscarLivroAutoresPorLivro_DeveRetornarListaDeLivroAutores()
        {
            // Arrange
            int livroId = 1;
            List<LivroAutor> livroAutoresRetorno = new List<LivroAutor>
            {
                new LivroAutor { LivroId = livroId, AutorId = 1 },
                new LivroAutor { LivroId = livroId, AutorId = 2 }
            };
            _mockLivroAutorRepository.Setup(r => r.BuscarLivroAutoresPorLivro(livroId)).ReturnsAsync(livroAutoresRetorno);

            // Act
            var result = await _mockLivroAutorRepository.Object.BuscarLivroAutoresPorLivro(livroId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].LivroId);
            Assert.Equal(1, result[0].AutorId);
            Assert.Equal(2, result[1].AutorId);
        }

        [Fact]
        public async Task AdicionarVinculoLivro_DeveAdicionarVinculo()
        {
            // Arrange
            List<LivroAutor> livroAutoresAdicionar = new List<LivroAutor>
            {
                new LivroAutor { LivroId = 1, AutorId = 1 },
                new LivroAutor { LivroId = 1, AutorId = 2 }
            };

            _mockLivroAutorRepository.Setup(r => r.AdicionarVinculoLivro(livroAutoresAdicionar)).Returns(Task.CompletedTask);

            // Act
            await _mockLivroAutorRepository.Object.AdicionarVinculoLivro(livroAutoresAdicionar);

            // Assert
            _mockLivroAutorRepository.Verify(r => r.AdicionarVinculoLivro(livroAutoresAdicionar), Times.Once);
        }

        [Fact]
        public async Task RemoverVinculoLivro_DeveRemoverVinculo()
        {
            // Arrange
            int livroId = 1;
            _mockLivroAutorRepository.Setup(r => r.RemoverVinculoLivro(livroId)).Returns(Task.CompletedTask);

            // Act
            await _mockLivroAutorRepository.Object.RemoverVinculoLivro(livroId);

            // Assert
            _mockLivroAutorRepository.Verify(r => r.RemoverVinculoLivro(livroId), Times.Once);
        }
    }
}
