using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PotluckPantry.Areas.Data.Accessors;
using PotluckPantry.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Controllers
{
    public class UserController : Controller
    {
        
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IRecipeRepository recipeRepository, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _recipeRepository = recipeRepository;
            _userManager = userManager;
        }

        public IActionResult Index(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var id = _userRepository.GetUserIdFromUsername(username);
                return View("User", new UserViewModel()
                {
                    user = _userRepository.GetUser(id),
                    userRecipes = _recipeRepository.GetRecipesByUser(id)
                });
            }
            return RedirectToAction("Error", "Home");
        }
    }
}
