using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEM.Data;
using SEM.Models;

namespace SEM.Controllers
{
    public class LoginController : Controller
    {
        BL bl = new BL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VUP(string u, string p)
        {
            bool r = false;

            UsuarioModel usr = bl.ValidaUsrPwd(u, p);

            r = usr.Validado;

            if (r)
                HttpContext.Session.SetString("userLogged", JsonConvert.SerializeObject(usr));

            return Json(r);
        }

        public IActionResult LO()
        {
            HttpContext.Session.SetString("userLogged", "");
            return Json(true);
        }
    }
}
