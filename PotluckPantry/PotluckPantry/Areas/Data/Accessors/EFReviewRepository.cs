using Microsoft.EntityFrameworkCore;
using PotluckPantry.Areas.Data.Entities;
using PotluckPantry.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Areas.Data.Accessors
{
    public class EFReviewRepository : IReviewRepository
    {
        private ApplicationDbContext _context { get; set; }

        public EFReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteReview(string reviewId)
        {
            Review review = new() { Id = reviewId };
            _context.Reviews.Attach(review);
            _context.Reviews.Remove(review);
        }

        public Review GetReview(string reviewId)
        {
            return _context.Reviews.AsNoTracking().Include(r => r.User).Where(r => r.Id.Equals(reviewId)).FirstOrDefault();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
        }

        public void CreateReview(Review review)
        {
            _context.Reviews.Add(review);
        }

        public IEnumerable<Review> GetRecipesReviews(string recipeId)
        {
            if (!string.IsNullOrEmpty(recipeId))
            {
                return _context.Reviews.AsNoTracking().Where(r => r.RecipeId.Equals(recipeId)).Include(r => r.User).ToList();
            }
            return new List<Review>();
        }
    }
}
