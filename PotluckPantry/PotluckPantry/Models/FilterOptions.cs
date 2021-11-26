using PotluckPantry.Areas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Models
{
    public class FilterOptions
    {
        public RecipeCategory Category { get; set; }
        public RecipeMeat Meat { get; set; }
        public int MinAvgRating { get; set; }
    }
}
