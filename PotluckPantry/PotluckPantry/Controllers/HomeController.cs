using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PotluckPantry.Areas.Data.Accessors;
using PotluckPantry.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeRepository _repo;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IRecipeRepository repo, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _repo = repo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var recipes = _repo.GetRecipes();
            foreach (var recipe in recipes)
            {
                if (recipe.Description.Length > 97)
                {
                    recipe.Description = recipe.Description.Substring(0, 97) + "...";
                }
            }
            return View(new HomeModel() 
            { 
                Recipes = recipes
            });
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

        public IActionResult Search(string searchString)
        {
            var recipes = _repo.SearchRecipes(searchString);

            return View("Index", new HomeModel() { Recipes = recipes });
        }
    }
}
