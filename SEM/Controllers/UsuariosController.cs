using Microsoft.AspNetCore.Mvc;

namespace SEM.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
