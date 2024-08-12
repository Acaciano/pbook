using Microsoft.AspNetCore.Mvc;
using PBook.Domain.Entidades;
using PBook.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PBook.UI.Controllers
{
    public class AssuntoController : Controller
    {
        private readonly IAssuntoService _service;

        public AssuntoController(IAssuntoService service)
        {
            _service = service;
        }

        public async Task<IActionResult>Index()
        {
            List<Assunto> assuntos = await _service.BuscarTodos();

            return View(assuntos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public async Task<IActionResult> Editar(int id)
        {
            Assunto assunto = await _service.BuscarPorID(id);
            return View(assunto);
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            Assunto assunto = await _service.BuscarPorID(id);
            return View(assunto);
        }

        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                bool apagado = await _service.Apagar(id);

                if (apagado) TempData["MensagemSucesso"] = "Assunto apagado com sucesso!"; else TempData["MensagemErro"] = "Ops, não conseguimos apagar o assunto, tente novamante!";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o assunto, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Assunto assunto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    assunto = await _service.Adicionar(assunto);

                    TempData["MensagemSucesso"] = "Assunto cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(assunto);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o assunto, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Assunto assuntoModel)
        {
            try
            {
                Assunto assunto = null;

                if (ModelState.IsValid)
                {
                    assunto = new Assunto()
                    {
                        Id = assuntoModel.Id,
                        Nome = assuntoModel.Nome
                    };

                    assunto = await _service.Atualizar(assunto);
                    TempData["MensagemSucesso"] = "Assunto alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(assunto);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar assunto, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
