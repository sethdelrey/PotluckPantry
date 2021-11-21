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
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public RecipeController(IRecipeRepository repo, IIngredientRepository ingredientRepository, IReviewRepository reviewRepository, UserManager<IdentityUser> userManager)
        {
            _recipeRepository = repo;
            _ingredientRepository = ingredientRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
        }

        public IActionResult Index(string id)
        {
            if (id != null)
            {
                var recipe = _recipeRepository.GetRecipe(id);
                if (recipe != null)
                {
                    var userRecipe = recipe.UserId == User.GetLoggedInUserId<string>();
                    var recipeReviewList = _reviewRepository.GetRecipesReviews(recipe.Id);
                    return View(new RecipeViewModel(recipe)
                    {
                        IsUsersRecipe = userRecipe,
                        RecipeReviews = recipeReviewList ?? new List<Review>()
                    });
                }

            }


            return View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new Recipe(""));
        }

        [Authorize]
        public IActionResult CreateRecipe(Recipe recipe)
        {
            if (ModelState.IsValid)
            {   
                recipe.Id = Guid.NewGuid().ToString();
                recipe.PostTime = DateTime.Now;
                recipe.RecipeIngredients = _ingredientRepository.RecipeIngredientMapper(recipe.RecipeIngredients, recipe.Id);
                recipe.UserId = User.GetLoggedInUserId<string>();
                _ingredientRepository.Save();

                _recipeRepository.CreateRepice(recipe);
                _recipeRepository.Save();

                return RedirectToAction(nameof(Index), new { id = recipe.Id });
            }

            return View("Create", recipe);
        }

        [Authorize]
        public IActionResult Update(string Id)
        {
            Recipe recipe = _recipeRepository.GetRecipe(Id);


            return View("Update", recipe);
        }

        [Authorize]
        public IActionResult UpdateRecipe(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                if (recipe != null)
                {
                    var recipeIngredients = _ingredientRepository.RecipeIngredientMapper(recipe.RecipeIngredients, recipe.Id);
                    _ingredientRepository.Save();
                    recipe.RecipeIngredients = recipeIngredients;
                    _recipeRepository.UpdateRecipe(recipe);
                    _recipeRepository.Save();
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
                var recipe = _recipeRepository.GetRecipe(id);
                if (recipe != null && recipe.UserId.Equals(userId)) {
                    _recipeRepository.DeleteRecipe(id);
                    _recipeRepository.Save();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddIngredient([Bind("RecipeIngredients")] Recipe recipeData)
        {
            if (recipeData.RecipeIngredients != null)
            {
                recipeData.RecipeIngredients.Add(new RecipeIngredient());
            }
            else
            {
                recipeData.RecipeIngredients = new List<RecipeIngredient>() { new RecipeIngredient() };
            }
            
            return PartialView("RecipeIngredients", recipeData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteIngredient([Bind("RecipeIngredients")] Recipe recipeData)
        {
            if (recipeData.RecipeIngredients != null)
            {
                var last = recipeData.RecipeIngredients.Last();
                if (last != null)
                {
                    recipeData.RecipeIngredients.Remove(last);
                }
            }
            else
            {
                recipeData.RecipeIngredients = new List<RecipeIngredient>() { new RecipeIngredient() };
            }
            
            
            return PartialView("RecipeIngredients", recipeData);
        }

        [HttpGet]
        public string GetAllRecipes()
        {
            var recipes = _recipeRepository.GetRecipes();
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
