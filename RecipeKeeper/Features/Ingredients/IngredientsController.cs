using Microsoft.AspNetCore.Mvc;
using RecipeKeeper.Client.Features.Ingredients.CreateIngredient;
using RecipeKeeper.Client.Features.Ingredients.UpdateIngredient;
using RecipeKeeper.Extensions;
using Serilog;

namespace RecipeKeeper.Features.Ingredients
{
    [Route("api/Recipes")]
    [ApiController]
    public class IngredientsController(IngredientService ingredientService) : ControllerBase
    {
        [HttpPost("{recipeId:int:required:min(1)}/Ingredients")]
        [Produces<CreateIngredientResponse>]
        public async Task<IResult> CreateIngredientAsync(int recipeId, [FromBody] CreateIngredientRequest request) =>
            await ingredientService
                .CreateIngredientAsync(recipeId, request)
                .OkResult(Log.Logger);

        [HttpPut("{recipeId:int:required:min(1)}/Ingredients/{id:int:required:min(1)}")]
        [Produces<UpdateIngredientResponse>]
        public async Task<IResult> UpdateIngredientAsync(int recipeId, int id, [FromBody] UpdateIngredientRequest request) =>
            await ingredientService
                .UpdateIngredientAsync(recipeId, id, request)
                .OkResult(Log.Logger);

        [HttpDelete("{recipeId:int:required:min(1)}/Ingredients/{id:int:required:min(1)}")]
        public async Task<IResult> DeleteIngredientAsync(int recipeId, int id) =>
            await ingredientService
                .DeleteIngredientAsync(recipeId, id)
                .NoContentResult(Log.Logger);
    }
}
