using RecipeKeeper.Client.Features.Ingredients;
using RecipeKeeper.Client.Features.Instructions;

namespace RecipeKeeper.Client.Features.Recipes;

public class RecipeResponse
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Author { get; init; }
    public string? Description { get; init; }
    public string? EstimatedDuration { get; init; }
    public string? Difficulty { get; init; }
    public List<IngredientResponse> Ingredients { get; set; } = [];
    public List<InstructionResponse> Instructions { get; set; } = [];
}