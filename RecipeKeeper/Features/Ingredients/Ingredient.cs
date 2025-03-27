using System.ComponentModel.DataAnnotations;
using RecipeKeeper.Client.Features.Ingredients.CreateIngredient;
using RecipeKeeper.Client.Features.Ingredients.GetIngredient;
using RecipeKeeper.Client.Features.Ingredients.UpdateIngredient;
using RecipeKeeper.Features.Recipes;

namespace RecipeKeeper.Features.Ingredients;

public class Ingredient
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int RecipeId { get; set; }

    [Required]
    public Recipe? Recipe { get; set; }

    [Required]
    public required int Position { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 1)]
    public required string Description { get; set; }
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

    public static CreateIngredientResponse ToCreateResponse(this Ingredient ingredient) =>
        new()
        {
            Id = ingredient.Id,
            Position = ingredient.Position,
            Description = ingredient.Description,
        };

    public static GetIngredientResponse ToGetResponse(this Ingredient ingredient) =>
        new()
        {
            Id = ingredient.Id,
            Position = ingredient.Position,
            Description = ingredient.Description,
        };

    public static UpdateIngredientResponse ToUpdateResponse(this Ingredient ingredient) =>
        new()
        {
            Id = ingredient.Id,
            Position = ingredient.Position,
            Description = ingredient.Description,
        };
}