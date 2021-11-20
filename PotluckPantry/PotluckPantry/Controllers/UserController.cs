using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {

            }
            return RedirectToAction("Error", "Home");
        }
    }
}
