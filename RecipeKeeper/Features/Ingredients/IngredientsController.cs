using Microsoft.AspNetCore.Mvc;
using RecipeKeeper.Client.Features.Ingredients;
using RecipeKeeper.Client.Features.Ingredients.CreateIngredient;
using RecipeKeeper.Client.Features.Ingredients.UpdateIngredient;
using RecipeKeeper.Extensions;
using Serilog;

namespace RecipeKeeper.Features.Ingredients;

[Route("api/Recipes/{recipeId:int:required:min(1)}/Ingredients")]
[ApiController]
public class IngredientsController(IngredientService ingredientService) : ControllerBase {
    [HttpPost("")]
    [Produces<IngredientResponse>]
    public async Task<IResult> CreateIngredientAsync(int recipeId, [FromBody] CreateIngredientRequest request) =>
        await ingredientService
            .CreateIngredientAsync(recipeId, request)
            .OkResult(Log.Logger);

    [HttpPut("{id:int:required:min(1)}")]
    [Produces<IngredientResponse>]
    public async Task<IResult> UpdateIngredientAsync(int recipeId, int id, [FromBody] UpdateIngredientRequest request) =>
        await ingredientService
            .UpdateIngredientAsync(recipeId, id, request)
            .OkResult(Log.Logger);

    [HttpDelete("{id:int:required:min(1)}")]
    public async Task<IResult> DeleteIngredientAsync(int recipeId, int id) =>
        await ingredientService
            .DeleteIngredientAsync(recipeId, id)
            .NoContentResult(Log.Logger);
}