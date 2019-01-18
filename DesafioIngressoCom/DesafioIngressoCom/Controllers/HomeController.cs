using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DesafioIngressoCom.Models;

namespace DesafioIngressoCom.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Filme");
        }  

        public IActionResult List()
        {
            return View();
        }
    }
}
