using PotluckPantry.Areas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Models.ViewModels
{
    public class RecipeViewModel : Recipe
    {
        public bool IsUsersRecipe { get; set; }

/*        public RecipeViewModel(Recipe recipe)
        {
            
        }*/
    }
}
