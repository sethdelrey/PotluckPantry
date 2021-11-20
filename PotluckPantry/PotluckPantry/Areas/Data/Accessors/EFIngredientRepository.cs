using Microsoft.EntityFrameworkCore;
using PotluckPantry.Areas.Data.Entities;
using PotluckPantry.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PotluckPantry.Areas.Data.Accessors
{
    public class EFIngredientRepository : IIngredientRepository
    {
        private ApplicationDbContext _context;

        public EFIngredientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
        }

        public void DeleteIngredient(string id)
        {
            throw new NotImplementedException();
        }

        public bool DoesIngredientExist(string name)
        {
            return _context.Ingredients.AsNoTracking().Where(i => i.Name.Equals(name)).CountAsync().Result != 0;
        }

        public Ingredient GetIngredient(string id)
        {
            return _context.Ingredients.AsNoTracking().Where(i => i.Id.Equals(id)).FirstOrDefault();
        }

        public Ingredient GetIngredientByName(string name)
        {
            return _context.Ingredients.AsNoTracking().Where(i => i.Name.Equals(name)).FirstOrDefault();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public List<RecipeIngredient> RecipeIngredientMapper(List<RecipeIngredient> recipeIngredients, string recipeId)
        {
            List<RecipeIngredient> ri = new();
            foreach (var recipeIngredient in recipeIngredients)
            {
                string ingredientId;
                if (DoesIngredientExist(recipeIngredient.Ingredient.Name))
                {
                    var ingridientFromRepo = GetIngredientByName(recipeIngredient.Ingredient.Name);
                    ingredientId = ingridientFromRepo.Id;
                }
                else
                {
                    ingredientId = Guid.NewGuid().ToString();
                    CreateIngredient(new Ingredient()
                    {
                        Id = ingredientId,
                        Name = recipeIngredient.Ingredient.Name
                    }
                    );
                }

                ri.Add(new RecipeIngredient()
                {
                    Amount = recipeIngredient.Amount,
                    NormalizedAmount = recipeIngredient.Amount.ToUpperInvariant(),
                    IngredientId = ingredientId,
                    RecipeId = recipeId
                }
                );
            }

            return ri;
        }
    }
}
