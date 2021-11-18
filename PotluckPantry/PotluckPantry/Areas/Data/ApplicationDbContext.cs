using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PotluckPantry.Areas.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PotluckPantry.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(256));

            builder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId, ri.NormalizedAmount });

            builder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId);

            builder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId);

            /*var ingredients = new[]
            {
                new Ingredient() 
                {
                    Id = "613d362f-8b05-43c6-822d-b84e4d9f5f59",
                    Name = "Ponce",
                    Description = "A Pig Stomach, cleaned"
                },
                new Ingredient()
                {
                    Id = "7bcc0279-8b61-4924-9950-b729b40cd0b5",
                    Name = "Trinity",
                    Description = "Onion, Green Bell Pepper, and Celery"
                }
            };

            var recipe = new[]
            {
                new Recipe()
                {
                    Id = "5e07d9ea-6016-4a10-ae58-6ae2e8666f88",
                    Title = "Baked Ponce",
                    PostTime = DateTime.Now,
                    Description = "Bake Ponce and Trinity at 350 degrees for 2 hours or until ponce is tender. Serve warm."
                }
            };

            var ponceIngredients = new[]
            {
                new RecipeIngredient()
                {
                    RecipeId = "5e07d9ea-6016-4a10-ae58-6ae2e8666f88",
                    IngredientId = "613d362f-8b05-43c6-822d-b84e4d9f5f59",
                    Amount = "1 Ponce",
                    NormalizedAmount = "1 PONCE"
                },
                new RecipeIngredient()
                {
                    RecipeId = "5e07d9ea-6016-4a10-ae58-6ae2e8666f88",
                    IngredientId = "7bcc0279-8b61-4924-9950-b729b40cd0b5",
                    Amount = "2 Cups",
                    NormalizedAmount = "2 CUPS"
                }
            };

            builder.Entity<Ingredient>().HasData(ingredients);
            builder.Entity<Recipe>().HasData(recipe);
            builder.Entity<RecipeIngredient>().HasData(ponceIngredients);*/
            base.OnModelCreating(builder);
        }
    }


}
