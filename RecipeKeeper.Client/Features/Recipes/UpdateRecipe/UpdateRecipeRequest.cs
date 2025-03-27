using System.ComponentModel.DataAnnotations;

namespace RecipeKeeper.Client.Features.Recipes.UpdateRecipe;

public class UpdateRecipeRequest
{
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? Difficulty { get; set; }
    public string? EstimatedDuration { get; set; }
}
