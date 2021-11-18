using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PotluckPantry.Areas.Data.Accessors;
using PotluckPantry.Areas.Data.Entities;
using PotluckPantry.Areas.Data.Extension;
using PotluckPantry.Models;
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
                    var userRecipe = recipe.Id == User.GetLoggedInUserId<string>();
                    return View(new RecipeModel() { Recipe = recipe, IsUsersRecipe = userRecipe });
                }

            }


            return View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new NewRecipeModel());
        }

        [Authorize]
        public IActionResult CreateRecipe(NewRecipeModel recipeData)
        {
            if (ModelState.IsValid)
            {
                var recipeId = Guid.NewGuid().ToString();

                List<RecipeIngredient> ri = new();
                foreach (var ingredient in recipeData.RecipeIngredients)
                {
                    var ingredientId = "";
                    if (_ingredientRepo.DoesIngredientExist(ingredient.IngredientName))
                    {
                        var ingridientFromRepo = _ingredientRepo.GetIngredientByName(ingredient.IngredientName);
                        ingredientId = ingridientFromRepo.Id;
                    }
                    else
                    {
                        ingredientId = Guid.NewGuid().ToString();
                        _ingredientRepo.CreateIngredient(new Ingredient()
                        {
                            Id = ingredientId,
                            Name = ingredient.IngredientName
                        }
                        );
                    }

                    ri.Add(new RecipeIngredient()
                    {
                        Amount = ingredient.IngredientAmount,
                        NormalizedAmount = ingredient.IngredientAmount.ToUpperInvariant(),
                        IngredientId = ingredientId,
                        RecipeId = recipeId
                    }
                    );
                }
                var dbRecipe = new Recipe()
                {
                    Id = recipeId,
                    Title = recipeData.Name,
                    Description = recipeData.Description,
                    PostTime = DateTime.Now,
                    RecipeIngredients = ri
                };

                _repo.CreateRepice(dbRecipe);
                _ingredientRepo.Save();
                _repo.Save();

                return RedirectToAction(nameof(Index), new { id = recipeId });
            }

            return View(recipeData);
        }

        /*public IActionResult Update(string id)
        {
            Recipe recipe = _repo.GetRecipe(id);


            return View("Edit", new RecipeModel() { Recipe = recipe });
        }

        [Authorize]
        public int Update(NewRecipeModel data)
        {
            var recipe = _repo.GetRecipe(data.Id);
            recipe.Title = data.Name;
            recipe.RecipeIngredients = data.RecipeIngredients;
            recipe.Description = data.Description;
            _repo.UpdateRecipe(recipe);

            return -1;
        }*/

        [Authorize]
        public int Delete(string id)
        {

            return -1;
        }

        [HttpPost]
        public IActionResult AddIngredient([Bind("RecipeIngredients")] NewRecipeModel recipeData)
        {
            recipeData.RecipeIngredients.Add(new RecipeIngredientModel());
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
