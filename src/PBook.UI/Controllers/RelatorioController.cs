using Microsoft.AspNetCore.Mvc;
using PBook.Domain.Services;
using System.Threading.Tasks;

namespace PBook.UI.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly ILivroService _service;
        
        public RelatorioController(ILivroService livroService)
        {
            _service = livroService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        public async Task<FileContentResult> GerarRelatorioLivros()
        {
            byte[] data = await _service.ObterRelatorioLivros();
            return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Livros.xlsx");
        }
    }
}
