using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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
        public int AvgScore { get; set; }

        public Recipe()
        {
            RecipeIngredients = new List<RecipeIngredient>();
        }

        public Recipe(string title)
        {
            this.Title = title;
        }
    }
}
