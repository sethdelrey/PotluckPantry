using Microsoft.AspNetCore.Mvc;
using PotluckPantry.Areas.Data.Accessors;
using PotluckPantry.Areas.Data.Entities;

namespace PotluckPantry.Controllers
{
    public class ReviewController : Controller
    {
        public IRecipeRepository _recipeRepository { get; set; }
        private IReviewRepository _reviewRepository { get; set; }

        public ReviewController(IRecipeRepository recipeRepository, IReviewRepository reviewRepository)
        {
            _recipeRepository = recipeRepository;
            _reviewRepository = reviewRepository;
        }

        public IActionResult Index(string recipeId)
        {
            var recipe = _recipeRepository.GetRecipe(recipeId);

            return View("Review", new Review() 
            { 
                Recipe = recipe
            });
        }

        public IActionResult Review(Review review)
        {
            _reviewRepository.CreateReview(review);

            return RedirectToAction("Index", "Recipe", new { id = review.RecipeId});
        }
    }
}
