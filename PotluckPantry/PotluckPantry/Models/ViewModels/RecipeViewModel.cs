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
        public IEnumerable<Review> RecipeReviews { get; set; }

        public RecipeViewModel(Recipe recipe)
        {
            this.Id = recipe.Id;
            this.Description = recipe.Description;
            this.AvgScore = recipe.AvgScore;
            this.PostTime = recipe.PostTime;
            this.Title = recipe.Title;
            this.RecipeIngredients = recipe.RecipeIngredients;
            this.UserId = recipe.UserId;
            this.User = recipe.User;
        }
    }
}
