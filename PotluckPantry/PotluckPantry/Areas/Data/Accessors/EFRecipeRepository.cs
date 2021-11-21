using Microsoft.EntityFrameworkCore;
using PotluckPantry.Areas.Data.Entities;
using PotluckPantry.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Areas.Data.Accessors
{
    public class EFRecipeRepository : IRecipeRepository
    {
        private ApplicationDbContext _context;

        public EFRecipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateRepice(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
        }

        public void DeleteRecipe(string recipeId)
        {
            Recipe recipe = new() { Id = recipeId };
            _context.Recipes.Attach(recipe);
            _context.Recipes.Remove(recipe);
        }

        public Recipe GetRecipe(string recipeId)
        {
            return _context.Recipes.AsNoTracking().Where(r => r.Id.Equals(recipeId)).Include(r => r.User).Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).First();
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            return _context.Recipes.ToList();
        }

        public IEnumerable<Recipe> SearchRecipes(string searchKey)
        {
            return _context.Recipes.Where(r => r.Title.ToUpper().Contains(searchKey.ToUpper())).ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
        }

        public void UpdateAverageScore(string recipeId, double newRating)
        {
            var recipe = GetRecipe(recipeId);
            var reviewCount = _context.Reviews.AsNoTracking().Where(r => r.RecipeId.Equals(recipeId)).Count();
            var newAvgScore = 0.0;
            if (reviewCount == 0)
            {
                newAvgScore = newRating;
            }
            else
            {
                newAvgScore = Math.Round((recipe.AvgScore + newRating) / reviewCount, 1 );
            }
            recipe.AvgScore = newAvgScore;
            UpdateRecipe(recipe);
        }
        public IEnumerable<Recipe> GetRecipesByCategory(RecipeCategory category)
        {
            return _context.Recipes.Where(r => r.Category.Equals(category)).ToList();
        }

        public IEnumerable<Recipe> GetRecipesByUser(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var usersRecipes = _context.Recipes.AsNoTracking().Where(r => r.UserId.Equals(userId)).Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).ToList();
                return usersRecipes;
            }

            return null;
        }
    }
}
