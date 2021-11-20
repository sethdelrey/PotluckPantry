using PotluckPantry.Areas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Areas.Data.Accessors
{
    public interface IIngredientRepository
    {
        public Ingredient GetIngredient(string id);
        public Ingredient GetIngredientByName(string name);
        public bool DoesIngredientExist(string name);
        public void CreateIngredient(Ingredient ingredient);
        public void UpdateIngredient(Ingredient ingredient);
        public void DeleteIngredient(string id);
        public void Save();
        public List<RecipeIngredient> RecipeIngredientMapper(List<RecipeIngredient> recipeIngredients, string recipeId);
    }
}
