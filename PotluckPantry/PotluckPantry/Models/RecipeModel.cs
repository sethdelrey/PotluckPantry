﻿using PotluckPantry.Areas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Models
{
    public class RecipeModel
    {
        public Recipe Recipe { get; set; }
        public bool IsUsersRecipe { get; set; }
    }
}
