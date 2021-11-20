using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PotluckPantry.Areas.Data.Accessors;
using PotluckPantry.Areas.Data.Entities;
using PotluckPantry.Areas.Data.Extension;
using System;

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

        [Authorize]
        public IActionResult Index(string recipeId)
        {
            if (!string.IsNullOrEmpty(recipeId))
            {
                var recipe = _recipeRepository.GetRecipe(recipeId);

                if (recipe != null)
                {
                    return View("Review", new Review()
                    {
                        Recipe = recipe,
                        RecipeId = recipeId
                    });
                }
            }

            return RedirectToAction("Error", "Home");
        }

        [Authorize]
        public IActionResult Review(Review review)
        {
            if (review != null)
            {
                review.Id = Guid.NewGuid().ToString();
                review.UserId = User.GetLoggedInUserId<string>();
                review.ReviewTime = DateTime.Now;
                _recipeRepository.UpdateAverageScore(review.RecipeId, review.Score);
                _reviewRepository.CreateReview(review);

                _reviewRepository.Save();
                return RedirectToAction("Index", "Recipe", new { id = review.RecipeId });
            }

            return RedirectToAction("Error", "Home");
        }
    }
}
