using Microsoft.AspNetCore.Mvc;
using RecipeKeeper.Client.Features.Recipes.CreateRecipe;
using RecipeKeeper.Client.Features.Recipes.GetRecipes;
using RecipeKeeper.Client.Features.Recipes.UpdateRecipe;
using RecipeKeeper.Extensions;
using Serilog;

namespace RecipeKeeper.Features.Recipes
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController(RecipeService recipeService) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IResult> SearchRecipes(
            [FromQuery] string? query,
            [FromQuery] bool? includeIngredients,
            [FromQuery] bool? includeInstructions
        ) =>
            await recipeService
                .SearchRecipeAsync(query, includeIngredients, includeInstructions)
                .OkResult(Log.Logger);

        [HttpGet("{id:int:min(1)}")]
        [Produces<GetRecipeResponse>]
        public async Task<IResult> GetRecipeById(int id) =>
            await recipeService
                .GetRecipeAsync(id)
                .OkResult(Log.Logger);

        [HttpPost("")]
        [Produces<CreateRecipeResponse>]
        public async Task<IResult> CreateRecipe([FromBody] CreateRecipeRequest recipe) =>
            await recipeService
                .CreateRecipeAsync(recipe)
                .OkResult(Log.Logger);

        [HttpPut("{id:int:min(1)}")]
        public async Task<IResult> UpdateRecipe(int id, [FromBody] UpdateRecipeRequest recipe) =>
            await recipeService
                .UpdateRecipeAsync(id, recipe)
                .OkResult(Log.Logger);

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IResult> DeleteRecipe(int id) =>
            await recipeService
                .DeleteRecipeAsync(id)
                .NoContentResult(Log.Logger);
    }
}
