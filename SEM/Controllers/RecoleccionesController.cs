using Microsoft.AspNetCore.Mvc;

namespace SEM.Controllers
{
    public class RecoleccionesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
