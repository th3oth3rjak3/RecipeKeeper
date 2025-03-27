using System.Net.Http.Json;
using Functional;
using RecipeKeeper.Client.Builders;
using RecipeKeeper.Client.Features.Recipes.CreateRecipe;
using RecipeKeeper.Client.Features.Recipes.GetRecipes;
using RecipeKeeper.Client.Features.Recipes.UpdateRecipe;
using static Functional.Prelude;

namespace RecipeKeeper.Client.Features.Recipes;

public class RecipeService(HttpClient http)
{
    public async Task<Result<List<SearchRecipeResponse>, Exception>> SearchRecipesAsync(
        string? query,
        bool? includeIngredients,
        bool? includeInstructions
    ) =>
        await TryAsync(async () =>
        {
            var queryString = new QueryBuilder()
                .Add("query", query)
                .Add("includeIngredients", includeIngredients)
                .Add("includeInstructions", includeInstructions)
                .ToString();

            var response = await http.GetAsync($"/api/Recipes?{queryString}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error occurred while searching for recipes");
            }

            var recipes = await response.Content.ReadFromJsonAsync<List<SearchRecipeResponse>>();
            return recipes ?? [];
        });

    public async Task<Result<GetRecipeResponse, Exception>> GetRecipeAsync(int id) =>
        await TryAsync(async () =>
        {
            var response = await http.GetAsync($"/api/Recipes/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception("Error occurred while getting recipe");

            return await response.Content.ReadFromJsonAsync<GetRecipeResponse>() ?? throw new Exception("Recipe was not found");
        });

    public async Task<Result<CreateRecipeResponse, Exception>> CreateRecipeAsync(CreateRecipeRequest request) =>
        await TryAsync(async () =>
        {
            var response = await http.PostAsJsonAsync("/api/Recipes", request);
            if (!response.IsSuccessStatusCode) throw new Exception("Error occurred while creating new recipe");

            var result = await response.Content.ReadFromJsonAsync<CreateRecipeResponse>();

            return result ?? throw new Exception("Error occurred while creating new recipe, the response was empty.");
        });

    public async Task<Result<UpdateRecipeResponse, Exception>> UpdateRecipeAsync(int recipeId, UpdateRecipeRequest request) =>
        await TryAsync(async () =>
        {
            var response = await http.PutAsJsonAsync($"/api/Recipes/{recipeId}", request);
            if (!response.IsSuccessStatusCode) throw new Exception("Error occurred while updating recipe");

            return await response.Content.ReadFromJsonAsync<UpdateRecipeResponse>() ?? throw new Exception("Error occurred while updating recipe, the response was empty.");
        });

    public async Task<Result<Unit, Exception>> DeleteRecipeAsync(int recipeId) =>
        await TryAsync(async () =>
        {
            var response = await http.DeleteAsync($"/api/Recipes/{recipeId}");
            if (!response.IsSuccessStatusCode) throw new Exception("Error occurred while deleting recipe");
            return Unit();
        });
}
