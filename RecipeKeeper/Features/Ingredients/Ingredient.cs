using System.ComponentModel.DataAnnotations;
using RecipeKeeper.Client.Features.Ingredients;
using RecipeKeeper.Client.Features.Ingredients.CreateIngredient;
using RecipeKeeper.Features.Recipes;

namespace RecipeKeeper.Features.Ingredients;

public class Ingredient
{
    [Required]
    public int Id { get; init; }

    [Required]
    public int RecipeId { get; init; }

    [Required]
    public Recipe? Recipe { get; init; }

    [Required]
    public required int Position { get; init; }

    [Required]
    [StringLength(500, MinimumLength = 1)]
    public required string Description { get; init; }
}

public static class IngredientExtensions
{
    public static Ingredient ToIngredient(this CreateIngredientRequest request, int recipeId) =>
        new()
        {
            RecipeId = recipeId,
            Position = request.Position ?? 1,
            Description = request.Description ?? "",
        };

    public static IngredientResponse ToResponse(this Ingredient ingredient) =>
        new()
        {
            Id = ingredient.Id,
            Position = ingredient.Position,
            Description = ingredient.Description,
        };
}