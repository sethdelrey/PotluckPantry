using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Areas.Data.Entities
{
    public class RecipeIngredient
    {
        public string RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public string IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public string Amount { get; set; }
        public string NormalizedAmount { get; set; }
    }
}
