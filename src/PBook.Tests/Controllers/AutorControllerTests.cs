using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using PBook.Domain.Entidades;
using PBook.Domain.Services;
using PBook.UI.Controllers;

namespace PBook.Tests.Controllers
{
    public class AutorControllerTests
    {
        private readonly Mock<IAutorService> _mockAutorService;
        private readonly Mock<ILivroService> _mockLivroService;
        private readonly Mock<ITempDataDictionary> _mockTempData;
        private readonly AutorController _controller;

        public AutorControllerTests()
        {
            _mockAutorService = new Mock<IAutorService>();
            _mockLivroService = new Mock<ILivroService>();
            _mockTempData = new Mock<ITempDataDictionary>();

            _controller = new AutorController(_mockAutorService.Object, _mockLivroService.Object)
            {
                TempData = _mockTempData.Object
            };
        }

        [Fact]
        public async Task Index_DeveRetornarViewComListaDeAutores()
        {
            // Arrange
            var autores = new List<Autor> { new Autor { Id = 1, Nome = "Autor 1" }, new Autor { Id = 2, Nome = "Autor 2" } };
            _mockAutorService.Setup(service => service.BuscarTodos()).ReturnsAsync(autores);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<List<Autor>>(result.ViewData.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void Criar_DeveRetornarView()
        {
            // Act
            var result = _controller.Criar().Result as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Editar_DeveRetornarViewComAutor()
        {
            // Arrange
            var autor = new Autor { Id = 1, Nome = "Autor Teste" };
            _mockAutorService.Setup(service => service.BuscarPorID(1)).ReturnsAsync(autor);

            // Act
            var result = await _controller.Editar(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<Autor>(result.ViewData.Model);
            Assert.Equal(1, model.Id);
            Assert.Equal("Autor Teste", model.Nome);
        }

        [Fact]
        public async Task ApagarConfirmacao_DeveRetornarViewComAutor()
        {
            // Arrange
            var autor = new Autor { Id = 1, Nome = "Autor Teste" };
            _mockAutorService.Setup(service => service.BuscarPorID(1)).ReturnsAsync(autor);

            // Act
            var result = await _controller.ApagarConfirmacao(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<Autor>(result.ViewData.Model);
            Assert.Equal(1, model.Id);
            Assert.Equal("Autor Teste", model.Nome);
        }

        [Fact]
        public async Task Apagar_DeveRedirecionarParaIndexComMensagemDeSucesso()
        {
            // Arrange
            _mockAutorService.Setup(service => service.Apagar(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Apagar(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockTempData.VerifySet(tempData => tempData["MensagemSucesso"] = "Autor apagado com sucesso!");
        }

        [Fact]
        public async Task Apagar_DeveRedirecionarParaIndexComMensagemDeErro()
        {
            // Arrange
            _mockAutorService.Setup(service => service.Apagar(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Apagar(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockTempData.VerifySet(tempData => tempData["MensagemErro"] = "Ops, não conseguimos apagar o autor, tente novamante!");
        }

        [Fact]
        public async Task Apagar_DeveRedirecionarParaIndexComMensagemDeErroAoLancarExcecao()
        {
            // Arrange
            _mockAutorService.Setup(service => service.Apagar(1)).ThrowsAsync(new Exception("Erro de teste"));

            // Act
            var result = await _controller.Apagar(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockTempData.VerifySet(tempData => tempData["MensagemErro"] = "Ops, não conseguimos apagar o autor, tente novamante, detalhe do erro: Erro de teste");
        }

        [Fact]
        public async Task ListarLivros_DeveRetornarPartialViewComListaDeLivros()
        {
            // Arrange
            var livros = new List<Livro> { new Livro { Id = 1, Titulo = "Livro 1" }, new Livro { Id = 2, Titulo = "Livro 2" } };
            _mockLivroService.Setup(service => service.BuscarTodos()).ReturnsAsync(livros);

            // Act
            var result = await _controller.ListarLivros() as PartialViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<List<Livro>>(result.ViewData.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Criar_Post_DeveRedirecionarParaIndexSeSucesso()
        {
            // Arrange
            var autor = new Autor { Nome = "Novo Autor" };
            _mockAutorService.Setup(service => service.Adicionar(autor)).ReturnsAsync(autor);

            // Act
            var result = await _controller.Criar(autor) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockTempData.VerifySet(tempData => tempData["MensagemSucesso"] = "Autor cadastrado com sucesso!");
        }

        [Fact]
        public async Task Criar_Post_DeveRetornarViewSeModelInvalido()
        {
            // Arrange
            _controller.ModelState.AddModelError("Nome", "Required");

            // Act
            var result = await _controller.Criar(new Autor()) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Editar_Post_DeveRedirecionarParaIndexSeSucesso()
        {
            // Arrange
            var autor = new Autor { Id = 1, Nome = "Autor Atualizado" };
            _mockAutorService.Setup(service => service.Atualizar(autor)).ReturnsAsync(autor);

            // Act
            var result = await _controller.Editar(autor) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockTempData.VerifySet(tempData => tempData["MensagemSucesso"] = "Autor alterado com sucesso!");
        }

        [Fact]
        public async Task Editar_Post_DeveRetornarViewSeModelInvalido()
        {
            // Arrange
            _controller.ModelState.AddModelError("Nome", "Required");

            // Act
            var result = await _controller.Editar(new Autor()) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
