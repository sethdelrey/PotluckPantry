using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PotluckPantry.Areas.Data.Entities
{
    public class Recipe
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }
        //[Required]
        [MaxLength(36)]
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostTime { get; set; }
        public virtual List<RecipeIngredient> RecipeIngredients { get; set; }
        public double AvgScore { get; set; }

        [DisplayName("What is the cuisine of the recipe?")]
        [Required(ErrorMessage = "Please select one.")]
        public RecipeCategory Category { get; set; }

        [DisplayName("What is the meat type of the recipe?")]
        [Required(ErrorMessage = "Please select one.")]
        public RecipeMeat Meat { get; set; }
        
        public Recipe()
        {
            
        }

        public Recipe(string title)
        {
            RecipeIngredients = new List<RecipeIngredient>() { new RecipeIngredient() };
            this.Title = title;
        }
    }
}
