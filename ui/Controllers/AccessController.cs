using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ui.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult PageNotFound()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}