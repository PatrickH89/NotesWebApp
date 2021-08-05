using Microsoft.AspNetCore.Mvc;

namespace NotesWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
