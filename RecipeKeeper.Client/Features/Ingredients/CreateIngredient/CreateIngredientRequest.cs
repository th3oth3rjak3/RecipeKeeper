using System.ComponentModel.DataAnnotations;

namespace RecipeKeeper.Client.Features.Ingredients.CreateIngredient;

public class CreateIngredientRequest
{
    [Required]
    public int? Position { get; set; }

    [Required]
    public string? Description { get; set; }
}
