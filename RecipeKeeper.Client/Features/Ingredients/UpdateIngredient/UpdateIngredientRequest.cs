using System.ComponentModel.DataAnnotations;

namespace RecipeKeeper.Client.Features.Ingredients.UpdateIngredient;

public class UpdateIngredientRequest
{
    [Required]
    public int RecipeId { get; set; }

    [Required]
    public int? Position { get; set; }

    [Required]
    public string? Description { get; set; }
}