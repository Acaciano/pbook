using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using PBook.Domain.Entidades;
using PBook.Domain.Services;
using PBook.UI.Controllers;

namespace PBook.Tests.Controllers
{
    public class LivroControllerTests
    {
        private readonly Mock<ILivroService> _mockLivroService;
        private readonly Mock<IAutorService> _mockAutorService;
        private readonly Mock<IAssuntoService> _mockAssuntoService;
        private readonly Mock<ITempDataDictionary> _mockTempData;
        private readonly LivroController _controller;

        public LivroControllerTests()
        {
            _mockLivroService = new Mock<ILivroService>();
            _mockAutorService = new Mock<IAutorService>();
            _mockAssuntoService = new Mock<IAssuntoService>();
            _mockTempData = new Mock<ITempDataDictionary>();

            _controller = new LivroController(_mockLivroService.Object, _mockAutorService.Object, _mockAssuntoService.Object)
            {
                TempData = _mockTempData.Object
            };
        }

        [Fact]
        public async Task Index_DeveRetornarViewComListaDeLivros()
        {
            // Arrange
            var livros = new List<Livro> { new Livro { Id = 1, Titulo = "Livro 1" }, new Livro { Id = 2, Titulo = "Livro 2" } };
            _mockLivroService.Setup(service => service.BuscarTodos()).ReturnsAsync(livros);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<List<Livro>>(result.ViewData.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Criar_DeveRetornarViewComViewBagPopulado()
        {
            // Arrange
            var autores = new List<Autor> { new Autor { Id = 1, Nome = "Autor 1" } };
            var assuntos = new List<Assunto> { new Assunto { Id = 1, Nome = "Assunto 1" } };

            _mockAutorService.Setup(service => service.BuscarTodos()).ReturnsAsync(autores);
            _mockAssuntoService.Setup(service => service.BuscarTodos()).ReturnsAsync(assuntos);

            // Act
            var result = await _controller.Criar() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewData.ContainsKey("Autores"));
            Assert.True(result.ViewData.ContainsKey("Assuntos"));

            var viewBagAutores = result.ViewData["Autores"] as List<Autor>;
            var viewBagAssuntos = result.ViewData["Assuntos"] as List<Assunto>;

            Assert.NotNull(viewBagAutores);
            Assert.NotNull(viewBagAssuntos);
            Assert.Single(viewBagAutores);
            Assert.Single(viewBagAssuntos);
        }

        [Fact]
        public async Task ApagarConfirmacao_DeveRetornarViewComLivro()
        {
            // Arrange
            var livro = new Livro { Id = 1, Titulo = "Livro Teste" };
            _mockLivroService.Setup(service => service.BuscarPorID(1)).ReturnsAsync(livro);

            // Act
            var result = await _controller.ApagarConfirmacao(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<Livro>(result.ViewData.Model);
            Assert.Equal(1, model.Id);
            Assert.Equal("Livro Teste", model.Titulo);
        }

        [Fact]
        public async Task Apagar_DeveRedirecionarParaIndexComMensagemDeSucesso()
        {
            // Arrange
            _mockLivroService.Setup(service => service.Apagar(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.Apagar(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockTempData.VerifySet(tempData => tempData["MensagemSucesso"] = "Livro apagado com sucesso!");
        }

        [Fact]
        public async Task Apagar_DeveRedirecionarParaIndexComMensagemDeErro()
        {
            // Arrange
            _mockLivroService.Setup(service => service.Apagar(1)).ReturnsAsync(false);

            // Act
            var result = await _controller.Apagar(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockTempData.VerifySet(tempData => tempData["MensagemErro"] = "Ops, não conseguimos cadastrar seu livro, tente novamante!");
        }

        [Fact]
        public async Task Apagar_DeveRedirecionarParaIndexComMensagemDeErroAoLancarExcecao()
        {
            // Arrange
            _mockLivroService.Setup(service => service.Apagar(1)).ThrowsAsync(new Exception("Erro de teste"));

            // Act
            var result = await _controller.Apagar(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockTempData.VerifySet(tempData => tempData["MensagemErro"] = "Ops, não conseguimos apagar seu livro, tente novamante, detalhe do erro: Erro de teste");
        }

        [Fact]
        public async Task Criar_Post_DeveRedirecionarParaIndexSeSucesso()
        {
            // Arrange
            var livro = new Livro { Titulo = "Novo Livro" };
            _mockLivroService.Setup(service => service.Adicionar(livro)).ReturnsAsync(livro);

            // Act
            var result = await _controller.Criar(livro) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockTempData.VerifySet(tempData => tempData["MensagemSucesso"] = "Livro cadastrado com sucesso!");
        }

        [Fact]
        public async Task Criar_Post_DeveRetornarViewSeModelInvalido()
        {
            // Arrange
            _controller.ModelState.AddModelError("Titulo", "Required");

            // Act
            var result = await _controller.Criar(new Livro()) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Editar_Post_DeveRedirecionarParaIndexSeSucesso()
        {
            // Arrange
            var livro = new Livro { Id = 1, Titulo = "Livro Atualizado" };
            _mockLivroService.Setup(service => service.Atualizar(livro)).ReturnsAsync(livro);

            // Act
            var result = await _controller.Editar(livro) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            _mockTempData.VerifySet(tempData => tempData["MensagemSucesso"] = "Livro alterado com sucesso!");
        }

        [Fact]
        public async Task Editar_Post_DeveRetornarViewSeModelInvalido()
        {
            // Arrange
            _controller.ModelState.AddModelError("Titulo", "Required");

            // Act
            var result = await _controller.Editar(new Livro()) as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
