using Microsoft.AspNetCore.Mvc;

namespace SEM.Controllers
{
    public class RutasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
