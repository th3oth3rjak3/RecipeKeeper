using Functional;
using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Client.Features.Ingredients;
using RecipeKeeper.Client.Features.Ingredients.CreateIngredient;
using RecipeKeeper.Client.Features.Ingredients.UpdateIngredient;
using RecipeKeeper.Persistence;
using static Functional.Prelude;

namespace RecipeKeeper.Features.Ingredients;

public class IngredientService(RecipeKeeperContext context) {
    public async Task<Result<IngredientResponse, Exception>> CreateIngredientAsync(int recipeId, CreateIngredientRequest request) =>
        await TryAsync(async () => {
            var ingredient = request.ToIngredient(recipeId);
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();

            return ingredient.ToResponse();
        });

    public async Task<Result<IngredientResponse, Exception>> UpdateIngredientAsync(int recipeId, int id, UpdateIngredientRequest request) =>
        await TryAsync(async () => {
            var found = await context.Ingredients.Where(ingredient => ingredient.RecipeId == recipeId && ingredient.Id == id).FirstAsync();
            context.Entry(found).CurrentValues.SetValues(request);
            await context.SaveChangesAsync();

            return found.ToResponse();
        });

    public async Task<Result<Unit, Exception>> DeleteIngredientAsync(int recipeId, int id) =>
        await TryAsync(async () => {
            var found = await context.Ingredients.Where(ingredient => ingredient.RecipeId == recipeId && ingredient.Id == id).FirstAsync();
            context.Remove(found);
            await context.SaveChangesAsync();
        });
}
