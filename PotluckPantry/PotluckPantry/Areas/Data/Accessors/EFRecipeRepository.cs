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
            throw new NotImplementedException();
        }

        public Recipe GetRecipe(string recipeId)
        {
            return _context.Recipes.AsNoTracking().Where(r => r.Id.Equals(recipeId)).Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).First();
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateRecipe(string recipeId)
        {
            throw new NotImplementedException();
        }
    }
}
