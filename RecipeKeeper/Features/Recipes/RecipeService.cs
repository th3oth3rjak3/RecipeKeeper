using Functional;
using Microsoft.EntityFrameworkCore;
using RecipeKeeper.Client.Features.Recipes;
using RecipeKeeper.Client.Features.Recipes.CreateRecipe;
using RecipeKeeper.Client.Features.Recipes.GetRecipes;
using RecipeKeeper.Client.Features.Recipes.UpdateRecipe;
using RecipeKeeper.Persistence;
using static Functional.Prelude;

namespace RecipeKeeper.Features.Recipes;

/// <summary>
///     The service used to handle recipe requests.
/// </summary>
/// <param name="context">The database context.</param>
public class RecipeService(RecipeKeeperContext context) {
    /// <summary>
    ///     Get a recipe by its id.
    /// </summary>
    /// <param name="id">The recipe id.</param>
    /// <returns>A recipe or an exception.</returns>
    public async Task<Result<RecipeResponse, Exception>> GetRecipeAsync(int id) =>
        await TryAsync(() =>
            context
                .Recipes
                .Include(recipe => recipe.Ingredients)
                .Include(recipe => recipe.Instructions)
                .Where(recipe => recipe.Id == id)
                .FirstAsync()
                .PipeAsync(recipe => recipe.ToResponse()));


    /// <summary>
    ///     Search for recipes using a query string.
    /// </summary>
    /// <param name="query">The query string to use for searching.</param>
    /// <param name="includeIngredients">When true, search includes ingredient descriptions.</param>
    /// <param name="includeInstructions">When true, search includes instruction descriptions.</param>
    /// <returns>A list of search results or an exception.</returns>
    public async Task<Result<List<SearchRecipeResponse>, Exception>> SearchRecipeAsync(
        string? query,
        bool? includeIngredients,
        bool? includeInstructions
    ) =>
        await TryAsync(async () => {
            if (string.IsNullOrWhiteSpace(query)){
                return context.Recipes.Select(recipe => recipe.ToSearchResponse()).ToList();
            }

            var recipeIds = new HashSet<int>();

            if (includeIngredients == true){
                var idsFromIngredients = await context
                    .Ingredients
                    .AsNoTracking()
                    .Where(ingredient =>
                        ingredient
                            .Description
                            .ToUpper()
                            .Contains(query.ToUpper()))
                    .Select(ingredient => ingredient.RecipeId)
                    .ToListAsync();

                idsFromIngredients.ForEach(id => recipeIds.Add(id));
            }

            if (includeInstructions == true){
                var idsFromInstructions = await context
                    .Instructions
                    .AsNoTracking()
                    .Where(instruction =>
                        instruction
                            .Description
                            .ToUpper()
                            .Contains(query.ToUpper()))
                    .Select(instruction => instruction.RecipeId)
                    .ToListAsync();

                idsFromInstructions.ForEach(id => recipeIds.Add(id));
            }


            var idsFromRecipes = await context
                .Recipes
                .AsNoTracking()
                .Where(r =>
                    r.Name.ToUpper().Contains(query.ToUpper())
                    || r.Description!.ToUpper().Contains(query.ToUpper())
                    || r.Author!.ToUpper().Contains(query.ToUpper()))
                .Select(r => r.Id)
                .ToListAsync();

            idsFromRecipes.ForEach(id => recipeIds.Add(id));


            return context.Recipes.Where(recipe => recipeIds.Contains(recipe.Id)).Select(recipe => recipe.ToSearchResponse()).ToList();
        });

    /// <summary>
    ///     Create a new recipe.
    /// </summary>
    /// <param name="request">The properties of the recipe to be created.</param>
    /// <returns>The created recipe or an exception.</returns>
    public async Task<Result<RecipeResponse, Exception>> CreateRecipeAsync(CreateRecipeRequest request) =>
        await TryAsync(async () => {
            var recipe = request.ToRecipe();
            context.Recipes.Add(recipe);
            await context.SaveChangesAsync();

            return await context
                .Recipes
                .Where(r => r.Id == recipe.Id)
                .Select(found => found.ToResponse())
                .FirstAsync();
        });

    /// <summary>
    ///     Update a recipe.
    /// </summary>
    /// <param name="id">The id of the recipe to update.</param>
    /// <param name="request">The properties of the request to be updated.</param>
    /// <returns>The updated recipe or an exception.</returns>
    public async Task<Result<RecipeResponse, Exception>> UpdateRecipeAsync(int id, UpdateRecipeRequest request) =>
        await TryAsync(async () => {
            var found = await context.Recipes.Where(recipe => recipe.Id == id).FirstAsync();
            context.Entry(found).CurrentValues.SetValues(request);
            await context.SaveChangesAsync();

            var updated = await context
                .Recipes
                .Include(recipe => recipe.Ingredients)
                .Include(recipe => recipe.Instructions)
                .Where(recipe => recipe.Id == id)
                .FirstAsync()
                .PipeAsync(recipe => recipe.ToResponse());

            return updated;
        });

    /// <summary>
    ///     Delete a recipe by its id.
    /// </summary>
    /// <param name="id">The id of the recipe.</param>
    public async Task<Result<Unit, Exception>> DeleteRecipeAsync(int id) =>
        await TryAsync(async () => {
            var foundRecipe = await context.Recipes.Where(recipe => recipe.Id == id).FirstAsync();
            context.Recipes.Remove(foundRecipe);
            await context.SaveChangesAsync();
        });
}