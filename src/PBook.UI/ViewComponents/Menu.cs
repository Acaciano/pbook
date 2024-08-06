using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PBook.UI.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
