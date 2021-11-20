using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PotluckPantry.Areas.Data.Accessors;
using PotluckPantry.Areas.Data.Entities;
using PotluckPantry.Areas.Data.Extension;
using PotluckPantry.Models;
using PotluckPantry.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PotluckPantry.Data.ApplicationDbContext;

namespace PotluckPantry.Controllers
{
    public class RecipeController : Controller
    {
        private IRecipeRepository _repo { get; set; }
        private IIngredientRepository _ingredientRepo { get; set; }
        private UserManager<IdentityUser> _userManager { get; set; }

        public RecipeController(IRecipeRepository repo, IIngredientRepository ingredientRepository, UserManager<IdentityUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
            _ingredientRepo = ingredientRepository;
        }

        public IActionResult Index(string id)
        {
            if (id != null)
            {
                var recipe = _repo.GetRecipe(id);
                if (recipe != null)
                {
                    var userRecipe = recipe.UserId == User.GetLoggedInUserId<string>();
                    return View(new RecipeViewModel() {
                        Id = recipe.Id,
                        UserId = recipe.UserId,
                        User = recipe.User,
                        Title = recipe.Title,
                        Description = recipe.Description,
                        RecipeIngredients = recipe.RecipeIngredients,
                        PostTime = recipe.PostTime,
                        IsUsersRecipe = userRecipe
                    }); ;
                }

            }


            return View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new Recipe());
        }

        [Authorize]
        public IActionResult CreateRecipe(Recipe recipe)
        {
            if (ModelState.IsValid)
            {   
                recipe.Id = Guid.NewGuid().ToString();
                recipe.PostTime = DateTime.Now;
                recipe.RecipeIngredients = _ingredientRepo.RecipeIngredientMapper(recipe.RecipeIngredients, recipe.Id);
                _ingredientRepo.Save();

                _repo.CreateRepice(recipe);
                _repo.Save();

                return RedirectToAction(nameof(Index), new { id = recipe.Id });
            }

            return View("Create", recipe);
        }

        public IActionResult Update(string Id)
        {
            Recipe recipe = _repo.GetRecipe(Id);


            return View("Update", recipe);
        }

        [Authorize]
        public IActionResult UpdateRecipe(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                if (recipe != null)
                {
                    var recipeIngredients = _ingredientRepo.RecipeIngredientMapper(recipe.RecipeIngredients, recipe.Id);
                    _ingredientRepo.Save();
                    recipe.RecipeIngredients = recipeIngredients;
                    _repo.UpdateRecipe(recipe);
                }


                return RedirectToAction(nameof(Index), new { id = recipe.Id });
            }

            return View("Update", recipe);
        }

        [Authorize]
        public IActionResult Delete(string id)
        {
            var userId = User.GetLoggedInUserId<string>();
            if (!string.IsNullOrEmpty(id))
            {
                var recipe = _repo.GetRecipe(id);
                if (recipe != null && recipe.UserId.Equals(userId)) {
                    _repo.DeleteRecipe(id);
                    _repo.Save();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddIngredient([Bind("RecipeIngredients")] Recipe recipeData)
        {
            recipeData.RecipeIngredients.Add(new RecipeIngredient());
            return PartialView("RecipeIngredients", recipeData);
        }

        [HttpGet]
        public string GetAllRecipes()
        {
            var recipes = _repo.GetRecipes();
            var json = JsonConvert.SerializeObject(
                new
                {
                    operations = recipes
                }
                );
            return json;
        }
    }
}
