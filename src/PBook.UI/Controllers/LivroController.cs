using Microsoft.AspNetCore.Mvc;
using PBook.Domain.Entidades;
using PBook.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PBook.UI.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroService _livroService;
        private readonly IAutorService _autorService;
        private readonly IAssuntoService _assuntoService;

        public LivroController(ILivroService livroService,
                               IAutorService autorService,
                               IAssuntoService assuntoService)
        {
            _livroService = livroService;
            _autorService = autorService;
            _assuntoService = assuntoService;
        }

        public async Task<IActionResult> Index()
        {
            List<Livro> livros = await _livroService.BuscarTodos();

            return View(livros);
        }

        public async Task<IActionResult> Criar()
        {
            await CarregarDadosAutoresEAssuntoViewBag();

            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {
            Livro livro = await _livroService.BuscarPorID(id);

            ViewBag.Autores = await _autorService.BuscarTodos();
            ViewBag.Assuntos = await _assuntoService.BuscarTodos();

            if (livro.Assuntos != null && livro.Assuntos.Any())
                livro.AssuntosSelecionados = livro.Assuntos.Select(x => x.AssuntoId).ToList();

            if (livro.Autores != null && livro.Autores.Any())
                livro.AutoresSelecionados = livro.Autores.Select(x => x.AutorId).ToList();

            return View(livro);
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            Livro livro = await _livroService.BuscarPorID(id);
            return View(livro);
        }

        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _livroService.Apagar(id);

                if (apagado) TempData["MensagemSucesso"] = "Livro apagado com sucesso!"; else TempData["MensagemErro"] = "Ops, não conseguimos cadastrar seu livro, tente novamante!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu livro, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Livro livro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    livro = await _livroService.Adicionar(livro);

                    TempData["MensagemSucesso"] = "Livro cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                await CarregarDadosAutoresEAssuntoViewBag();
                return View(livro);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu livro, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Livro livro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    livro = await _livroService.Atualizar(livro);
                    TempData["MensagemSucesso"] = "Livro alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(livro);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu livro, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        private async Task CarregarDadosAutoresEAssuntoViewBag()
        {
            ViewBag.Autores = await _autorService.BuscarTodos();
            ViewBag.Assuntos = await _assuntoService.BuscarTodos();
        }
    }
}
