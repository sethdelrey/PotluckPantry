using PotluckPantry.Areas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Models
{
    public class HomeModel
    {
        IEnumerable<Recipe> Recipes { get; set; }
    }
}
