using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static PotluckPantry.Data.ApplicationDbContext;

namespace PotluckPantry.Areas.Data.Entities
{
    public class Recipe
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostTime { get; set; }
        public virtual List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
