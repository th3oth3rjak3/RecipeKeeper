using System.Net.Http.Json;
using Functional;
using RecipeKeeper.Client.Features.Ingredients.CreateIngredient;
using RecipeKeeper.Client.Features.Ingredients.UpdateIngredient;
using static Functional.Prelude;

namespace RecipeKeeper.Client.Features.Ingredients;

public class IngredientService(HttpClient http)
{
    public async Task<Result<CreateIngredientResponse, Exception>> CreateIngredientAsync(int recipeId, CreateIngredientRequest request) =>
        await TryAsync(async () =>
        {
            var response = await http.PostAsJsonAsync($"/api/Recipes/{recipeId}/Ingredients", request);
            if (!response.IsSuccessStatusCode) throw new Exception("Error occurred while creating new ingredient");
            var contents = await response.Content.ReadFromJsonAsync<CreateIngredientResponse>();
            return contents ?? throw new Exception("Error occurred while creating new ingredient, response was empty.");
        });

    public async Task<Result<UpdateIngredientResponse, Exception>> UpdateIngredientAsync(int recipeId, int id, UpdateIngredientRequest request) =>
        await TryAsync(async () =>
        {
            var response = await http.PutAsJsonAsync($"/api/Recipes/{recipeId}/Ingredients/{id}", request);
            if (!response.IsSuccessStatusCode) throw new Exception("Error occurred while updating ingredient");
            var contents = await response.Content.ReadFromJsonAsync<UpdateIngredientResponse>();
            return contents ?? throw new Exception("Error occurred while updating ingredient, response was empty");
        });

    public async Task<Result<Unit, Exception>> DeleteIngredientAsync(int recipeId, int id) =>
        await TryAsync(async () =>
        {
            var response = await http.DeleteAsync($"/api/Recipes/{recipeId}/Ingredients/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception("Error occurred while deleting ingredient");
            return Unit();
        });
}
