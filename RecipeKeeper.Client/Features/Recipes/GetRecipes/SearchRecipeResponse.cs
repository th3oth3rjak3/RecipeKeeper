namespace RecipeKeeper.Client.Features.Recipes.GetRecipes;

public class SearchRecipeResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Author { get; set; }
    public string? EstimatedDuration { get; set; }
    public string? Difficulty { get; set; }
    public string? Description { get; set; }
}