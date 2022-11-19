using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEM.Data;
using SEM.Models;
using System.Diagnostics;

namespace SEM.Controllers
{
    public class HomeController : Controller
    {
        BL bl = new BL();

        private readonly ILogger<HomeController> _logger;

        UsuarioModel? u;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            /////////////////////////////////Para pruebas
            //u = new UsuarioModel
            //{
            //    Id = 1,
            //    Usuario = "albertoh",
            //    Nombre = "Alberto",
            //    ApPat = "Hernández",
            //    Validado = true
            //};

            HttpContext.Session.SetString("userLogged", JsonConvert.SerializeObject(u));

            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("userLogged")))
                return RedirectToAction("Index", "Login");
            else
            {
                u = JsonConvert.DeserializeObject<UsuarioModel>(HttpContext.Session.GetString("userLogged"));

                if (u != null && u.Id > 0)
                    HttpContext.Session.SetString("menu", JsonConvert.SerializeObject(bl.GetNodosMenu(null, u.Id)));

                string msg = "Bienvenid" + (u.Genero == "m" ? "o " : "a ") + u.Nombre;

                ViewBag.MsgBienvenido = msg;

                return View();
            }
        }

        public IActionResult SelectBase()
        {
            //return Json(bl.SelectBase());
            return Json("");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}