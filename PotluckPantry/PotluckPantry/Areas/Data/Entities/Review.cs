using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Areas.Data.Entities
{
    public class Review
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        [ForeignKey("AspNetUsers")]
        [Required]
        [MaxLength(36)]
        public string UserId { get; set; }
        [ForeignKey("Recipes")]
        [Required]
        [MaxLength(36)]
        public string RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual IdentityUser User { get; set; }
        [Required]
        public double Score { get; set; }
        [DisplayName("Desciption")]
        [Required(ErrorMessage = "Please write your review of the recipe.")]
        [MaxLength(1000, ErrorMessage = "The description has to be less than 1000 characters.")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ReviewTime { get; set; }
    }
}
