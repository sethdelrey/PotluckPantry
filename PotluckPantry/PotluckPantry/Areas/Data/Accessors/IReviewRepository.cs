using PotluckPantry.Areas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Areas.Data.Accessors
{
    public interface IReviewRepository
    {
        public Review GetReview(string reviewId);
        public void CreateReview(Review review);
        public void DeleteReview(string reviewId);
        public void UpdateReview(Review review);
        public IEnumerable<Review> GetRecipesReviews(string recipeId);
        public void Save();
    }
}
