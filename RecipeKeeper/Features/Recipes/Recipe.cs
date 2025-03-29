using System.ComponentModel.DataAnnotations;
using RecipeKeeper.Client.Features.Recipes;
using RecipeKeeper.Client.Features.Recipes.CreateRecipe;
using RecipeKeeper.Client.Features.Recipes.GetRecipes;
using RecipeKeeper.Features.Ingredients;
using RecipeKeeper.Features.Instructions;

namespace RecipeKeeper.Features.Recipes;

public class Recipe
{
    [Required]
    public required int Id { get; init; }

    [Required]
    [StringLength(250, MinimumLength = 1)]
    public required string Name { get; init; }

    [StringLength(100)]
    public string? Author { get; init; }

    [StringLength(2000)]
    public string? Description { get; init; }

    [StringLength(100)]
    public string? Difficulty { get; init; }

    [StringLength(100)]
    public string? EstimatedDuration { get; init; }

    public List<Ingredient> Ingredients { get; init; } = [];
    public List<Instruction> Instructions { get; init; } = [];
}

public static class RecipeExtensions
{
    public static Recipe ToRecipe(this CreateRecipeRequest request) =>
        new()
        {
            Id = 0,
            Name = request.Name ?? "",
            Description = request.Description,
            Author = request.Author,
            Difficulty = request.Difficulty,
            EstimatedDuration = request.EstimatedDuration,
        };

    public static RecipeResponse ToResponse(this Recipe recipe) =>
        new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Author = recipe.Author,
            Description = recipe.Description,
            EstimatedDuration = recipe.EstimatedDuration,
            Difficulty = recipe.Difficulty,
            Ingredients = recipe.Ingredients.Select(ingredient => ingredient.ToResponse()).ToList(),
            Instructions = recipe.Instructions.Select(instruction => instruction.ToResponse()).ToList(),
        };

    public static SearchRecipeResponse ToSearchResponse(this Recipe recipe) =>
        new()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Author = recipe.Author,
            EstimatedDuration = recipe.EstimatedDuration,
            Difficulty = recipe.Difficulty,
            Description = recipe.Description,
        };
}