using RecipeKeeper.Client.Features.Ingredients;
using RecipeKeeper.Client.Features.Instructions;

namespace RecipeKeeper.Client.Features.Recipes.UpdateRecipe;

public class UpdateRecipeResponse : IRecipe
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? Difficulty { get; set; }
    public string? EstimatedDuration { get; set; }
    public List<IIngredient> Ingredients { get; set; } = [];
    public List<IInstruction> Instructions { get; set; } = [];
}