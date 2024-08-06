using Microsoft.AspNetCore.Mvc;
using PBook.Domain.Entidades;
using PBook.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PBook.UI.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorService _autorService;
        private readonly ILivroService _livroService;

        public AutorController(IAutorService autorService,
                               ILivroService livroService)
        {
            _autorService = autorService;
            _livroService = livroService;
        }

        public async Task<IActionResult> Index()
        {
            List<Autor> autores = await _autorService.BuscarTodos();

            return View(autores);
        }

        public async Task<IActionResult> Criar()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {
            Autor autor = await _autorService.BuscarPorID(id);
            return View(autor);
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            Autor autor = await _autorService.BuscarPorID(id);
            return View(autor);
        }

        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _autorService.Apagar(id);

                if (apagado) TempData["MensagemSucesso"] = "Autor apagado com sucesso!"; else TempData["MensagemErro"] = "Ops, não conseguimos apagar o autor, tente novamante!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o autor, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> ListarLivros()
        {
            List<Livro> livros = await _livroService.BuscarTodos();
            return PartialView("_LivrosAutor", livros);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Autor autor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    autor = await _autorService.Adicionar(autor);

                    TempData["MensagemSucesso"] = "Autor cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(autor);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o autor, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Autor autorModel)
        {
            try
            {
                Autor autor = null;

                if (ModelState.IsValid)
                {
                    autor = new Autor()
                    {
                        Id = autorModel.Id,
                        Nome = autorModel.Nome
                    };

                    autor = await _autorService.Atualizar(autor);
                    TempData["MensagemSucesso"] = "Autor alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(autor);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar autor, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
