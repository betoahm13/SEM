using Microsoft.AspNetCore.Mvc;

namespace SEM.Controllers
{
    public class EventosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
