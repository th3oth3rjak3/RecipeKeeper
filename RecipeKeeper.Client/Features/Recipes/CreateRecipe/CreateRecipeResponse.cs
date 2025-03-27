using RecipeKeeper.Client.Features.Ingredients.GetIngredient;
using RecipeKeeper.Client.Features.Instructions.GetInstruction;

namespace RecipeKeeper.Client.Features.Recipes.CreateRecipe;

public class CreateRecipeResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Author { get; set; }
    public string? Description { get; set; }
    public string? Difficulty { get; set; }
    public string? EstimatedDuration { get; set; }
    public List<GetIngredientResponse> Ingredients { get; set; } = [];
    public List<GetInstructionResponse> Instructions { get; set; } = [];
}