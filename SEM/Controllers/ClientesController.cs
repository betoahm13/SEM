﻿using Microsoft.AspNetCore.Mvc;

namespace SEM.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
