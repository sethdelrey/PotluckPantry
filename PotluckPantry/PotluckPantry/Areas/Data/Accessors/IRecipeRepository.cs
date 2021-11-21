using Microsoft.AspNetCore.Mvc;
using PotluckPantry.Areas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Areas.Data.Accessors
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetRecipes();
        public Recipe GetRecipe(string recipeId);
        public void DeleteRecipe(string recipeId);
        public void UpdateRecipe(Recipe recipe);
        public IEnumerable<Recipe> SearchRecipes(string searchKey);
        public void CreateRepice(Recipe recipe);
        public IEnumerable<Recipe> GetRecipesByUser(string userId);
        public void UpdateAverageScore(string recipeId, double newRating);
        public IEnumerable<Recipe> GetRecipesByCategory(RecipeCategory category);
        public void Save();
    }
}
