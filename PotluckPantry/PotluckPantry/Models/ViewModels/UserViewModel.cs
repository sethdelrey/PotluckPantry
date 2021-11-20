using Microsoft.AspNetCore.Identity;
using PotluckPantry.Areas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Models.ViewModels
{
    public class UserViewModel
    {
        public IdentityUser user { get; set; }

        public IEnumerable<Recipe> userRecipes { get; set; }
    }
}
