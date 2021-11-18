using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Models
{
    public class NewRecipeModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RecipeIngredientModel> RecipeIngredients { get; set; }

        public NewRecipeModel()
        {
            RecipeIngredients = new List<RecipeIngredientModel>() { new RecipeIngredientModel()};
        }
    }
}
