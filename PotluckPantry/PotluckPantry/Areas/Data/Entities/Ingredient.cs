using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static PotluckPantry.Data.ApplicationDbContext;

namespace PotluckPantry.Areas.Data.Entities
{
    public class Ingredient
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
